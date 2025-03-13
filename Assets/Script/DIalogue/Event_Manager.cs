using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
public class Event_Manager : MonoBehaviour
{

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
    private int day = 1;//���� ��¥
    public int Time = 2;//�ൿ��
    [SerializeField]Text Day_TXT;//��¥
    [SerializeField] Text Time_TXT;//���� ����
    Dialogue[] dialogue;

    //Ʃ�丮�� �� ���̵� ��
    [SerializeField]
    GameObject Guide_IMG;
    private bool guide_on = false;

    //Event������ ���ִ� �����ͺ��̽� ����
    Event_DataBase E_Database;



    void Start()
    {
        E_Database = GetComponent<Event_DataBase>();
    }

    void Update()
    {
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
                if(Input.GetKeyDown(KeyCode.Space))
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
        isdialogue = false;
        context_count = 0;
        line_count = 0;
        dialogue = null;
        isnext = false;
        Setting_Dir_UI(false);
    }
    public void Event(int Start_Num,int End_Num)//��ȭâ�� ������ �Լ�.
    {
        Show_Dialogue(this.GetComponent<Interaction_Event>().GetDialogues(Start_Num,End_Num));
    }

    void Evnet_Gud()//�̺�Ʈ ������ ���� �Լ�.(�ϵ��ڵ�)
    {
       E_Database.E_DataBase(day);
    }
    public void Guide_Open()
    {
        if(guide_on == false)
        {
            Guide_IMG.SetActive(true);//���̵� �̹��� ����.
        }
    }
    public void Guide_Off()
    {
        Guide_IMG.SetActive(false);//���̵� �̹��� ����.
    }
}
