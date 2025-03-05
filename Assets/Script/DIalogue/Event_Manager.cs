using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Event_Manager : MonoBehaviour
{
    //다이알로그 관련
    [SerializeField] GameObject Dialogue_Bar_Ui;
     [SerializeField] GameObject Dialogue_Name_Ui;
    [SerializeField]Text Dialogue_TXT;
    [SerializeField] Text Dialogue_Name_TXT;

    bool isdialogue = false;//대화중일 경우 true;
    bool isnext = false;// 특정 키 입력 대기.

    int line_count = 0;//대화카운트
    int context_count = 0;//대사 카운트

    //날짜 관련
    private int day = 1;//현재 날짜
    public int Time = 2;//행동력
    [SerializeField]Text Day_TXT;//날짜
    [SerializeField] Text Time_TXT;//오전 오후
    Dialogue[] dialogue;

    //이벤트 관련
    bool is_oneday = false;//첫 대화 중복실행방지.

    //애니매이터
    [SerializeField] Animator dog_Anim;
    
    // Update is called once per frame
    void Update()
    {
        Evnet_Gud();
        //다음으로 넘어가는지 키 체크 하는 부분.
        if(isdialogue)
        {
            if(isnext)
            {
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    isnext = false;
                    Dialogue_TXT.text = "";
                    if(++context_count < dialogue[line_count].context.Length)
                    {
                        StartCoroutine(TypeWriter());
                    }else{
                        context_count = 0;//0번째 인덱스에 대사부터 출력을 해야하기때문에
                        if(++line_count < dialogue.Length)
                        {
                            StartCoroutine(TypeWriter());
                        }else//대화 끝
                        {
                            End_dialogue();
                        }
                    } 
                }
            }
        }
    }
    public void Show_Dialogue(Dialogue[] p_dialogues)//대화가 시작되면 텍스트를 초기화하는 함수.
    {
        isdialogue = true;//대화중이다
        Dialogue_TXT.text = "";
        Dialogue_Name_TXT.text = "";

        dialogue = p_dialogues;//대사를 가져오고 넣어주기
        StartCoroutine(TypeWriter());
    }

    void Setting_Dir_UI(bool p_fleg)//대화UI를 키고끄는 함수.
    {
        Dialogue_Name_Ui.SetActive(p_fleg);
        Dialogue_Bar_Ui.SetActive(p_fleg);
    }
    IEnumerator TypeWriter()
    {
         Setting_Dir_UI(true);

         string t_replace_TXT = dialogue[line_count].context[context_count];//CSV구조상 못쓰는 문자들(,)등을 치환시킬 변수
         t_replace_TXT = t_replace_TXT.Replace("'",",");

         Dialogue_TXT.text = t_replace_TXT;

         isnext = true;
         yield return null;
    }
    void End_dialogue()
    {
        isdialogue = false;
        context_count = 0;
        line_count = 0;
        dialogue = null;
        isnext = false;
        Setting_Dir_UI(false);
    }
    public void Event(int Start_Num,int End_Num)//대화창을 꺼내는 함수.
    {
        Show_Dialogue(this.GetComponent<Interaction_Event>().GetDialogues(Start_Num,End_Num));
    }

    void Evnet_Gud()//이벤트 판정을 위한 함수.(하드코딩)
    {
        if(day == 1)//1일차 이벤트
        {   
            dog_Anim.SetBool("Is_Hurt",isdialogue);//첫번쨰 대화가 진행중이면 누워있는 모습,아니라면 멀쩡한 모습
            if(!is_oneday)//한번도 실행 안됬다면
            {   Event(1,6);               
                is_oneday = true;
            }
        }
    }
}
