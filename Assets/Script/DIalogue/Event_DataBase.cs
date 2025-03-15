using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Event_DataBase : MonoBehaviour
{

    //사용할 매니저들 가져오기
    private BTN_Manager B_Manager;
    //이벤트 관련
    bool is_oneday = false;//첫 대화 중복실행방지.
    private bool guide_on = false;//가이드 판 켜지기 중복실행 방지.
    //애니매이터
    [SerializeField] Animator dog_Anim;
    void Start()
    {//GetComponent는 본인에게 달린 스크립트만 찾을 수 있음.
        B_Manager = FindObjectOfType<BTN_Manager>();//그러므로 이 스크립트를 가진 객체에 접근해야함.
    }

    public void E_DataBase(int day)
    {
        switch(day){
            case  1 : 
                if(Event_Manager.Instance.c_Time == Event_Manager.Time.AM)//오전이라면
                {
                    dog_Anim.SetBool("Is_Hurt",Event_Manager.Instance.isdialogue);//첫번쨰 대화가 진행중이면 누워있는 모습,아니라면 멀쩡한 모습
                    if(!is_oneday)//한번도 실행 안됬다면
                    {   Event_Manager.Instance.Event(1,6);               
                        is_oneday = true;
                    }
                    if(Event_Manager.Instance.isdialogue == false&&guide_on == false)//대화가 끝난다면
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
