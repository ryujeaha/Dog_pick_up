using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Game_One_Patten : MonoBehaviour
{
    //���� �̵�����
    public float y_pos;//������ ��ġ
    [SerializeField] float speed;//�ӵ� ��
    [SerializeField] float damage;//���� ���ϸ����� ����� ��.

    Mini_Game_One mini_Game_One;//�Ŵ��� ��������

    void Start()
    {
        mini_Game_One = FindAnyObjectByType<Mini_Game_One>();
    }

    void Update()
    {
        if (mini_Game_One.isBoost)//�뽬���̶�� �����ӵ���
        {
             transform.Translate(new Vector3(1, 0, 0) * (speed * 3)* Time.deltaTime);//�ش� ��������,�ӵ�����ŭ,������ �ӵ��� ���ؼ� deltaTime ������.
        }
        else//�뽬�� �ƴҋ��� ����� �ӵ��� 
        {
            transform.Translate(new Vector3(1, 0, 0) * speed * Time.deltaTime);//�ش� ��������,�ӵ�����ŭ,������ �ӵ��� ���ؼ� deltaTime ������.
        }
       
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Destroy(gameObject);//����
            if (mini_Game_One.isBoost != true)
            {
                mini_Game_One.Dog_c_Hp -= damage;//����� ����.
                mini_Game_One.Dog_c_Boost -= (damage / 2);
                mini_Game_One.Update_Bar();
            }
        }
     
    }

    private void OnBecameInvisible()//ȭ������� ��ü�� ������ ȣ��Ǵ� �Լ�
    {
        Destroy(gameObject);//����
    }
}
