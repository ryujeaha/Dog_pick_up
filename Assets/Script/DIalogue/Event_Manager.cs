using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Event_Manager : MonoBehaviour
{
    //���̾˷α� ����
    [SerializeField] GameObject Dialogue_Bar_Ui;
     [SerializeField] GameObject Dialogue_Name_Ui;
    [SerializeField]Text Dialogue_TXT;
    [SerializeField] Text Dialogue_Name_TXT;

    bool isdialogue = false;//��ȭ���� ��� true;
    bool isnext = false;// Ư�� Ű �Է� ���.

    int line_count = 0;//��ȭī��Ʈ
    int context_count = 0;//��� ī��Ʈ

    //��¥ ����
    private int day = 1;//���� ��¥
    public int Time = 2;//�ൿ��
    [SerializeField]Text Day_TXT;//��¥
    [SerializeField] Text Time_TXT;//���� ����
    Dialogue[] dialogue;

    //�̺�Ʈ ����
    bool is_oneday = false;//ù ��ȭ �ߺ��������.

    //�ִϸ�����
    [SerializeField] Animator dog_Anim;
    
    // Update is called once per frame
    void Update()
    {
        Evnet_Gud();
        //�������� �Ѿ���� Ű üũ �ϴ� �κ�.
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
    public void Event(int Start_Num,int End_Num)//��ȭâ�� ������ �Լ�.
    {
        Show_Dialogue(this.GetComponent<Interaction_Event>().GetDialogues(Start_Num,End_Num));
    }

    void Evnet_Gud()//�̺�Ʈ ������ ���� �Լ�.(�ϵ��ڵ�)
    {
        if(day == 1)//1���� �̺�Ʈ
        {   
            dog_Anim.SetBool("Is_Hurt",isdialogue);//ù���� ��ȭ�� �������̸� �����ִ� ���,�ƴ϶�� ������ ���
            if(!is_oneday)//�ѹ��� ���� �ȉ�ٸ�
            {   Event(1,6);               
                is_oneday = true;
            }
        }
    }
}
