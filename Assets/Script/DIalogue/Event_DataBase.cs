using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Event_DataBase : MonoBehaviour
{

    //�̺�Ʈ �Ŵ��� ��������
    private Event_Manager E_Manager;

    //�̺�Ʈ ����
    bool is_oneday = false;//ù ��ȭ �ߺ��������.
    private bool guide_on = false;//���̵� �� ������ �ߺ����� ����.
    //�ִϸ�����
    [SerializeField] Animator dog_Anim;
    void Start()
    {
        E_Manager = GetComponent<Event_Manager>();
    }

    public void E_DataBase(int day)
    {
        switch(day){
            case  1 : 
                dog_Anim.SetBool("Is_Hurt",E_Manager.isdialogue);//ù���� ��ȭ�� �������̸� �����ִ� ���,�ƴ϶�� ������ ���
                if(!is_oneday)//�ѹ��� ���� �ȉ�ٸ�
                {   E_Manager.Event(1,6);               
                    is_oneday = true;
                }
                if(E_Manager.isdialogue == false&&guide_on == false)//��ȭ�� �����ٸ�
                {
                    E_Manager.Guide_Open();
                    guide_on = true;
                }  
                break;
            default:
                break;        
        }
    }

    
}
