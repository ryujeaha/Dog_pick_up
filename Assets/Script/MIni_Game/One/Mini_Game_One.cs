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

    // Start is called before the first frame update
    void Start()
    {
       is_Start = false;
       StartCoroutine(Ready_Coroutin());
    }

    // Update is called once per frame
    void Update()
    {
        if(is_Start)
        {
            Timer();
            Score_Check();
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
