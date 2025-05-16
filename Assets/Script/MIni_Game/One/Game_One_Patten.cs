using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Game_One_Patten : MonoBehaviour
{
    //패턴 이동관련
    public float y_pos;//생성될 위치
    [SerializeField] float speed;//속도 값
    [SerializeField] float damage;//각자 패턴마다의 대미지 값.

    Mini_Game_One mini_Game_One;//매니저 가져오기

    void Start()
    {
        mini_Game_One = FindAnyObjectByType<Mini_Game_One>();
    }

    void Update()
    {
        transform.Translate(new Vector3(1, 0, 0) * speed * Time.deltaTime);//해당 방향으로,속도값만큼,일정한 속도를 위해서 deltaTime 곱해줌.
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Destroy(gameObject);//삭제
            if (mini_Game_One.isBoost != true)
            {
                mini_Game_One.Dog_c_Hp -= damage;//대미지 연산.
                mini_Game_One.Dog_c_Boost -= (damage / 2);
                mini_Game_One.Update_Bar();
            }
        }
     
    }

    private void OnBecameInvisible()//화면밖으로 객체가 나가면 호출되는 함수
    {
        Destroy(gameObject);//삭제
    }
}
