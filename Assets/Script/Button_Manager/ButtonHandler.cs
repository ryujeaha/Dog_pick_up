using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;//아마도 Action을 쓰기위해서?

public class ButtonHandler : MonoBehaviour,IPointerClickHandler
{
    //버튼 클릭을 구현하기 위한 IPointerClickHandler를 상속받는다. 
    // 비슷한 역할로 키다운 구현을 위한 IPointerDownHandler,키업 구현을 위한 IPointerUpHandler도 있다.
    //public Action OnClicked;//버튼 클릭시 명령을 실행시켜줄 대리자.


    [SerializeField]
    int My_Work_Num = 0;//버튼이 가지고 있는 각자의 넘버
    bool is_Selec = false;
    Vector3 ori_Pos;//돌아가야할 버튼의 원래 위치.
    
  
    private void Start() {
        ori_Pos = this.gameObject.transform.position;//원래 위치 기억
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)//마우스 왼쪽 버튼으로 눌렀다면
        {
            if(Event_Manager.Instance.Work[0] == 0&&is_Selec == false)//오전 일과를 고르는 경우이며 첫번째 일과가 정해지지 않았을경우
            {
                Event_Manager.Instance.Work[0] = My_Work_Num;
                this.gameObject.transform.localPosition = new Vector3(-240,100,0);//선택된거는 위치를 오전으로
                is_Selec = true;
            }
            else if(Event_Manager.Instance.Work[1] == 0&&is_Selec == false)//오후 일과를 고르는 경우이며 두번째 일과가 정해지지 않았을경우
            {
                Event_Manager.Instance.Work[1] = My_Work_Num;
                this.gameObject.transform.localPosition = new Vector3(240,100,0);//선택된거는 위치를 오후로
                 is_Selec = true;
            }
        }
        else if(eventData.button == PointerEventData.InputButton.Right)//마우스 오른쪽 버튼으로 눌렀다면
        {
            if(this.My_Work_Num == Event_Manager.Instance.Work[1]&&is_Selec == true)//지우려는 버튼의 번호가 이미 오후에 있다면
            {
                Event_Manager.Instance.Work[1] = 0;//초기화 
                this.gameObject.transform.position = ori_Pos;//선택된거는 위치를 원래대로
                is_Selec = false;
            }
            else if(this.My_Work_Num == Event_Manager.Instance.Work[0]&&is_Selec == true)//오전 일과를 지우고 싶다면
            {
                Event_Manager.Instance.Work[0] = 0;
                this.gameObject.transform.position = ori_Pos;//선택된거는 위치를 오후로
                is_Selec = false;
            }
        }
    }
}
