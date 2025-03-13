using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
public class Event_Manager : MonoBehaviour
{

    //다이알로그 관련
    [SerializeField] GameObject Dialogue_Bar_Ui;
     [SerializeField] GameObject Dialogue_Name_Ui;
    [SerializeField]Text Dialogue_TXT;
    [SerializeField] Text Dialogue_Name_TXT;

    public bool isdialogue = false;//대화중일 경우 true;
    bool isnext = false;// 특정 키 입력 대기.

    [Header("텍스트 출력 딜레이")]
    [SerializeField] float textDelay;//텍스트 딜레이

    int line_count = 0;//대화카운트
    int context_count = 0;//대사 카운트

    //날짜 관련
    private int day = 1;//현재 날짜
    public int Time = 2;//행동력
    [SerializeField]Text Day_TXT;//날짜
    [SerializeField] Text Time_TXT;//오전 오후
    Dialogue[] dialogue;

    //튜토리얼 용 가이드 판
    [SerializeField]
    GameObject Guide_IMG;
    private bool guide_on = false;

    //Event정보가 모여있는 데이터베이스 변수
    Event_DataBase E_Database;



    void Start()
    {
        E_Database = GetComponent<Event_DataBase>();
    }

    void Update()
    {
        Evnet_Gud();
        //다음으로 넘어가는지 키 체크 하는 부분.
        TXT_Action();
    }

    void TXT_Action()
    {
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

         Dialogue_Name_TXT.text = dialogue[line_count].name;
         for(int i = 0; i < t_replace_TXT.Length; i++)
         {
              Dialogue_TXT.text += t_replace_TXT[i];//텍스트에 한글자씩 더해줌으로써 하나씩 출력되는 것처럼 보여줌.
             yield return new WaitForSeconds(textDelay);//딜레이 부여
         }
        

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
       E_Database.E_DataBase(day);
    }
    public void Guide_Open()
    {
        if(guide_on == false)
        {
            Guide_IMG.SetActive(true);//가이드 이미지 켜줌.
        }
    }
    public void Guide_Off()
    {
        Guide_IMG.SetActive(false);//가이드 이미지 꺼줌.
    }
}
