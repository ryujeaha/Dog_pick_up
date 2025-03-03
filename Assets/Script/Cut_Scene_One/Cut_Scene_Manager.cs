using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Cut_Scene_Manager : MonoBehaviour
{
    public float One_Scene_Time = 20f;//ù���� ���� ����ð�.
    public string Scene_Name;
    public BG_Scrolling bG_Scrolling;//�ϴ��� ���߰� �ϱ����� ��ũ��Ʈ ��������.
    bool is_change = false;//ȭ�� ������ �õ��ߴ°��� �Ǵ��ϴ� �Լ�(�ѹ��� �Ǿ�������.) 
    void Start()
    {
        bG_Scrolling = FindObjectOfType<BG_Scrolling>();//ã��
    }

    // Update is called once per frame
    void Update()
    {
        Timer();//�ð� ����.
    }

    void Timer()
    {
        One_Scene_Time -= Time.deltaTime;
        if(One_Scene_Time <= 14)//������ ����� �Ѵٸ�.
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
