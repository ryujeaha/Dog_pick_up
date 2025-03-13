using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Event_DataBase : MonoBehaviour
{

    //이벤트 매니저 가져오기
    private Event_Manager E_Manager;

    //이벤트 관련
    bool is_oneday = false;//첫 대화 중복실행방지.
    private bool guide_on = false;//가이드 판 켜지기 중복실행 방지.
    //애니매이터
    [SerializeField] Animator dog_Anim;
    void Start()
    {
        E_Manager = GetComponent<Event_Manager>();
    }

    public void E_DataBase(int day)
    {
        switch(day){
            case  1 : 
                dog_Anim.SetBool("Is_Hurt",E_Manager.isdialogue);//첫번쨰 대화가 진행중이면 누워있는 모습,아니라면 멀쩡한 모습
                if(!is_oneday)//한번도 실행 안됬다면
                {   E_Manager.Event(1,6);               
                    is_oneday = true;
                }
                if(E_Manager.isdialogue == false&&guide_on == false)//대화가 끝난다면
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
