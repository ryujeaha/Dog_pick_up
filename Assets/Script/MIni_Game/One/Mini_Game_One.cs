using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;
public class Mini_Game_One : MonoBehaviour
{
    //타이머
    private float c_Time = 30;//현재시간; 기본값 60초
    [SerializeField] Text Timer_Txt;//제한시간 표시 텍스트

    //거리 재기(점수측정)
    public float score;//산책한 거리 변수.
    private float max_speed = 20;//산책점수 오르는 속도 변수 최대값;
    private float min_speed = 10;
    [SerializeField] Text Score_TxT;

    //씬 시작시 레디부분.
    bool is_Start;
    int Ready_Count = 3;//3초를 셀 변수.
    [SerializeField] Text Ready_TXT;

    //점프 관련
    [SerializeField] Rigidbody2D my_Rig;//본인의 리지드 바디 찾아주기
    public float jump_Porce;//점프 힘 정도 변수
    [SerializeField] GameObject G_check_box;
    bool isGrounded;//점프 체크 변수.

    //패턴 스폰 관련.
    [SerializeField] GameObject[] Pattens;//생성할 패턴들.
    public GameObject P_Spawns;//스폰포인트를 담을 변수
    public float spawn_cooltime;//쿨타임 기준 설정
    public float D_spawn_cooltime;//대쉬 시 쿨타임 기준 설정초기화를 위한 원래 쿨타임 저장변수
    public float cooltime;//실제 쿨타임 연산


    //체력바, 부스터 관련
    [SerializeField] Image Hp_Bar;//체력바
    [SerializeField] Image Boost_Bar;//부스트 바
    [SerializeField] GameObject Boost_alarm_TXT;

    public float Dog_c_Hp = 100;//현재 체력
    public float Dog_c_Boost = 0;//현재 부스트 게이지

    bool Ready_Boost = false;//부스트 가능여부 벼눗
    public bool isBoost;//부스트 중인지 판별한 변수

    //부스트 증가 관련
    bool Boost_cool_Start= false;//부스트 게이지 시작을 알릴 변수.
    float Boost_Up_Cnt = 0;//몇초마다 부스트게이지를 증가시킬지를 관리하는 변수

    float Dog_Max_Hp = 100;//최대 체력
    float Dog_Max_Boost = 100;//부스트 최대치.

    [SerializeField] float Boost_Speed;//증감되는 부스터 게이지 값

    //게임 오버 관련
    [SerializeField] GameObject Game_Over_screen;
    [SerializeField] Text reward_TxT;//점수를 보여줄 텍스트
    [SerializeField] Text r_Time_Txt;//달린시간을 보여줄 텍스트;
    [SerializeField] Text Stat_Txt;//스탯 추가치를 보여줄 텍스트(추후 구현)

    //애니메이터 관련
    [SerializeField] Animator P_anim;
    [SerializeField] Animator D_anim;

    //관련 스크립트 가져오기
    BG_Scrolling bG_Scrolling;//부스트 연출,시작 연출을 위한 배경이동 스크립트 가져오기

    // Start is called before the first frame update
    void Start()
    {
        D_spawn_cooltime = spawn_cooltime;//시작하자 마자 원래 쿨타임 저장.
        bG_Scrolling = FindAnyObjectByType<BG_Scrolling>();
        is_Start = false;
        Update_Bar();
        StartCoroutine(Ready_Coroutin());
    }

    // Update is called once per frame
    void Update()
    {
        if (is_Start)
        {
            Timer();
            Score_Check();
            Boost_On();
            Jump();
            SpawnCool();
            Game_Finish();
        }
    }


    public void Update_Bar()
    {
        Hp_Bar.fillAmount = (Dog_c_Hp) / (Dog_Max_Hp);//HP바 꽉 채우기.
        Boost_Bar.fillAmount = (Dog_c_Boost) / (Dog_Max_Boost);
    }

    public void Game_Finish()
    {
        if (c_Time <= 0 || Dog_c_Hp <= 0)
        {
            is_Start = false;
            reward_TxT.text = "산책한 거리: " + Mathf.Round(score).ToString() + "미터";
            r_Time_Txt.text = "산책한 시간: " + Mathf.Round((30 - c_Time)) + "초";
            Game_Over_screen.SetActive(true);
            Boost_alarm_TXT.SetActive(false);//알람 꺼주기   
            Time.timeScale = 0.01f;//시간 멈추기
        }
    }
    void Timer()
    {
        c_Time -= Time.deltaTime;
        Timer_Txt.text = Mathf.Round(c_Time).ToString() + ":00";//Mathf.Round(변수명)문법은 소수점을 제외하고 보여준다.
    }
    void Score_Check()//점수 체크
    {
        score += (Random.Range(min_speed,max_speed) * Time.deltaTime);
        Score_TxT.text = "현재 산책한 거리\n" + Mathf.Round(score).ToString() + "m";
    }

