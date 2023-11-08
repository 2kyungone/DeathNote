using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingController : MonoBehaviour
{
    //�Լ��� ������ ����ƽ���� �����ϸ� �ε������� �Ѿ���� �ʾƼ� �ε���Ʈ�ѷ��� ������ ������Ʈ�� �������� �ʾƵ� �ε��� ��Ʈ�ѷη��� �Լ����� ���� ȣ���� ����Ѵ�.
    static string nextScene;

    [SerializeField] GameObject one1;
    [SerializeField] GameObject one2;
    [SerializeField] GameObject one3;
    [SerializeField] GameObject one4;
    [SerializeField] GameObject one5;

    public static void LoadScene(string sceneName)
    {
        //�ε��� �ҷ��´�.
        nextScene = sceneName;
        SceneManager.LoadScene("Loading");
    }

    private void Awake()
    {
        one1.SetActive(false);
        one2.SetActive(false);
        one3.SetActive(false);
        one4.SetActive(false);
        one5.SetActive(false);
    }

    private void Start()
    {
        StartCoroutine(LoadSceneProcess());
    }

    IEnumerator LoadSceneProcess()
    {
        //�񵿱� ����̶� ���� �ҷ����� ���߿� �ٸ� �۾��� �����ϴ�
        AsyncOperation op  = SceneManager.LoadSceneAsync(nextScene);
        //�� �ε��� ��� ���缭 �ε����� �ʹ� ���� �Ѿ�� �ʰ� �Ѵ�(����ũ �ε�)
        //���� 90%������ �ε��Ѵ�.
        op.allowSceneActivation = false;


        //�ε� ��Ȳ ǥ�����ִ� �ڵ�
        float timer = 0f;
        while(!op.isDone) //�� �ε��� ������ ���� ���¶�� ��� �ݺ��Ѵ�.
        {
            print(op.progress);
            if(op.progress > 0.1f) one1.SetActive(true);
            if(op.progress > 0.3f) one2.SetActive(true);
            if(op.progress > 0.5f) one3.SetActive(true);
            if(op.progress > 0.7f) one4.SetActive(true);

            //����Ƽ ������ ������� �ѱ��. 
            //�ݺ��� �� ������ ������� �ѱ��� ������ �ݺ����� ������ ������ ȭ���� ���ŵ��� �ʾƼ� ����ٰ� �������� ���� ��
            yield return null;

            if(op.progress >= 0.9f)
            {
                timer += Time.unscaledDeltaTime;
            }
            float time = Mathf.Lerp(0.9f, 1f, timer);
            if(time > 0.98f) one5.SetActive(true);
            if (time >= 1.0f)
            {
                op.allowSceneActivation = true;
                yield break;
            }
        }
    }
}
