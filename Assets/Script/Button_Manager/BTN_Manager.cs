using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class BTN_Manager : MonoBehaviour
{
     //Ʃ�丮�� �� ���̵� ��
    [SerializeField]
    GameObject Guide_IMG;
    //�ϰ����� ȭ��
    [SerializeField]
    GameObject Work_Select_Screen;
    //�ϰ� ���� ȭ��
    [SerializeField]
    GameObject Work_Start_Screen;
    //�ϰ����� �ȳ� �ؽ�Ʈ
    [SerializeField]
    Text Work_Guide_TXT;

    Event_Manager.Working[] working;//�˻縦 ���ؼ� �迭�� ����.
    // Update is called once per frame

    void Start()
    {
        working = (Event_Manager.Working[])Enum.GetValues(typeof(Event_Manager.Working));//������ ���� ��ȯ.
        Guide_IMG = GameObject.Find("UI").transform.Find("Guide").gameObject;//��Ȱ��ȭ�Ǿ ã�� ���ؼ�
        Work_Select_Screen = GameObject.Find("Screen").transform.Find("Work_Select_Screen").gameObject;//��Ȱ��ȭ�Ǿ ã�� ���ؼ�
        Work_Start_Screen = GameObject.Find("Screen").transform.Find("Work_Start_Screen").gameObject;//��Ȱ��ȭ�Ǿ ã�� ���ؼ�
    }

    void Update()
    {
      Reset_Work_Start_Guide_TxT();
    }

    //��ư ����
    public void Guide_On_Off(bool p_fleg)
    {
         Guide_IMG.SetActive(p_fleg);//���̵� �̹��� ����.
    }
    public void ON_Selection_BTN(bool p_fleg)
    {
        Work_Select_Screen.SetActive(p_fleg);//�ϰ����� �̹��� ����.
    }

    public void On_Start_Work_Screen(bool p_fleg)
    {
      Work_Start_Screen.SetActive(p_fleg);//�ϰ� ���� ȭ�� ����.
      if(Event_Manager.Instance.c_Time == Event_Manager.Time.����)
      {
        for(int i = 0; i <working.Length; i++ )
        {
          if(i == Event_Manager.Instance.Work[0]-1)//���� ���������� ���ٸ�.(�������� ���� ������ȣ�� 1���� ���������Ƿ� ���ÿ��� -1)
          {
            Work_Guide_TXT.text = "���� ���� ���� �ϰ��� "+working[i].ToString()+"�Դϴ�.\n�����Ͻðڽ��ϱ�?";
          }
        }
      }
      else if(Event_Manager.Instance.c_Time == Event_Manager.Time.����)
      {
        for(int i = 0; i <working.Length; i++ )
        {
          if(i == Event_Manager.Instance.Work[1]-1)//���� ���������� ���ٸ�.(�������� ���� ������ȣ�� 1���� ���������Ƿ� ���ÿ��� -1)
          {
            Work_Guide_TXT.text = "���� ���� ���� �ϰ��� "+working[i].ToString()+"�Դϴ�.\n�����Ͻðڽ��ϱ�?";
          }
        }
      }
    }
    public void Start_Work()
    {
      if(Event_Manager.Instance.c_Time == Event_Manager.Time.����)//�����ϰ� ������
      {
          if(Event_Manager.Instance.Mini_Game_SceneName.ContainsKey(Event_Manager.Instance.Work[0]))
          { 
            Save_Manager.Instance.Save_On();
            Scene_Manager.Instance.Change_Scene(Event_Manager.Instance.Mini_Game_SceneName[Event_Manager.Instance.Work[0]]);
          }
          else{
            ;
          }
      }
      else if(Event_Manager.Instance.c_Time == Event_Manager.Time.����)//���� �ϰ� ����
      {
        if(Event_Manager.Instance.Mini_Game_SceneName.ContainsKey(Event_Manager.Instance.Work[1]))
        {
           Save_Manager.Instance.Save_On();
          Scene_Manager.Instance.Change_Scene(Event_Manager.Instance.Mini_Game_SceneName[Event_Manager.Instance.Work[1]]);
        }
        else{
          ;
        }
      }  
      
    }

    void Reset_Work_Start_Guide_TxT()
    {
      if(Event_Manager.Instance.Work[0] == 0||Event_Manager.Instance.Work[1] == 0)//�� ������
      {
        //Debug.Log("����");
        Work_Guide_TXT.text = "���� �ϰ��� ��� �������� �ʾҽ��ϴ�.";
      }
    }
}
