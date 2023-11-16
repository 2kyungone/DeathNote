using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MoveSetting : MonoBehaviour
{
    public static MoveSetting instance;
    public RectTransform uiElement; // �̵���ų UI ���
    public Vector3 offScreenPosition; // ȭ�� ���� ��ġ
    public Vector3 onScreenPosition; // ȭ�� ���� ��ġ
    public float moveSpeed = 1.0f; // �̵� �ӵ�
    public GameObject regame;
    public GameObject returns;
    Coroutine cor;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // �� ��ȯ�� �ı����� �ʵ��� ����
        }
        else if (instance != this)
        {
            Destroy(gameObject); // �ߺ� �ν��Ͻ��� �ı�
        }
    }
    // ȭ�� ������ �̵�
    public void MoveIn()
    {
        if(cor!=null)StopCoroutine(cor);
        cor = StartCoroutine(Move(uiElement, onScreenPosition, moveSpeed));
    }

    // ȭ�� ������ �̵�
    public void MoveOut()
    {
        if (cor != null) StopCoroutine(cor);
        cor = StartCoroutine(Move(uiElement, offScreenPosition, moveSpeed));
    }

    public void SceneChange()
    {
        Debug.Log("������");
        MusicManager.instance.audioSource.Stop();
        MusicManager.instance.gameStart = false;
        MoveOut();
        SceneManager.LoadScene("StageScene");
    }

    public void SceneRestart()
    {
        Debug.Log("������");
        // ���� Ȱ�� ���� �̸��� ����ϴ�.
        string sceneName = SceneManager.GetActiveScene().name;
        MoveOut();
        // ���� Ȱ�� ���� �ٽ� �ε��մϴ�.
        SceneManager.LoadScene(sceneName);
    }

    // �̵� �ڷ�ƾ
    IEnumerator Move(RectTransform element, Vector3 targetPosition, float speed)
    {
        if (MusicManager.instance.gameStart)
        {
            regame.SetActive(true);
            returns.SetActive(true);
        }
        else
        {
            regame.SetActive(false);
            returns.SetActive(false);
        }
        while (Vector3.Distance(element.localPosition, targetPosition) > 0.01f)
        {
            element.localPosition = Vector3.MoveTowards(element.localPosition, targetPosition, speed * Time.deltaTime);
            yield return null;
        }
    }

}
