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

     //���� ����
    [SerializeField] Rigidbody2D my_Rig;//������ ������ �ٵ� ã���ֱ�
    public float jump_Porce;//���� �� ���� ����
    [SerializeField] GameObject G_check_box;
    bool isGrounded;//���� üũ ����.

    //���� ���� ����.
    [SerializeField] GameObject[] Pattens;//������ ���ϵ�.
    public GameObject P_Spawns;//��������Ʈ�� ���� ����
    public float spawn_cooltime;//��Ÿ�� ���� ����
    public float cooltime;//���� ��Ÿ�� ����


    //ü�¹�, �ν��� ����
    [SerializeField] Slider Hp_Bar;//ü�¹�
    float Dog_Hp = 100;
    [SerializeField] Slider Boost_Bar;//�ν�Ʈ ��
    float Dog_Boost = 0;
    // Start is called before the first frame update
    void Start()
    {
       is_Start = false;
       StartCoroutine(Ready_Coroutin());
    }

    // Update is called once per frame
    void Update()
    {
        if (is_Start)
        {
            Timer();
            Score_Check();
            Jump();
            SpawnCool();
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

        void Jump()//���� �Լ�
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("����");
            RaycastHit2D hit = Physics2D.Raycast(G_check_box.transform.position,Vector2.down,0.2f);//���� ��ü�� ������ ������ ���� hit�� ������ ������ġ,����,����
            if(hit.collider != null && hit.collider.CompareTag("Floor"))
            {
                Debug.Log("�߻�!");
                my_Rig.AddForce(Vector2.up * jump_Porce, ForceMode2D.Impulse);
            }
            else if(hit.collider == null)
            {
                Debug.Log("���� �ȵ�");
            }
        }
    }

    void SpawnCool()//���� ���� ��Ÿ�� ���� �Լ�.
    {
        cooltime -= Time.deltaTime;//���� ��Ÿ�� ����
        if (cooltime <= 0)//���� ��Ÿ���� ��ŸŸ�ӿ������� �� 0�ʰ� �Ǿ��ٸ�
        {
            Spawn_Patten();//�� ��ȯ
            cooltime = spawn_cooltime;//�� ���� �ʱ�ȭ <= �ٽ� 0�̵Ǹ� �� ����
        }
    }
    void Spawn_Patten()
    {
        int P_Num = Random.Range(0, Pattens.Length);//0���� ���� ���ϵ��� �� ��ȣ�߿��� �ϳ��� �����ϰ� ����.
        GameObject Patten = Instantiate(Pattens[P_Num], new Vector2(P_Spawns.transform.position.x + 0.5f, Pattens[P_Num].GetComponent<Game_One_Patten>().y_pos),Quaternion.identity);
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
