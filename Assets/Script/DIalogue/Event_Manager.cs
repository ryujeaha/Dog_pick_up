using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
public class Event_Manager : MonoBehaviour
{
    //본인 인스턴스화.
    public static Event_Manager Instance;

    void Awake()
    {
        if(Instance == null){
            Instance = this;
        }else if(Instance != this){
           Destroy(gameObject);
        }
    }

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

    public enum Time{오전,오후,밤};//열거형(순서대로 0,1상수로 고정)을 사용한 오전/오후를 나타낼 수와 모든 일과를 끝냈을 경우를 나타낼 수.
    public Time c_Time = Time.오전;//현재시간
    public int day = 1;//현재 날짜
    [SerializeField]Text Day_TXT;//날짜
    [SerializeField] Text Time_TXT;//오전 오후
    Dialogue[] dialogue;

    //일과 시스템 관련
    public int[] Work = new int[2] {0,0};//오전과 오후의 일과데이터를 담을 배열
    public enum Working{조깅하기,운동하기,원반물기,사냥하기,공부하기,훈련하기}//일과 활동들에 대응되는 번호들.(편의성을 위해 +1을 해줘야 대응됨)
    //일과데이터와 매칭해서 이동해여할 씬 이름을 가져오기 위한 딕셔너리.
    public Dictionary<int,string> Mini_Game_SceneName = new Dictionary<int,string>()
    {
        {1,"Jogging"},
        {2,"Work_Out"},
        {3,"FlyingDisc"},
        {4,"Hunting"},
        {5,"Study"},
        {6,"Traning"},
    };
    //Event정보가 모여있는 데이터베이스 변수
    Event_DataBase E_Database;

    void Start()
    {
        Save_Manager.Instance.Load_On();
        Debug.Log("로드 완료");
        E_Database = GetComponent<Event_DataBase>();
    }

    void Update()
    {
        Time_Set();
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
                if(Input.GetKeyDown(KeyCode.Space)||Input.GetMouseButtonDown(0))
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
        //초기화
        isdialogue = false;
        context_count = 0;
        line_count = 0;
        dialogue = null;
        isnext = false;
        Setting_Dir_UI(false);
    }
    //이벤트 관련.
    public void Event(int Start_Num,int End_Num)//대화창을 꺼내는 함수.
    {
        Show_Dialogue(this.GetComponent<Interaction_Event>().GetDialogues(Start_Num,End_Num));
    }

    void Evnet_Gud()//이벤트 판정을 위한 함수.
    {
       E_Database.E_DataBase(day);
    }
    //시간 세팅 관련.
    void Time_Set()
    {
        Day_TXT.text = "개를 키운지 "+day+"일째";
        Time_TXT.text = c_Time.ToString();
    }
}
