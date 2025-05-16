using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Game_One_Patten : MonoBehaviour
{
    //���� �̵�����
    public float y_pos;//������ ��ġ
    [SerializeField] float speed;//�ӵ� ��

    Mini_Game_One mini_Game_One;//�Ŵ��� ��������

    void Start()
    {
        mini_Game_One = FindAnyObjectByType<Mini_Game_One>();
    }

    void Update()
    {
        transform.Translate(new Vector3(1, 0, 0) * speed * Time.deltaTime);//�ش� ��������,�ӵ�����ŭ,������ �ӵ��� ���ؼ� deltaTime ������.
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);//����
    }

    private void OnBecameInvisible()//ȭ������� ��ü�� ������ ȣ��Ǵ� �Լ�
    {
        Destroy(gameObject);//����
    }
}
