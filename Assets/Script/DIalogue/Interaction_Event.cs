using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class Interaction_Event : MonoBehaviour
{
    [Tooltip("이 스크립트가 들어간 객체에서 출력할 대사를 뽑아오는 역할.")]//설명
    //데이터 직렬화(커스텀클래스를 인스펙터 창에서 보거나 수정하기 위해서)
    [SerializeField]DialogueEvent dialogue;
    public Dialogue[] GetDialogues(int Start_Num,int End_Num)//데이터 베이스에서 대사 뽑아오기.
    {
        //직렬화해서 line값을 수정할 수 있기때문에 수정된 라인값을 받아서 데이터베이스를 통해 데이터를 가져옴.
        dialogue.dialogues = Database_Manager.Instance.GetDialogues(Start_Num,End_Num);
        return dialogue.dialogues;
    }
}
