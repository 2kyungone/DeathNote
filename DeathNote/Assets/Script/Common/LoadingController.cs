using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingController : MonoBehaviour
{
    //�Լ��� ������ ����ƽ���� �����ϸ� �ε������� �Ѿ���� �ʾƼ� �ε���Ʈ�ѷ��� ������ ������Ʈ�� �������� �ʾƵ� �ε��� ��Ʈ�ѷη��� �Լ����� ���� ȣ���� ����Ѵ�.
    static string nextScene;

    [SerializeField] Image progressBar;

    public static void LoadScene(string sceneName)
    {
        //�ε��� �ҷ��´�.
        nextScene = sceneName;
        SceneManager.LoadScene("Loading");
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
        bool firstVal = false;
        while(!op.isDone) //�� �ε��� ������ ���� ���¶�� ��� �ݺ��Ѵ�.
        {
            //����Ƽ ������ ������� �ѱ��. 
            //�ݺ��� �� ������ ������� �ѱ��� ������ �ݺ����� ������ ������ ȭ���� ���ŵ��� �ʾƼ� ����ٰ� �������� ���� ��
            yield return null;

            if(op.progress < 0.9f)
            {
                progressBar.fillAmount = op.progress;
            }
            else
            {
                timer += Time.unscaledDeltaTime;
                progressBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);

                if(progressBar.fillAmount >= 1.0f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}
