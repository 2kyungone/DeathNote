using System;

using UnityEngine;
using UnityEngine.EventSystems;

using UnityEngine.UI;

public class ClickNote : MonoBehaviour, IPointerDownHandler
{
    //��������
    public int noteNum; // ��Ʈ��ȣ
    public Animator animator; // ��Ʈ �ִϸ�����
    private NotePool notePool; // ����� ��ƮǮ
    public Effect effect; // ����� ������

    private double currentTime = 0; // ���� �ð� 
    public double checkTime = 0; // ��Ʈ�� ��Ȯ�� ���� �ð�
    private double timeUnit = 0; // ��Ʈ�� �ð�
    public float speed = 1; // ��Ʈ�� ���ǵ�

    public int bonus; // ���ʽ� ����
    public int combo; // �޺� ���ʽ�
    public int perfect; // ����Ʈ ���ʽ�

    void OnEnable()
    {
        transform.localScale = Vector3.one; // ������ ����

    }

    void Update()
    {
        currentTime = AudioSettings.dspTime; // ���� �ð�

        // �����ð��� �ݺ�Ʈ �ڿ�, Ŭ���� ���� �ʾҴٸ� �̹����� �����
        if (currentTime >= checkTime + 0.25 * timeUnit)
        {
            ScoreManager.instance.IncreaseCombo(false); //�޺� ����
            effect.JudgeEffect("miss"); // ���ƴٰ� ���
            SetActive(false);// ��Ʈ ��Ȱ��ȭ 
            notePool.clickQueue.Enqueue(gameObject); // ������Ʈ�� �ٽ� Ǯ�� ����
        }
    }

    public void SetActive(bool status)
    {
        gameObject.SetActive(status);


    }

    // ��Ʈ�� ����Ʈ�� ��Ʈ Ǯ�� ����
    public void SetNoteInfo(Effect effect, NotePool pool, double t, double u)
    {
        this.effect = effect;
        this.notePool = pool;
        this.checkTime = t;
        this.timeUnit = u;
    }

    // ��Ʈ�� Ŭ������ ���� �޼���
    public void OnPointerDown(PointerEventData eventData)
    {

        double pressTime = AudioSettings.dspTime; // ���� �ð� ����
        double bestTime = 0.08; // �ְ� ���� ���� : 0.1��
        if (Math.Abs(pressTime - checkTime) <= bestTime)
        {
            ScoreManager.instance.IncreaseCombo(true); // �޺� �߰�
            ScoreManager.instance.IncreaseScore(100); // ���� �߰�
            effect.NoteHitEffect(); // Ÿ�� ����Ʈ ���
            effect.JudgeEffect("perfect"); // ���� �̺�Ʈ ���
            SetActive(false); // ��Ʈ ��Ȱ��ȭ

            notePool.clickQueue.Enqueue(gameObject); // ������Ʈ�� �ٽ� Ǯ�� ����
            
        }

        else
        {
            float lerpValue = EvaluatePress((float)pressTime); // ���� �ð��� ��Ȯ���� ���
            Debug.Log("�������:" + lerpValue);
            if (lerpValue < 0.1)
            {
                ScoreManager.instance.IncreaseCombo(false); //�޺� ����
                effect.JudgeEffect("miss"); // ���ƴٰ� ���
                SetActive(false);// ��Ʈ ��Ȱ��ȭ 
                notePool.clickQueue.Enqueue(gameObject); // ������Ʈ�� �ٽ� Ǯ�� ����

            }
            else
            {
                ScoreManager.instance.IncreaseCombo(true); // �޺� �߰�
                ScoreManager.instance.IncreaseScore((int)(lerpValue * 100)); // ������ ��꿡 ����
                effect.NoteHitEffect(); // ���� �̺�Ʈ ���
                effect.JudgeEffect("good"); // ����Ʈ�� good����
                SetActive(false); // ��Ʈ ��Ȱ��ȭ
                notePool.clickQueue.Enqueue(gameObject); // ������Ʈ�� �ٽ� Ǯ�� ����
            }

        }
    }

    // ���� ��Ȯ�� ��� �޼���
    float EvaluatePress(float pressTime)
    {
        // checkTime�� pressTime ������ ���� ���� ���
        float absDifference = Mathf.Abs((float)(checkTime - pressTime));

        // ���� �ִ� ���� �ð� ����
        float maxDifference = (float)(0.5 * timeUnit);
        // absDifference�� maxDifference �̳��� ��� ������ ���, �׷��� ������ 0
        if (absDifference <= maxDifference)
        {
            // absDifference�� 0�̸� �Ϻ��ϰ� �������Ƿ� 1 ��ȯ
            // absDifference�� maxDifference�� ������ ���� ���� ������ 0 ��ȯ
            // �� ������ ���� ���� �������� ���
            return 1 - (absDifference / maxDifference);
        }
        else
        {
            // ���� �ִ� ���̸� ������Ƿ� 0 ��ȯ
            return 0;
        }
    }
}
