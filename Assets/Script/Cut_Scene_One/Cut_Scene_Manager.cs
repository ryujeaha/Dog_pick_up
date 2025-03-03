using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Cut_Scene_Manager : MonoBehaviour
{
    public float One_Scene_Time = 20f;//첫번쨰 씬의 재생시간.
    public string Scene_Name;
    public BG_Scrolling bG_Scrolling;//하늘을 멈추게 하기위한 스크립트 가져오기.
    bool is_change = false;//화면 암전을 시도했는가를 판단하는 함수(한번만 되야함으로.) 
    void Start()
    {
        bG_Scrolling = FindObjectOfType<BG_Scrolling>();//찾기
    }

    // Update is called once per frame
    void Update()
    {
        Timer();//시간 세기.
    }

    void Timer()
    {
        One_Scene_Time -= Time.deltaTime;
        if(One_Scene_Time <= 14)//구름을 멈춰야 한다면.
        {
            bG_Scrolling.speed = 0f;
        }
        if(One_Scene_Time <= 5)
        {
            if(is_change == false)
            {
                Scene_Manager.Instance.Change_Scene(Scene_Name);
                is_change = true;
            }
            
        }
    }
}
