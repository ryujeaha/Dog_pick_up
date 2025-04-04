using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save_Manager : MonoBehaviour
{
  public static Save_Manager Instance;

    void Awake()
    {
        if(Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }else if(Instance != this){
           Destroy(gameObject);
        }
    }
    public Event_Manager.Time c_Time;//현재시간
    private int day = 1;//현재 날짜 기본값 1
    public int[] Work = new int[2] {0,0};//오전과 오후의 일과데이터를 담을 배열
    // Start is called before the first frame update
   public void Save_On()
   {
    c_Time = Event_Manager.Instance.c_Time+1;//현재 시간보다 한 칸 이후를 저장.
    day = Event_Manager.Instance.day;//현재 날짜 저장
    Work[0] = Event_Manager.Instance.Work[0];//오후 일과정보 저장
    Work[1] = Event_Manager.Instance.Work[1];//오후 일과정보 저장
   }

   public void Load_On()
   {
     Event_Manager.Instance.c_Time = c_Time;//현재 시간 로드
     Event_Manager.Instance.day = day;//현재 날짜 로드
     Event_Manager.Instance.Work[0] = Work[0];//오후 일과정보 로드
     Event_Manager.Instance.Work[1] = Work[1];//오후 일과정보 로드
   }
}
