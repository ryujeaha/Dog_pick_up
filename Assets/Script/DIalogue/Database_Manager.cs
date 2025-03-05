using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Database_Manager : MonoBehaviour
{
    public static Database_Manager Instance;//본인 스스로를 스태틱화(인스턴스화)

    [SerializeField] string CSV_FileName;//가져올 파일의 이름.

    Dictionary<int,Dialogue> dialogue_Dic = new Dictionary<int, Dialogue>();//

    public static bool isFinish = false;//전부 저장되었는지 여부를 확인할 불 변수

    void Awake()
    {
        if(Instance  == null)//데이터베이스 매니저의 인스턴스가 비어있다면 본인을 넣어준다.
        {
            Instance = this;//본인을 지정한다
            Dialogue_Parser the_Parser = GetComponent<Dialogue_Parser>();
            Dialogue[] dialogues = the_Parser.Parse(CSV_FileName);//데이터 가져오기
            for(int i = 0; i < dialogues.Length; i++)
            {
                dialogue_Dic.Add(i+1,dialogues[i]);//직관적이게 하기위해서 i를 1부터 시작해서 1에 첫번쨰 데이터부터 저장
            }
            isFinish = true;
        }   
    }

    public Dialogue[] GetDialogues(int Start_Num,int End_Num)//가져올 첫번쨰 라인과, 마지막 번호 3까지가져오고 싶으면 1~3
    {
        List<Dialogue> dialogueList = new List<Dialogue>();//반환하기전 저장할 공간.

        for(int i = 0; i <= End_Num - Start_Num; i++)//i가 0부터 시작하고 조건식을 같거나 적게 했으므로 둘을 빼준값만큼 반복한다(1,3이면 2가 나오는데 0 1 2 3번을 반복)
        {
            dialogueList.Add(dialogue_Dic[Start_Num+i]);//딕셔너리에서 키코드를 1부터 저장했으므로 0,1,2반복한다면 1,2,3이 되게 1을 더해줌
        }
        
        return dialogueList.ToArray();//배열로 변환후 반환.
    }
}
