using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Mini_Game_One : MonoBehaviour
{
    //Ÿ�̸�
    private float c_Time = 60;//����ð�; �⺻�� 60��
    [SerializeField] Text Timer_Txt;//���ѽð� ǥ�� �ؽ�Ʈ

    //�Ÿ� ���(��������)
    public float score;//��å�� �Ÿ� ����.
    private float speed = 10;//��å �ӵ� ���� �⺻�� 1;
    [SerializeField] Text Score_TxT;

    //�� ���۽� ����κ�.
    bool is_Start;
    int Ready_Count = 3;//3�ʸ� �� ����.
    [SerializeField] Text Ready_TXT;

    // Start is called before the first frame update
    void Start()
    {
       is_Start = false;
       StartCoroutine(Ready_Coroutin());
    }

    // Update is called once per frame
    void Update()
    {
        if(is_Start)
        {
            Timer();
            Score_Check();
        }
    }

    void Timer()
    {
        c_Time -= Time.deltaTime;
        Timer_Txt.text = Mathf.Round(c_Time).ToString()+":00";//Mathf.Round(������)������ �Ҽ����� �����ϰ� �����ش�.
    }
    void Score_Check()//���� üũ
    {
        score += (speed  * Time.deltaTime);
        Score_TxT.text = "���� ��å�� �Ÿ�\n"+Mathf.Round(score).ToString()+"m";
    }

    IEnumerator Ready_Coroutin()//���� �ܰ� �ڷ�ƾ
    {
        while(!is_Start)//���� �ܰ��� ����.
        {
            if(Ready_Count > 0)//�ؾ��� ī��Ʈ �ٿ��� ���Ҵٸ�
            {
                Ready_TXT.text = Ready_Count.ToString();//ī��Ʈ �ٿ� ���� ����.
                Ready_Count--;
                yield return new WaitForSeconds(1f);//1�ʸ��� �ݺ�
            }
            else
            {
                Ready_TXT.text = "��å����!!";
                yield return new WaitForSeconds(1f); // �ؽ�Ʈ 1�� �����ֱ����� ��� ��� �����ְ�
                Ready_TXT.gameObject.SetActive(false); // �ؽ�Ʈ ����� ��
                is_Start = true;
            }
        }
    }
}
