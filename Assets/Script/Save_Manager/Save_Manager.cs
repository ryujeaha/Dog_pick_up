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
    public Event_Manager.Time c_Time;//����ð�
    private int day = 1;//���� ��¥ �⺻�� 1
    public int[] Work = new int[2] {0,0};//������ ������ �ϰ������͸� ���� �迭
    // Start is called before the first frame update
   public void Save_On()
   {
    c_Time = Event_Manager.Instance.c_Time+1;//���� �ð����� �� ĭ ���ĸ� ����.
    day = Event_Manager.Instance.day;//���� ��¥ ����
    Work[0] = Event_Manager.Instance.Work[0];//���� �ϰ����� ����
    Work[1] = Event_Manager.Instance.Work[1];//���� �ϰ����� ����
   }

   public void Load_On()
   {
     Event_Manager.Instance.c_Time = c_Time;//���� �ð� �ε�
     Event_Manager.Instance.day = day;//���� ��¥ �ε�
     Event_Manager.Instance.Work[0] = Work[0];//���� �ϰ����� �ε�
     Event_Manager.Instance.Work[1] = Work[1];//���� �ϰ����� �ε�
   }
}
