using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTN_Manager : MonoBehaviour
{
     //Ʃ�丮�� �� ���̵� ��
    [SerializeField]
    GameObject Guide_IMG;
    //�ϰ����� ȭ��
    [SerializeField]
    GameObject Work_Select_Screen;
    // Update is called once per frame
    void Update()
    {
        
    }
      //��ư ����
    public void Guide_On_Off(bool p_fleg)
    {
         Guide_IMG.SetActive(p_fleg);//���̵� �̹��� ����.
    }
    public void ON_Selection_BTN(bool p_fleg)
    {
        Work_Select_Screen.SetActive(p_fleg);//���̵� �̹��� ����.
    }

}
