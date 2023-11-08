using System;
using System.Collections;

using UnityEngine;
using UnityEngine.EventSystems;

using UnityEngine.UI;

public class ClickNote : MonoBehaviour, IPointerDownHandler
{
    public int noteNum; // ��Ʈ��ȣ
    private Image image; // �̹���
    private Animator animator; // ��Ʈ �ִϸ��̼�
    public EffectController effectController; // ����Ʈ ����
    private ScoreManager scoreManager; // ���� ����

    private double currentTime = 0; // ��Ʈ�� ���� �ð� 
    private double enabledTime = 0; // ��Ʈ�� ���� �ð�
    private double timeUnit = 0; // ��Ʈ�� �ð�
    public float speed;

    private float lerpValue; // ������ ���� ��ġ
    private bool isClicked; // �������� �ȴ������� �Ǵ�
    // private int unbreakRatio; // ��ų ��, �޺� �ȱ��� Ȯ��
    public double checkTime = 0; // ��Ʈ�� ���� ����

    void Awake()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        image = transform.GetComponent<Image>();
        animator = transform.GetComponent<Animator>();
    }

    void OnEnable()
    {
        transform.localScale = Vector3.one; // ������ ����
        isClicked = false; // Ŭ������ �ʱ�ȭ
        enabledTime = AudioSettings.dspTime; // �ð� �ʱ�ȭ
        animator.speed = speed; 
    }

    void Update()
    {
        currentTime = AudioSettings.dspTime; // ���� �ð�
        // lerpValue = Mathf.Clamp01((float)((currentTime - enabledTime) / (checkTime - enabledTime))); // ����(Clamp�� 0~1�� ����)

        // �����ð��� �ݺ�Ʈ �ڿ�, Ŭ���� ���� �ʾҴٸ� �̹����� �����
        if (currentTime >= checkTime + 0.25 * timeUnit)
        {
            scoreManager.IncreaseCombo(false); //�޺� ����
            isClicked = true; // Ŭ���Ǿ��ٰ� ����
            effectController.JudgeEffect("miss"); // Dismiss ���
            SetActive(false);
        }
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    // ��Ʈ�� ������ �߰�
    public void SetNoteInfo(double t, double u)
    {
        this.checkTime = t;
        this.timeUnit = u;
    }

    float EvaluatePress(float pressTime)
    {
        // checkTime�� pressTime ������ ���� ���� ���
        float absDifference = Mathf.Abs((float)(checkTime - pressTime));

        // ���� �ִ� ���� �ð� ����
        float maxDifference = (float)(1/speed);

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

    // ��Ʈ�� Ŭ������ �� �޼���
    public void OnPointerDown(PointerEventData eventData)
    {
        
        isClicked = true; // Ŭ���Ǿ��ٰ� ǥ��
        double pressTime = AudioSettings.dspTime; // ���� �ð� ����
        double bestTime = 0.1; // �ְ� ���� ����
        if (Math.Abs(pressTime - checkTime) <= bestTime)
        {
      
            scoreManager.IncreaseCombo(true); // �޺� �߰�
            scoreManager.IncreaseScore(100); // ���� �߰�
            effectController.NoteHitEffect(); // Ÿ������Ʈ ���
            effectController.JudgeEffect("perfect");
            SetActive(false);
            
            return;
        }

        else
        {
            float lerpValue = EvaluatePress((float)pressTime);
            // 1�� �̳��� ���
                scoreManager.IncreaseCombo(true); // �޺� �߰�
                scoreManager.IncreaseScore((int)(lerpValue*100)); // ������ break�� ��
                effectController.NoteHitEffect();
                effectController.JudgeEffect("good"); // ����Ʈ�� good����
                SetActive(false);
                return;
        }
    }
}
