using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameStartController : MonoBehaviour
{
    public Text countdownText;
    public Text scoreText;
    public Animator backgroundAnimator;
    public ColliderMove colliderMoveScript;
    public Rigidbody2D controlledRigidbody;

    public static bool shouldStartCountdown = true; // ó���� true�� ����

    private void Start()
    {
        if (shouldStartCountdown)
        {
            InitGameStart();
            StartCoroutine(StartCountdown());
        }
        else
        {
            InitGameResume(); // ī��Ʈ�ٿ� ���� ���� �簳
        }
    }

    private void InitGameStart()
    {
        // ī��Ʈ�ٿ� ���ۿ� �ʿ��� �ʱ�ȭ
        scoreText.gameObject.SetActive(false);
        backgroundAnimator.enabled = false;
        colliderMoveScript.StopMoving();
        controlledRigidbody.simulated = false;
    }

    private void InitGameResume()
    {
        // ī��Ʈ�ٿ� ���� ���� �簳�� �ʿ��� �ʱ�ȭ
        countdownText.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(true);
        backgroundAnimator.enabled = true;
        colliderMoveScript.StartMoving();
        controlledRigidbody.simulated = true;
    }

    private IEnumerator StartCountdown()
    {
        countdownText.text = "3";
        yield return new WaitForSeconds(1);

        countdownText.text = "2";
        yield return new WaitForSeconds(1);

        countdownText.text = "1";
        yield return new WaitForSeconds(1);

        countdownText.text = "���� ����!";
        yield return new WaitForSeconds(1);

        countdownText.gameObject.SetActive(false); // ī��Ʈ�ٿ� �ؽ�Ʈ�� ����ϴ�.
        scoreText.gameObject.SetActive(true); // ���ھ� �ؽ�Ʈ�� Ȱ��ȭ�մϴ�.
        backgroundAnimator.enabled = true; // ��� �ִϸ��̼��� Ȱ��ȭ�մϴ�.
        colliderMoveScript.StartMoving(); // �������� �簳�մϴ�.
        controlledRigidbody.simulated = true; // ���� �ùķ��̼��� �簳�մϴ�.
    }

    public static void ResetCountdown()
    {
        shouldStartCountdown = false; // ī��Ʈ�ٿ��� ��Ȱ��ȭ
    }
}
