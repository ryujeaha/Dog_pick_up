using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class BTN_Manager : MonoBehaviour
{
     //튜토리얼 용 가이드 판
    [SerializeField]
    GameObject Guide_IMG;
    //일과선택 화면
    [SerializeField]
    GameObject Work_Select_Screen;
    //일과 시작 화면
    [SerializeField]
    GameObject Work_Start_Screen;
    //일과시작 안내 텍스트
    [SerializeField]
    Text Work_Guide_TXT;

    Event_Manager.Working[] working;//검사를 위해서 배열로 만듬.
    // Update is called once per frame

    void Start()
    {
        working = (Event_Manager.Working[])Enum.GetValues(typeof(Event_Manager.Working));//포문을 위한 변환.
        Guide_IMG = GameObject.Find("UI").transform.Find("Guide").gameObject;//비활성화되어도 찾기 위해서
        Work_Select_Screen = GameObject.Find("Screen").transform.Find("Work_Select_Screen").gameObject;//비활성화되어도 찾기 위해서
        Work_Start_Screen = GameObject.Find("Screen").transform.Find("Work_Start_Screen").gameObject;//비활성화되어도 찾기 위해서
    }

    void Update()
    {
      Reset_Work_Start_Guide_TxT();
    }

    //버튼 관련
    public void Guide_On_Off(bool p_fleg)
    {
         Guide_IMG.SetActive(p_fleg);//가이드 이미지 켜줌.
    }
    public void ON_Selection_BTN(bool p_fleg)
    {
        Work_Select_Screen.SetActive(p_fleg);//일과선택 이미지 켜줌.
    }

    public void On_Start_Work_Screen(bool p_fleg)
    {
      Work_Start_Screen.SetActive(p_fleg);//일과 시작 화면 켜줌.
      if(Event_Manager.Instance.c_Time == Event_Manager.Time.오전)
      {
        for(int i = 0; i <working.Length; i++ )
        {
          if(i == Event_Manager.Instance.Work[0]-1)//현재 오전일정과 같다면.(가독성을 위해 고유번호를 1부터 시작했으므로 계산시에는 -1)
          {
            Work_Guide_TXT.text = "현재 고르신 오전 일과는 "+working[i].ToString()+"입니다.\n실행하시겠습니까?";
          }
        }
      }
      else if(Event_Manager.Instance.c_Time == Event_Manager.Time.오후)
      {
        for(int i = 0; i <working.Length; i++ )
        {
          if(i == Event_Manager.Instance.Work[1]-1)//현재 오후일정과 같다면.(가독성을 위해 고유번호를 1부터 시작했으므로 계산시에는 -1)
          {
            Work_Guide_TXT.text = "현재 고르신 오후 일과는 "+working[i].ToString()+"입니다.\n실행하시겠습니까?";
          }
        }
      }
    }
    public void Start_Work()
    {
      if(Event_Manager.Instance.c_Time == Event_Manager.Time.오전)//오전일과 실행행
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
      else if(Event_Manager.Instance.c_Time == Event_Manager.Time.오후)//오후 일과 실행
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
      if(Event_Manager.Instance.Work[0] == 0||Event_Manager.Instance.Work[1] == 0)//다 없으면
      {
        //Debug.Log("실행");
        Work_Guide_TXT.text = "아직 일과가 모두 정해지지 않았습니다.";
      }
    }
}
