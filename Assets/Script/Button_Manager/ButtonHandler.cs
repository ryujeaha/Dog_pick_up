using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;//�Ƹ��� Action�� �������ؼ�?

public class ButtonHandler : MonoBehaviour,IPointerClickHandler
{
    //��ư Ŭ���� �����ϱ� ���� IPointerClickHandler�� ��ӹ޴´�. 
    // ����� ���ҷ� Ű�ٿ� ������ ���� IPointerDownHandler,Ű�� ������ ���� IPointerUpHandler�� �ִ�.
    //public Action OnClicked;//��ư Ŭ���� ����� ��������� �븮��.


    [SerializeField]
    int My_Work_Num = 0;//��ư�� ������ �ִ� ������ �ѹ�
    bool is_Selec = false;
    Vector3 ori_Pos;//���ư����� ��ư�� ���� ��ġ.
    
  
    private void Start() {
        ori_Pos = this.gameObject.transform.position;//���� ��ġ ���
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)//���콺 ���� ��ư���� �����ٸ�
        {
            if(Event_Manager.Instance.Work[0] == 0&&is_Selec == false)//���� �ϰ��� ���� ����̸� ù��° �ϰ��� �������� �ʾ������
            {
                Event_Manager.Instance.Work[0] = My_Work_Num;
                this.gameObject.transform.localPosition = new Vector3(-240,100,0);//���õȰŴ� ��ġ�� ��������
                is_Selec = true;
            }
            else if(Event_Manager.Instance.Work[1] == 0&&is_Selec == false)//���� �ϰ��� ���� ����̸� �ι�° �ϰ��� �������� �ʾ������
            {
                Event_Manager.Instance.Work[1] = My_Work_Num;
                this.gameObject.transform.localPosition = new Vector3(240,100,0);//���õȰŴ� ��ġ�� ���ķ�
                 is_Selec = true;
            }
        }
        else if(eventData.button == PointerEventData.InputButton.Right)//���콺 ������ ��ư���� �����ٸ�
        {
            if(this.My_Work_Num == Event_Manager.Instance.Work[1]&&is_Selec == true)//������� ��ư�� ��ȣ�� �̹� ���Ŀ� �ִٸ�
            {
                Event_Manager.Instance.Work[1] = 0;//�ʱ�ȭ 
                this.gameObject.transform.position = ori_Pos;//���õȰŴ� ��ġ�� �������
                is_Selec = false;
            }
            else if(this.My_Work_Num == Event_Manager.Instance.Work[0]&&is_Selec == true)//���� �ϰ��� ����� �ʹٸ�
            {
                Event_Manager.Instance.Work[0] = 0;
                this.gameObject.transform.position = ori_Pos;//���õȰŴ� ��ġ�� ���ķ�
                is_Selec = false;
            }
        }
    }
}
