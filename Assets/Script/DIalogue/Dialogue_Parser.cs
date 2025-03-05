using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue_Parser : MonoBehaviour
{
  public Dialogue[] Parse(string _CSVFileName)
  {
    List<Dialogue> dialogueList = new List<Dialogue>();//가져온 데이터를 임시로 저장할 리스트 공간.
    TextAsset csvData = Resources.Load<TextAsset>(_CSVFileName);//TextAsset은 CSV데이터를 가져오기 위한 자료형이고,리소스 폴더에서 텍스트에셋형태로 로드해서 CSV파일을 가져온다.

    string[] data = csvData.text.Split(new char[]{'\n'});//CSV파일에 텍스트들을 엔터를 기준으로(한줄을 기준) 잘라서 data[n]에 대입한다.

    for(int i = 1; i < data.Length;)//1부터 시작하는 이유는 0번째는 편의를 위한 설명이기 때문이다.
    {
       string[] row = data[i].Split(new char[]{','});//data에 들어온 한줄들을 ,마 단위로 쪼갠다.(아이디 이름 대사를 각각 얻기 위함.)

       Dialogue dialogue = new Dialogue();//가져온 데이터들을 Dialogue로 받을 대사 리스트 생성.

       dialogue.name = row[1];//한줄이 아이디,이름,대사로 쪼개지기 떄문에 순서대로 대입.

       List<string> contextList = new List<string>();//context는 배열이므로 context[n]번째가 아니라면 row[2]에 저장된 string 값을 넣을 수 없기 때문에 리스트에다가 임시저장
       
        do
        {
            contextList.Add(row[2]);
            if(++i <data.Length)//다음에 가져올 줄의 번호가 data의 길이보다 짦지않다면 마지막 대사이기때문에 검사.
            {
                row = data[i].Split(new char[]{','});//data에 들어온 한줄들을 ,마 단위로 쪼갠다.(아이디 이름 대사를 각각 얻기 위함.);
            }else{
                break;
            }
        }while(row[0].ToString() == "");//대사를 가져올떄 한 캐릭터가 계속 말하고있다면 아이디가 공백상태일 것이기때문에 공백이라면 대사를 계속 가져오고 다음대사를 쪼갠 후 대기..
        
    
        dialogue.context = contextList.ToArray();//리스트쨰로 배열로 형변환 후 배열자체에 대입

        dialogueList.Add(dialogue);//만든 임시공간에 한줄 씩 추가
        
    }

    return dialogueList.ToArray();//다시 배열 형태로 변환해서 반환.
  }
}
