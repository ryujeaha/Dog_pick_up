using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Mini_Game_One : MonoBehaviour
{
    //타이머
    private float c_Time = 60;//현재시간; 기본값 60초
    [SerializeField] Text Timer_Txt;//제한시간 표시 텍스트

    //거리 재기(점수측정)
    public float score;//산책한 거리 변수.
    private float speed = 10;//산책 속도 변수 기본값 1;
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
    public float cooltime;//실제 쿨타임 연산


    //체력바, 부스터 관련
    [SerializeField] Slider Hp_Bar;//체력바
    float Dog_Hp = 100;
    [SerializeField] Slider Boost_Bar;//부스트 바
    float Dog_Boost = 0;
    // Start is called before the first frame update
    void Start()
    {
       is_Start = false;
       StartCoroutine(Ready_Coroutin());
    }

    // Update is called once per frame
    void Update()
    {
        if (is_Start)
        {
            Timer();
            Score_Check();
            Jump();
            SpawnCool();
        }
    }

    void Timer()
    {
        c_Time -= Time.deltaTime;
        Timer_Txt.text = Mathf.Round(c_Time).ToString()+":00";//Mathf.Round(변수명)문법은 소수점을 제외하고 보여준다.
    }
    void Score_Check()//점수 체크
    {
        score += (speed  * Time.deltaTime);
        Score_TxT.text = "현재 산책한 거리\n"+Mathf.Round(score).ToString()+"m";
    }

        void Jump()//점프 함수
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("점프");
            RaycastHit2D hit = Physics2D.Raycast(G_check_box.transform.position,Vector2.down,0.2f);//맞은 객체의 정보를 저장할 변수 hit와 레이의 시작위치,방향,길이
            if(hit.collider != null && hit.collider.CompareTag("Floor"))
            {
                Debug.Log("발사!");
                my_Rig.AddForce(Vector2.up * jump_Porce, ForceMode2D.Impulse);
            }
            else if(hit.collider == null)
            {
                Debug.Log("점프 안되");
            }
        }
    }

    void SpawnCool()//패턴 스폰 쿨타임 관련 함수.
    {
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
        GameObject Patten = Instantiate(Pattens[P_Num], new Vector2(P_Spawns.transform.position.x + 0.5f, Pattens[P_Num].GetComponent<Game_One_Patten>().y_pos),Quaternion.identity);
    }
    IEnumerator Ready_Coroutin()//레디 단계 코루틴
    {
        while(!is_Start)//레디 단계일 동안.
        {
            if(Ready_Count > 0)//해야할 카운트 다운이 남았다면
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
            }
        }
    }
}
