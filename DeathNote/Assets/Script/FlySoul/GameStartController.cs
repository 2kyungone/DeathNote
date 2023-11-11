using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameStartController : MonoBehaviour
{
    public Text countdownText;
    public Text scoreText;
    public Animator backgroundAnimator; // ��� �ִϸ�����
    public ColliderMove colliderMoveScript; // ColliderMove ��ũ��Ʈ
    public Rigidbody2D controlledRigidbody; // ������ Rigidbody 2D

    private void Start()
    {
        scoreText.gameObject.SetActive(false); // ���ھ� �ؽ�Ʈ�� ����ϴ�.
        backgroundAnimator.enabled = false; // ��� �ִϸ��̼��� ��Ȱ��ȭ�մϴ�.
        colliderMoveScript.StopMoving(); // �������� �����մϴ�.
        controlledRigidbody.simulated = false; // ���� �ùķ��̼��� �����մϴ�.

        StartCoroutine(StartCountdown());
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
}
