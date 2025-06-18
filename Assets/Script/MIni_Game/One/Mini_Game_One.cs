using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;
public class Mini_Game_One : MonoBehaviour
{
    //Ÿ�̸�
    private float c_Time = 30;//����ð�; �⺻�� 60��
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
    public float D_spawn_cooltime;//�뽬 �� ��Ÿ�� ���� �����ʱ�ȭ�� ���� ���� ��Ÿ�� ���庯��
    public float cooltime;//���� ��Ÿ�� ����


    //ü�¹�, �ν��� ����
    [SerializeField] Image Hp_Bar;//ü�¹�
    [SerializeField] Image Boost_Bar;//�ν�Ʈ ��
    [SerializeField] GameObject Boost_alarm_TXT;

    public float Dog_c_Hp = 100;//���� ü��
    public float Dog_c_Boost = 0;//���� �ν�Ʈ ������

    bool Ready_Boost = false;//�ν�Ʈ ���ɿ��� ����
    public bool isBoost;//�ν�Ʈ ������ �Ǻ��� ����

    //�ν�Ʈ ���� ����
    bool Boost_cool_Start= false;//�ν�Ʈ ������ ������ �˸� ����.
    float Boost_Up_Cnt = 0;//���ʸ��� �ν�Ʈ�������� ������ų���� �����ϴ� ����

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
        D_spawn_cooltime = spawn_cooltime;//�������� ���� ���� ��Ÿ�� ����.
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
            reward_TxT.text = "��å�� �Ÿ�: " + Mathf.Round(score).ToString() + "����";
            r_Time_Txt.text = "��å�� �ð�: " + Mathf.Round((30 - c_Time)) + "��";
            Game_Over_screen.SetActive(true);
            Boost_alarm_TXT.SetActive(false);//�˶� ���ֱ�   
            Time.timeScale = 0.01f;//�ð� ���߱�
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
        if (isBoost)//�뽬���̶��
        { spawn_cooltime = 0.3f; }
        else{spawn_cooltime = D_spawn_cooltime;}
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
        if (Ready_Boost)//�ν�Ʈ�� �����ϴٸ�
        {
            if (Input.GetKeyDown(KeyCode.R) && !isBoost)//R�� ������ �̹� �ν�Ʈ ���� �ƴ϶��
            {
                StartCoroutine(Boost());
                Boost_alarm_TXT.SetActive(false);//�˶� ���ֱ�   
            }
        }
        else if (Boost_cool_Start)//�ν�Ʈ ���� �����ؾ� �Ѵٸ�.(�ν�Ʈ�� �����Ѱ� �ƴ϶��)
        {
            Boost_Up_Cnt -= Time.deltaTime;//�ʴ� ����
            if (Boost_Up_Cnt <= 0)//1�ʸ��� �ν�Ʈ ������ �ø���
            {
                Dog_c_Boost += Boost_Speed;
                Debug.Log("����");
                Boost_Up_Cnt = 1;
                Update_Bar();
            }
            if (Dog_c_Boost >= 100 && !isBoost)//�ν�Ʈ �������� �������� �Ѿ���,�ν�Ʈ ���� �ƴ϶��.
            {
                Boost_alarm_TXT.SetActive(true);//�˶� ���ֱ�
                Debug.Log("�غ�");
                Boost_cool_Start = false;//�ν�Ʈ ������ ���� ����.
                Dog_c_Boost = 100;//100����
                Ready_Boost = true;//�ν�Ʈ ����
            }
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
                Boost_cool_Start = true;//��� �۾��� �����ٸ� �ν�Ʈ ������ ���� ����.
            }
        }
    }

    IEnumerator Boost()//�ν�Ʈ �� �Լ�
    {
        while (Dog_c_Boost > 0)//�� ���� �Ǳ� ������.
        {
            isBoost = true;//���� ��
            Dog_c_Boost -= 35f;//5�� ���ӵǵ���
            Debug.Log("����");
            Update_Bar();
            bG_Scrolling.speed = -3.0f;
            D_anim.SetFloat("Run_Speed", 5f);
            P_anim.SetFloat("P_Run_Speed", 2f);
            max_speed = 200;//���� ����
            min_speed = 190;//���� ����
            yield return new WaitForSeconds(1f);//1�ʸ��� �ݺ�
        }
        //�ν�Ʈ �������� ��� �������ٸ�.
        Dog_c_Boost = 0;//�ʱ�ȭ
        isBoost = false;//���� ����
        bG_Scrolling.speed = -0.2f;
        D_anim.SetFloat("Run_Speed", 1.5f);
        P_anim.SetFloat("P_Run_Speed", 1f);
        max_speed = 20;
        min_speed = 10;
        Boost_cool_Start = true;//��� �۾��� �����ٸ� �ν�Ʈ ������ ���� ����.
        Ready_Boost = false;//�����ؾ��ҋ��� �ν�ƮŰ�� �ȵǵ���,���� �ڷ�ƾ�� ������ �ڵ����� �� �ݺ����� �ǹǷ�. �ν�Ʈ�� ���Ե�.
    }
}
