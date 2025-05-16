using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;
public class Mini_Game_One : MonoBehaviour
{
    //Ÿ�̸�
    private float c_Time = 60;//����ð�; �⺻�� 60��
    [SerializeField] Text Timer_Txt;//���ѽð� ǥ�� �ؽ�Ʈ

    //�Ÿ� ���(��������)
    public float score;//��å�� �Ÿ� ����.
    private float max_speed = 20;//��å���� ������ �ӵ� ���� �ִ밪;
    private float min_speed = 10;
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
    [SerializeField] Image Hp_Bar;//ü�¹�
    [SerializeField] Image Boost_Bar;//�ν�Ʈ ��

    public float Dog_c_Hp = 100;//���� ü��
    public float Dog_c_Boost = 0;//���� �ν�Ʈ ������
   public bool isBoost;//�ν�Ʈ ������ �Ǻ��� ����
    
    float Dog_Max_Hp = 100;//�ִ� ü��
    float Dog_Max_Boost = 100;//�ν�Ʈ �ִ�ġ.

    
    [SerializeField] float Boost_Speed;//�����Ǵ� �ν��� ������ ��

    //���� ���� ����
    [SerializeField] GameObject Game_Over_screen;
    [SerializeField] Text reward_TxT;//������ ������ �ؽ�Ʈ
    [SerializeField] Text r_Time_Txt;//�޸��ð��� ������ �ؽ�Ʈ;
    [SerializeField] Text Stat_Txt;//���� �߰�ġ�� ������ �ؽ�Ʈ(���� ����)

    //�ִϸ����� ����
    [SerializeField] Animator P_anim;
    [SerializeField] Animator D_anim;

    //���� ��ũ��Ʈ ��������
    BG_Scrolling bG_Scrolling;//�ν�Ʈ ����,���� ������ ���� ����̵� ��ũ��Ʈ ��������

    // Start is called before the first frame update
    void Start()
    {
        bG_Scrolling = FindAnyObjectByType<BG_Scrolling>();
        is_Start = false;
        Update_Bar();
        StartCoroutine(Ready_Coroutin());
    }

    // Update is called once per frame
    void Update()
    {
        if (is_Start)
        {
            Timer();
            Score_Check();
            Boost_On();
            Jump();
            SpawnCool();
            Game_Finish();
        }
    }


    public void Update_Bar()
    {
        Hp_Bar.fillAmount = (Dog_c_Hp) / (Dog_Max_Hp);//HP�� �� ä���.
        Boost_Bar.fillAmount = (Dog_c_Boost) / (Dog_Max_Boost);
    }

    public void Game_Finish()
    {
        if (c_Time <= 0 || Dog_c_Hp <= 0)
        {
            is_Start = false;
            reward_TxT.text = "��å�� �Ÿ�: " + score + "����";
            r_Time_Txt.text = "��å�� �ð�: " + ((60) - c_Time) + "��";
            Game_Over_screen.SetActive(true);

            Time.timeScale = 0.001f;//�ð� ���߱�
        }
    }
    void Timer()
    {
        c_Time -= Time.deltaTime;
        Timer_Txt.text = Mathf.Round(c_Time).ToString() + ":00";//Mathf.Round(������)������ �Ҽ����� �����ϰ� �����ش�.
    }
    void Score_Check()//���� üũ
    {
        score += (Random.Range(min_speed,max_speed) * Time.deltaTime);
        Score_TxT.text = "���� ��å�� �Ÿ�\n" + Mathf.Round(score).ToString() + "m";
    }

    void Jump()//���� �Լ�
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RaycastHit2D hit = Physics2D.Raycast(G_check_box.transform.position, Vector2.down, 0.2f);//���� ��ü�� ������ ������ ���� hit�� ������ ������ġ,����,����
            if (hit.collider != null && hit.collider.CompareTag("Floor"))
            {
                my_Rig.AddForce(Vector2.up * jump_Porce, ForceMode2D.Impulse);
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
        GameObject Patten = Instantiate(Pattens[P_Num], new Vector2(P_Spawns.transform.position.x + 0.5f, Pattens[P_Num].GetComponent<Game_One_Patten>().y_pos), Quaternion.identity);
    }


void Boost_On()
{
    if (Dog_c_Boost >= 100&&!isBoost)
    {
            Debug.Log("����");
        StartCoroutine(Boost());
    }
}

    IEnumerator Ready_Coroutin()//���� �ܰ� �ڷ�ƾ
    {
        while (!is_Start)//���� �ܰ��� ����.
        {
            if (Ready_Count > 0)//�ؾ��� ī��Ʈ �ٿ��� ���Ҵٸ�
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
                bG_Scrolling.speed = -0.2f;//��� �����̱�
                P_anim.SetBool("Is_Work", true);
                P_anim.SetBool("Is_Idle", false);
                D_anim.SetBool("Is_RUN", true);
                StartCoroutine("Boost_Up");//��� �۾��� �����ٸ� �ν�Ʈ ������ ���� ����.
            }
        }
    }
    IEnumerator Boost_Up()//�ν�Ʈ ���� �ڷ�ƾ 12.5��1��
    {
        if (!isBoost)//�ν�Ʈ���� �ƴҶ���
        {
            while (Dog_c_Boost < 100)//100�� �Ǹ� ���ߵ���
            {
                Dog_c_Boost += Boost_Speed;
                Debug.Log("����");
                Update_Bar();
                yield return new WaitForSeconds(1f);//1�ʸ��� �ݺ�
            }
        }
      
    }
    IEnumerator Boost()//�ν�Ʈ �� �ڷ�ƾ
    {
        StopCoroutine("Boost_Up");
        while (Dog_c_Boost > 0)//�� ���� �Ǳ� ������.
        {
            isBoost = true;//���� ��
            Dog_c_Boost -= 20f;//5�� ���ӵǵ���
            Debug.Log("����");
            Update_Bar();
            bG_Scrolling.speed = -5.0f;
            D_anim.SetFloat("Run_Speed", 5f);
            max_speed = 200;
            min_speed = 190;
            yield return new WaitForSeconds(1f);//1�ʸ��� �ݺ�
        }
            //�ν�Ʈ �������� ��� �������ٸ�.
            isBoost = false;//���� ����
            bG_Scrolling.speed = -0.2f;
            D_anim.SetFloat("Run_Speed", 2f);
            max_speed = 20;
            min_speed = 10;
            StartCoroutine("Boost_Up");//��� �۾��� �����ٸ� �ν�Ʈ ������ ���� ����
    }
}
