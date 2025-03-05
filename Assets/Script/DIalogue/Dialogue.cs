using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]//커스텀 클래스를 인스펙터 창에서 수정하기 위해서 직렬화해주는 명령어.
public class Dialogue//사용자 정의 클래스로 사용할 것이기에 유니티에서 상속받는 부분인 MonoBehaviour을 뺴준다.
{
    [Tooltip("대사 치는 캐릭터 이름")]//인스펙터 창에서 툴팁을띄우는 명령어.
    public string name;
     [Tooltip("대사 내용")]
    public string[] context;//대사는 여러번 말할 수 있으므로 배열로 선언.
}
[System.Serializable]
public class DialogueEvent//여러명의 캐릭터가 Dialogue클래스를 사용할 수 있도록 Dialogue를 배열로 만들어 주는 클래스
{
    public string E_name;//이벤트의 이름

    public Vector2 line;//대사를 추출해낼 범위 x~y
    public Dialogue[] dialogues;
}
