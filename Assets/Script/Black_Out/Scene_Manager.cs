using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scene_Manager : MonoBehaviour
{
    public static Scene_Manager Instance //외부에서 참조할 Scene_Manager 형식의 스크립트 변수인 Instance에 내부용 instance 정보를 가져옴.
    {
        get//값을 가져올때 쓰이는 함수.
        {
            return instance;
        }
    }

    private static Scene_Manager instance;//정보를 담을 변수 (private로 선언하여 외부참조 없이 여기서만 정보를 저장함.)
    public CanvasGroup Fade_img;//암전에 쓰일 이미지. CanvasGroup인 이유는 하위속성인 blocksRaycastfmf 켜서 이미지가 켜진동안 버튼 클릭을 막기위함.
    private float fadeDuration = 3; //암전되는 시간
    void Start()
    {
        Time.timeScale = 1f;//다른씬에서 연출을 위해 시간을 건드렸을 경우 초기화.
        if (instance != null)//시작했을떄 이미 정보를 가진 객체가 존재한다면 충돌 방지를 위해 그 객체를 없에고 이 객체를 유지함.
        {
            DestroyImmediate(this.gameObject);//DestroyImmediate는 지연시간 줄 수 없어 즉시 삭제하며,Destroy는 지연시간을 줄 수있다.
            //Destroy는 오류(가비지)가 많이생겨 여러번 삭제가 필요할시 쓰지 않는다.
            return;//탈출
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += load_Scene; // 씬매니저가 씬을 로드할떄 호출할 이벤트에 추가
    }   
    private void OnDestroy() {
    SceneManager.sceneLoaded -= load_Scene; // 이벤트에서 제거*
    }    

    public void Change_Scene(string Scene_Name)//화면 전환 함수.
    {
        StartCoroutine("Fade_Coroutine",Scene_Name);
    }
    private void load_Scene(Scene scene, LoadSceneMode mode)//로드후 암전을 풀어주는 함수.Scene scene, LoadSceneMode mode게 있어야지만 지정할수 있다.
    {
        StartCoroutine("Load_Coroutine");
    }


    IEnumerator Fade_Coroutine(string Scene_Name)//화면 어둡게 하는 코루틴.
    {
        Fade_img.blocksRaycasts = true; //코루틴이 실행되면 버튼이 눌리는것을 막기.
        while(Fade_img.alpha < 1.0f)//알파값이 1이 되기 전까지(다 켜지기 전까지) 반복
        {
            float speed = Time.unscaledDeltaTime  / fadeDuration;//실행될 속도.
            //unscaledDeltaTime 은 특정 씬에서 타임스케일을 줄여도 영향받지 않고 일정한 계산을 할수있도록 하기 위해 채용.
            Fade_img.alpha += speed;
           yield return new WaitForSecondsRealtime(speed);//0.01초마다 실행
        }
        Fade_img.blocksRaycasts = false; //코루틴이 끝나면 버튼을 다시 키기.
        AsyncOperation async = SceneManager.LoadSceneAsync(Scene_Name);//비동기 방식 씬로드방식으로 loadScene과 다르게 다른작업을 같이 수행할 수 있음
        //프로퍼티로는 allowSceneActivation(씬 즉시 활성화 여부 기본값 true),isDone(비동기 작업 완료 여부),progress(진행상황 여부 0.0~1.0)
    }
     IEnumerator Load_Coroutine()//화면 밝게 하는 코루틴.
    {
        Fade_img = FindObjectOfType<CanvasGroup>();//시작하자마자 블랙이미지를 찾아준다.
        Fade_img.alpha = 1.0f;//검게 만들어줌.
        Fade_img.blocksRaycasts = true; //코루틴이 실행되면 버튼이 눌리는것을 막기.
        while(Fade_img.alpha > 0f)//알파값이 0이 되기 전까지(다 켜지기 전까지) 반복
        {
            float speed = Time.unscaledDeltaTime  / fadeDuration;//실행될 속도.
            //unscaledDeltaTime 은 특정 씬에서 타임스케일을 줄여도 영향받지 않고 일정한 계산을 할수있도록 하기 위해 채용.
            Fade_img.alpha -= speed;
           yield return new WaitForSecondsRealtime(speed);//0.01초마다 실행
        }
        Fade_img.blocksRaycasts = false; //코루틴이 끝나면 버튼을 다시 키기.
    }
}