    void Jump()//점프 함수
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RaycastHit2D hit = Physics2D.Raycast(G_check_box.transform.position, Vector2.down, 0.2f);//맞은 객체의 정보를 저장할 변수 hit와 레이의 시작위치,방향,길이
            if (hit.collider != null && hit.collider.CompareTag("Floor"))
            {
                my_Rig.AddForce(Vector2.up * jump_Porce, ForceMode2D.Impulse);
            }
        }
    }

    void SpawnCool()//패턴 스폰 쿨타임 관련 함수.
    {
        if (isBoost)//대쉬중이라면
        { spawn_cooltime = 0.3f; }
        else{spawn_cooltime = D_spawn_cooltime;}
        cooltime -= Time.deltaTime;//실제 쿨타임 연산
        if (cooltime <= 0)//만약 쿨타임이 델타타임에서부터 깎여 0초가 되었다면
        {
            Spawn_Patten();//적 소환
            cooltime = spawn_cooltime;//쿨 기준 초기화 <= 다시 0이되면 적 스폰
        }
    }
    void Spawn_Patten()
    {
        int P_Num = Random.Range(0, Pattens.Length);//0에서 나올 패턴들의 끝 번호중에서 하나를 랜덤하게 추출.
        GameObject Patten = Instantiate(Pattens[P_Num], new Vector2(P_Spawns.transform.position.x + 0.5f, Pattens[P_Num].GetComponent<Game_One_Patten>().y_pos), Quaternion.identity);
    }


    void Boost_On()
    {
        if (Ready_Boost)//부스트가 가능하다면
        {
            if (Input.GetKeyDown(KeyCode.R) && !isBoost)//R을 눌렀고 이미 부스트 중이 아니라면
            {
                StartCoroutine(Boost());
                Boost_alarm_TXT.SetActive(false);//알람 꺼주기   
            }
        }
        else if (Boost_cool_Start)//부스트 쿨을 시작해야 한다면.(부스트가 가능한게 아니라면)
        {
            Boost_Up_Cnt -= Time.deltaTime;//초당 빼기
            if (Boost_Up_Cnt <= 0)//1초마다 부스트 게이지 올리기
            {
                Dog_c_Boost += Boost_Speed;
                Debug.Log("증가");
                Boost_Up_Cnt = 1;
                Update_Bar();
            }
            if (Dog_c_Boost >= 100 && !isBoost)//부스트 게이지가 일정량을 넘었고,부스트 중이 아니라면.
            {
                Boost_alarm_TXT.SetActive(true);//알람 켜주기
                Debug.Log("준비");
                Boost_cool_Start = false;//부스트 게이지 증가 안함.
                Dog_c_Boost = 100;//100고정
                Ready_Boost = true;//부스트 가능
            }
        }
    }

    IEnumerator Ready_Coroutin()//레디 단계 코루틴
    {
        while (!is_Start)//레디 단계일 동안.
        {
            if (Ready_Count > 0)//해야할 카운트 다운이 남았다면
            {
                Ready_TXT.text = Ready_Count.ToString();//카운트 다운 숫자 대입.
                Ready_Count--;
                yield return new WaitForSeconds(1f);//1초마다 반복
            }
            else
            {
                Ready_TXT.text = "산책가자!!";
                yield return new WaitForSeconds(1f); // 텍스트 1초 보여주기위해 대기 잠깐 보여주고
                Ready_TXT.gameObject.SetActive(false); // 텍스트 숨기기 등
                is_Start = true;
                bG_Scrolling.speed = -0.2f;//배경 움직이기
                P_anim.SetBool("Is_Work", true);
                P_anim.SetBool("Is_Idle", false);
                D_anim.SetBool("Is_RUN", true);
                Boost_cool_Start = true;//모든 작업이 끝났다면 부스트 게이지 증감 시작.
            }
        }
    }

    IEnumerator Boost()//부스트 온 함수
    {
        while (Dog_c_Boost > 0)//다 소진 되기 전까지.
        {
            isBoost = true;//무적 온
            Dog_c_Boost -= 35f;//5초 지속되도록
            Debug.Log("감소");
            Update_Bar();
            bG_Scrolling.speed = -3.0f;
            D_anim.SetFloat("Run_Speed", 5f);
            P_anim.SetFloat("P_Run_Speed", 2f);
            max_speed = 200;//점수 버프
            min_speed = 190;//점수 버프
            yield return new WaitForSeconds(1f);//1초마다 반복
        }
        //부스트 게이지가 모두 떨어졌다면.
        Dog_c_Boost = 0;//초기화
        isBoost = false;//무적 오프
        bG_Scrolling.speed = -0.2f;
        D_anim.SetFloat("Run_Speed", 1.5f);
        P_anim.SetFloat("P_Run_Speed", 1f);
        max_speed = 20;
        min_speed = 10;
        Boost_cool_Start = true;//모든 작업이 끝났다면 부스트 게이지 증감 시작.
        Ready_Boost = false;//증가해야할떄면 부스트키가 안되도록,이후 코루틴이 끝나면 자동으로 이 반복문이 되므로. 부스트를 막게됨.
    }
}
