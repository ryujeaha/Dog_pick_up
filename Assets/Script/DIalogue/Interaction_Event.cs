using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class Interaction_Event : MonoBehaviour
{
    [Tooltip("�� ��ũ��Ʈ�� �� ��ü���� ����� ��縦 �̾ƿ��� ����.")]//����
    //������ ����ȭ(Ŀ����Ŭ������ �ν����� â���� ���ų� �����ϱ� ���ؼ�)
    [SerializeField]DialogueEvent dialogue;
    public Dialogue[] GetDialogues(int Start_Num,int End_Num)//������ ���̽����� ��� �̾ƿ���.
    {
        //����ȭ�ؼ� line���� ������ �� �ֱ⶧���� ������ ���ΰ��� �޾Ƽ� �����ͺ��̽��� ���� �����͸� ������.
        dialogue.dialogues = Database_Manager.Instance.GetDialogues(Start_Num,End_Num);
        return dialogue.dialogues;
    }
}
