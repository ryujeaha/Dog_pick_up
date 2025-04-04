using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
public class Event_Manager : MonoBehaviour
{
    //���� �ν��Ͻ�ȭ.
    public static Event_Manager Instance;

    void Awake()
    {
        if(Instance == null){
            Instance = this;
        }else if(Instance != this){
           Destroy(gameObject);
        }
    }

    //���̾˷α� ����
    [SerializeField] GameObject Dialogue_Bar_Ui;
     [SerializeField] GameObject Dialogue_Name_Ui;
    [SerializeField]Text Dialogue_TXT;
    [SerializeField] Text Dialogue_Name_TXT;


    public bool isdialogue = false;//��ȭ���� ��� true;
    bool isnext = false;// Ư�� Ű �Է� ���.

    [Header("�ؽ�Ʈ ��� ������")]
    [SerializeField] float textDelay;//�ؽ�Ʈ ������

    int line_count = 0;//��ȭī��Ʈ
    int context_count = 0;//��� ī��Ʈ

    //��¥ ����

    public enum Time{����,����,��};//������(������� 0,1����� ����)�� ����� ����/���ĸ� ��Ÿ�� ���� ��� �ϰ��� ������ ��츦 ��Ÿ�� ��.
    public Time c_Time = Time.����;//����ð�
    public int day = 1;//���� ��¥
    [SerializeField]Text Day_TXT;//��¥
    [SerializeField] Text Time_TXT;//���� ����
    Dialogue[] dialogue;

    //�ϰ� �ý��� ����
    public int[] Work = new int[2] {0,0};//������ ������ �ϰ������͸� ���� �迭
    public enum Working{�����ϱ�,��ϱ�,���ݹ���,����ϱ�,�����ϱ�,�Ʒ��ϱ�}//�ϰ� Ȱ���鿡 �����Ǵ� ��ȣ��.(���Ǽ��� ���� +1�� ����� ������)
    //�ϰ������Ϳ� ��Ī�ؼ� �̵��ؿ��� �� �̸��� �������� ���� ��ųʸ�.
    public Dictionary<int,string> Mini_Game_SceneName = new Dictionary<int,string>()
    {
        {1,"Jogging"},
        {2,"Work_Out"},
        {3,"FlyingDisc"},
        {4,"Hunting"},
        {5,"Study"},
        {6,"Traning"},
    };
    //Event������ ���ִ� �����ͺ��̽� ����
    Event_DataBase E_Database;

    void Start()
    {
        Save_Manager.Instance.Load_On();
        Debug.Log("�ε� �Ϸ�");
        E_Database = GetComponent<Event_DataBase>();
    }

    void Update()
    {
        Time_Set();
        Evnet_Gud();
        //�������� �Ѿ���� Ű üũ �ϴ� �κ�.
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
                        context_count = 0;//0��° �ε����� ������ ����� �ؾ��ϱ⶧����
                        if(++line_count < dialogue.Length)
                        {
                            StartCoroutine(TypeWriter());
                        }else//��ȭ ��
                        {
                            End_dialogue();
                        }
                    } 
                }
            }
        }
    }
    public void Show_Dialogue(Dialogue[] p_dialogues)//��ȭ�� ���۵Ǹ� �ؽ�Ʈ�� �ʱ�ȭ�ϴ� �Լ�.
    {
        isdialogue = true;//��ȭ���̴�
        Dialogue_TXT.text = "";
        Dialogue_Name_TXT.text = "";

        dialogue = p_dialogues;//��縦 �������� �־��ֱ�
        StartCoroutine(TypeWriter());
    }

    void Setting_Dir_UI(bool p_fleg)//��ȭUI�� Ű����� �Լ�.
    {
        Dialogue_Name_Ui.SetActive(p_fleg);
        Dialogue_Bar_Ui.SetActive(p_fleg);
    }
    IEnumerator TypeWriter()
    {
         Setting_Dir_UI(true);

         string t_replace_TXT = dialogue[line_count].context[context_count];//CSV������ ������ ���ڵ�(,)���� ġȯ��ų ����
         t_replace_TXT = t_replace_TXT.Replace("'",",");

         Dialogue_Name_TXT.text = dialogue[line_count].name;
         for(int i = 0; i < t_replace_TXT.Length; i++)
         {
              Dialogue_TXT.text += t_replace_TXT[i];//�ؽ�Ʈ�� �ѱ��ھ� ���������ν� �ϳ��� ��µǴ� ��ó�� ������.
             yield return new WaitForSeconds(textDelay);//������ �ο�
         }
         isnext = true;
         yield return null;
    }
    void End_dialogue()
    {
        //�ʱ�ȭ
        isdialogue = false;
        context_count = 0;
        line_count = 0;
        dialogue = null;
        isnext = false;
        Setting_Dir_UI(false);
    }
    //�̺�Ʈ ����.
    public void Event(int Start_Num,int End_Num)//��ȭâ�� ������ �Լ�.
    {
        Show_Dialogue(this.GetComponent<Interaction_Event>().GetDialogues(Start_Num,End_Num));
    }

    void Evnet_Gud()//�̺�Ʈ ������ ���� �Լ�.
    {
       E_Database.E_DataBase(day);
    }
    //�ð� ���� ����.
    void Time_Set()
    {
        Day_TXT.text = "���� Ű���� "+day+"��°";
        Time_TXT.text = c_Time.ToString();
    }
}
