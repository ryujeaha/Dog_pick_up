using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue_Parser : MonoBehaviour
{
  public Dialogue[] Parse(string _CSVFileName)
  {
    List<Dialogue> dialogueList = new List<Dialogue>();//������ �����͸� �ӽ÷� ������ ����Ʈ ����.
    TextAsset csvData = Resources.Load<TextAsset>(_CSVFileName);//TextAsset�� CSV�����͸� �������� ���� �ڷ����̰�,���ҽ� �������� �ؽ�Ʈ�������·� �ε��ؼ� CSV������ �����´�.

    string[] data = csvData.text.Split(new char[]{'\n'});//CSV���Ͽ� �ؽ�Ʈ���� ���͸� ��������(������ ����) �߶� data[n]�� �����Ѵ�.

    for(int i = 1; i < data.Length;)//1���� �����ϴ� ������ 0��°�� ���Ǹ� ���� �����̱� �����̴�.
    {
       string[] row = data[i].Split(new char[]{','});//data�� ���� ���ٵ��� ,�� ������ �ɰ���.(���̵� �̸� ��縦 ���� ��� ����.)

       Dialogue dialogue = new Dialogue();//������ �����͵��� Dialogue�� ���� ��� ����Ʈ ����.

       dialogue.name = row[1];//������ ���̵�,�̸�,���� �ɰ����� ������ ������� ����.

       List<string> contextList = new List<string>();//context�� �迭�̹Ƿ� context[n]��°�� �ƴ϶�� row[2]�� ����� string ���� ���� �� ���� ������ ����Ʈ���ٰ� �ӽ�����
       
        do
        {
            contextList.Add(row[2]);
            if(++i <data.Length)//������ ������ ���� ��ȣ�� data�� ���̺��� �F���ʴٸ� ������ ����̱⶧���� �˻�.
            {
                row = data[i].Split(new char[]{','});//data�� ���� ���ٵ��� ,�� ������ �ɰ���.(���̵� �̸� ��縦 ���� ��� ����.);
            }else{
                break;
            }
        }while(row[0].ToString() == "");//��縦 �����Ë� �� ĳ���Ͱ� ��� ���ϰ��ִٸ� ���̵� ��������� ���̱⶧���� �����̶�� ��縦 ��� �������� ������縦 �ɰ� �� ���..
        
    
        dialogue.context = contextList.ToArray();//����Ʈ���� �迭�� ����ȯ �� �迭��ü�� ����

        dialogueList.Add(dialogue);//���� �ӽð����� ���� �� �߰�
        
    }

    return dialogueList.ToArray();//�ٽ� �迭 ���·� ��ȯ�ؼ� ��ȯ.
  }
}
