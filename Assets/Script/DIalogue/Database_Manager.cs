using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Database_Manager : MonoBehaviour
{
    public static Database_Manager Instance;//���� �����θ� ����ƽȭ(�ν��Ͻ�ȭ)

    [SerializeField] string CSV_FileName;//������ ������ �̸�.

    Dictionary<int,Dialogue> dialogue_Dic = new Dictionary<int, Dialogue>();//

    public static bool isFinish = false;//���� ����Ǿ����� ���θ� Ȯ���� �� ����

    void Awake()
    {
        if(Instance  == null)//�����ͺ��̽� �Ŵ����� �ν��Ͻ��� ����ִٸ� ������ �־��ش�.
        {
            Instance = this;//������ �����Ѵ�
            Dialogue_Parser the_Parser = GetComponent<Dialogue_Parser>();
            Dialogue[] dialogues = the_Parser.Parse(CSV_FileName);//������ ��������
            for(int i = 0; i < dialogues.Length; i++)
            {
                dialogue_Dic.Add(i+1,dialogues[i]);//�������̰� �ϱ����ؼ� i�� 1���� �����ؼ� 1�� ù���� �����ͺ��� ����
            }
            isFinish = true;
        }   
    }

    public Dialogue[] GetDialogues(int Start_Num,int End_Num)//������ ù���� ���ΰ�, ������ ��ȣ 3������������ ������ 1~3
    {
        List<Dialogue> dialogueList = new List<Dialogue>();//��ȯ�ϱ��� ������ ����.

        for(int i = 0; i <= End_Num - Start_Num; i++)//i�� 0���� �����ϰ� ���ǽ��� ���ų� ���� �����Ƿ� ���� ���ذ���ŭ �ݺ��Ѵ�(1,3�̸� 2�� �����µ� 0 1 2 3���� �ݺ�)
        {
            dialogueList.Add(dialogue_Dic[Start_Num+i]);//��ųʸ����� Ű�ڵ带 1���� ���������Ƿ� 0,1,2�ݺ��Ѵٸ� 1,2,3�� �ǰ� 1�� ������
        }
        
        return dialogueList.ToArray();//�迭�� ��ȯ�� ��ȯ.
    }
}
