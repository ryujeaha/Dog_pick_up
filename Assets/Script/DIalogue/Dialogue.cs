using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]//Ŀ���� Ŭ������ �ν����� â���� �����ϱ� ���ؼ� ����ȭ���ִ� ��ɾ�.
public class Dialogue//����� ���� Ŭ������ ����� ���̱⿡ ����Ƽ���� ��ӹ޴� �κ��� MonoBehaviour�� ���ش�.
{
    [Tooltip("��� ġ�� ĳ���� �̸�")]//�ν����� â���� ���������� ��ɾ�.
    public string name;
     [Tooltip("��� ����")]
    public string[] context;//���� ������ ���� �� �����Ƿ� �迭�� ����.
}
[System.Serializable]
public class DialogueEvent//�������� ĳ���Ͱ� DialogueŬ������ ����� �� �ֵ��� Dialogue�� �迭�� ����� �ִ� Ŭ����
{
    public string E_name;//�̺�Ʈ�� �̸�

    public Vector2 line;//��縦 �����س� ���� x~y
    public Dialogue[] dialogues;
}
