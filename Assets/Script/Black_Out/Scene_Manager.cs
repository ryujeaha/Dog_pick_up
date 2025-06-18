using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scene_Manager : MonoBehaviour
{
    public static Scene_Manager Instance //�ܺο��� ������ Scene_Manager ������ ��ũ��Ʈ ������ Instance�� ���ο� instance ������ ������.
    {
        get//���� �����ö� ���̴� �Լ�.
        {
            return instance;
        }
    }

    private static Scene_Manager instance;//������ ���� ���� (private�� �����Ͽ� �ܺ����� ���� ���⼭�� ������ ������.)
    public CanvasGroup Fade_img;//������ ���� �̹���. CanvasGroup�� ������ �����Ӽ��� blocksRaycastfmf �Ѽ� �̹����� �������� ��ư Ŭ���� ��������.
    private float fadeDuration = 3; //�����Ǵ� �ð�
    void Start()
    {
        Time.timeScale = 1f;//�ٸ������� ������ ���� �ð��� �ǵ���� ��� �ʱ�ȭ.
        if (instance != null)//���������� �̹� ������ ���� ��ü�� �����Ѵٸ� �浹 ������ ���� �� ��ü�� ������ �� ��ü�� ������.
        {
            DestroyImmediate(this.gameObject);//DestroyImmediate�� �����ð� �� �� ���� ��� �����ϸ�,Destroy�� �����ð��� �� ���ִ�.
            //Destroy�� ����(������)�� ���̻��� ������ ������ �ʿ��ҽ� ���� �ʴ´�.
            return;//Ż��
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += load_Scene; // ���Ŵ����� ���� �ε��ҋ� ȣ���� �̺�Ʈ�� �߰�
    }   
    private void OnDestroy() {
    SceneManager.sceneLoaded -= load_Scene; // �̺�Ʈ���� ����*
    }    

    public void Change_Scene(string Scene_Name)//ȭ�� ��ȯ �Լ�.
    {
        StartCoroutine("Fade_Coroutine",Scene_Name);
    }
    private void load_Scene(Scene scene, LoadSceneMode mode)//�ε��� ������ Ǯ���ִ� �Լ�.Scene scene, LoadSceneMode mode�� �־������ �����Ҽ� �ִ�.
    {
        StartCoroutine("Load_Coroutine");
    }


    IEnumerator Fade_Coroutine(string Scene_Name)//ȭ�� ��Ӱ� �ϴ� �ڷ�ƾ.
    {
        Fade_img.blocksRaycasts = true; //�ڷ�ƾ�� ����Ǹ� ��ư�� �����°��� ����.
        while(Fade_img.alpha < 1.0f)//���İ��� 1�� �Ǳ� ������(�� ������ ������) �ݺ�
        {
            float speed = Time.unscaledDeltaTime  / fadeDuration;//����� �ӵ�.
            //unscaledDeltaTime �� Ư�� ������ Ÿ�ӽ������� �ٿ��� ������� �ʰ� ������ ����� �Ҽ��ֵ��� �ϱ� ���� ä��.
            Fade_img.alpha += speed;
           yield return new WaitForSecondsRealtime(speed);//0.01�ʸ��� ����
        }
        Fade_img.blocksRaycasts = false; //�ڷ�ƾ�� ������ ��ư�� �ٽ� Ű��.
        AsyncOperation async = SceneManager.LoadSceneAsync(Scene_Name);//�񵿱� ��� ���ε������� loadScene�� �ٸ��� �ٸ��۾��� ���� ������ �� ����
        //������Ƽ�δ� allowSceneActivation(�� ��� Ȱ��ȭ ���� �⺻�� true),isDone(�񵿱� �۾� �Ϸ� ����),progress(�����Ȳ ���� 0.0~1.0)
    }
     IEnumerator Load_Coroutine()//ȭ�� ��� �ϴ� �ڷ�ƾ.
    {
        Fade_img = FindObjectOfType<CanvasGroup>();//�������ڸ��� ���̹����� ã���ش�.
        Fade_img.alpha = 1.0f;//�˰� �������.
        Fade_img.blocksRaycasts = true; //�ڷ�ƾ�� ����Ǹ� ��ư�� �����°��� ����.
        while(Fade_img.alpha > 0f)//���İ��� 0�� �Ǳ� ������(�� ������ ������) �ݺ�
        {
            float speed = Time.unscaledDeltaTime  / fadeDuration;//����� �ӵ�.
            //unscaledDeltaTime �� Ư�� ������ Ÿ�ӽ������� �ٿ��� ������� �ʰ� ������ ����� �Ҽ��ֵ��� �ϱ� ���� ä��.
            Fade_img.alpha -= speed;
           yield return new WaitForSecondsRealtime(speed);//0.01�ʸ��� ����
        }
        Fade_img.blocksRaycasts = false; //�ڷ�ƾ�� ������ ��ư�� �ٽ� Ű��.
    }
}
