using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTN_Manager : MonoBehaviour
{
     //튜토리얼 용 가이드 판
    [SerializeField]
    GameObject Guide_IMG;
    //일과선택 화면
    [SerializeField]
    GameObject Work_Select_Screen;
    // Update is called once per frame
    void Update()
    {
        
    }
      //버튼 관련
    public void Guide_On_Off(bool p_fleg)
    {
         Guide_IMG.SetActive(p_fleg);//가이드 이미지 켜줌.
    }
    public void ON_Selection_BTN(bool p_fleg)
    {
        Work_Select_Screen.SetActive(p_fleg);//가이드 이미지 켜줌.
    }

}
