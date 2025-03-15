using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Event_DataBase : MonoBehaviour
{

    //����� �Ŵ����� ��������
    private BTN_Manager B_Manager;
    //�̺�Ʈ ����
    bool is_oneday = false;//ù ��ȭ �ߺ��������.
    private bool guide_on = false;//���̵� �� ������ �ߺ����� ����.
    //�ִϸ�����
    [SerializeField] Animator dog_Anim;
    void Start()
    {//GetComponent�� ���ο��� �޸� ��ũ��Ʈ�� ã�� �� ����.
        B_Manager = FindObjectOfType<BTN_Manager>();//�׷��Ƿ� �� ��ũ��Ʈ�� ���� ��ü�� �����ؾ���.
    }

    public void E_DataBase(int day)
    {
        switch(day){
            case  1 : 
                if(Event_Manager.Instance.c_Time == Event_Manager.Time.AM)//�����̶��
                {
                    dog_Anim.SetBool("Is_Hurt",Event_Manager.Instance.isdialogue);//ù���� ��ȭ�� �������̸� �����ִ� ���,�ƴ϶�� ������ ���
                    if(!is_oneday)//�ѹ��� ���� �ȉ�ٸ�
                    {   Event_Manager.Instance.Event(1,6);               
                        is_oneday = true;
                    }
                    if(Event_Manager.Instance.isdialogue == false&&guide_on == false)//��ȭ�� �����ٸ�
                    {
                        B_Manager.Guide_On_Off(true);
                        guide_on = true;
                    } 
                }
                break;
            default:
                break;        
        }
    }

    
}
