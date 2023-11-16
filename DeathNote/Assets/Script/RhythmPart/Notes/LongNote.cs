using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

using UnityEngine.UI;

public class LongNote : MonoBehaviour
{
    //public Image image; // �̹���
    //private EffectController effectController; // ����Ʈ ����
    //private ScoreManager scoreManager; // ���� ����
    //Animator animator; // �ִϸ�����
    //public List<CenterNote> gauges; // ������ ��Ʈ�� ����Ʈ

    //public Color white = Color.white; // ���� ����
    //public Color transparent; // ��ǥ ���� (ȸ��)

    //private float posX = 0; // ��Ʈ�� ���� ��ġ(x��ǥ)
    //private float posY = 0; // ��Ʈ�� ���� ��ġ(y��ǥ)

    //private double currentTime = 0; // ��Ʈ�� ���� �ð� 
    //private double enabledTime = 0; // ��Ʈ�� ���� �ð�
    //private double timeUnit = 0; // ��Ʈ�� �ð�

    //public double checkTime = 0; // ��Ʈ�� ���� ����
    //public double endTime = 0; // ��Ʈ�� ���� ���� �ð�

    //private float lerpValue; // ������ ���� ��ġ
    //private bool isClicked; // �������� �ȴ������� �Ǵ�
    //private bool holding = false; // ������ �ִ��� �ƴ��� �Ǵ�
    //private int unbreakRatio; // ��ų ��, �޺� �ȱ��� Ȯ��

    //private int scoreSum; // ������ ��ġ

    //void Awake()
    //{
    //    effectController = transform.GetComponentInChildren<EffectController>();
    //    scoreManager = FindObjectOfType<ScoreManager>();
    //    image = transform.GetChild(0).GetComponent<Image>();
    //    transparent = new Color(white.r, white.g, white.b, 0); // �������
    //    unbreakRatio = SkillManager.instance.comboUnbreakRatio; // ��ų ��ü���� ������
    //    animator = transform.GetChild(0).GetComponent<Animator>();

    //}
    //void OnEnable()
    //{

    //    image.color = Color.Lerp(white, transparent, 1); // ��������� ����

    //    transform.localPosition = new Vector3(posX, posY); // ��Ʈ�� ��ġ�� ����
    //    transform.localScale = Vector3.one; // ������ ����

    //    isClicked = false; // Ŭ������ �ʱ�ȭ
    //    holding = false; // Ȧ�忩�� �ʱ�ȭ
    //    scoreSum = 0; // ������ ��ġ �ʱ�ȭ
    //    image.enabled = true; // �̹��� ǥ��
    //    enabledTime = AudioSettings.dspTime; // �ð� �ʱ�ȭ

    //}

    //void Update()
    //{
    //    currentTime = AudioSettings.dspTime; // ���� �ð�
    //    lerpValue = Mathf.Clamp01((float)((currentTime - enabledTime) / (checkTime - enabledTime))); // ����(Clamp�� 0~1�� ����)

    //    // ���� �ִϸ��̼�
    //    image.color = Color.Lerp(white, transparent, 1 - lerpValue);


    //    // �����ð��� 2��Ʈ �ڿ�, ��Ʈ Ǯ�� ��ü����
    //    if (currentTime >= endTime + 2 * timeUnit)
    //    {
    //        NotePool.instance.longQueue.Enqueue(gameObject);
    //        gameObject.SetActive(false); // Active�� ���� false�� ��������

    //    }

    //    // ���� ���� ���� ���¿���, ������ �ð����� 0.5��Ʈ ���Ŀ� ������� 
    //    else if (!isClicked && currentTime >= checkTime + 0.5 * timeUnit)
    //    {

    //        scoreManager.IncreaseCombo(false); //�޺� ����
    //        isClicked = true; // Ŭ���Ǿ��ٰ� ����
    //        effectController.JudgeEffect("miss"); // Dismiss ��

    //        HideImage(); // �̹��� ���߱�
    //    }

    //    // ������ �ִ� ���¿���, ������ �ð����� 0.5��Ʈ ���Ŀ� ������� 
    //    else if (holding && currentTime >= endTime + 0.5 * timeUnit)
    //    {
    //        // ���� ����� �� ������ �ƴ� ��� �ճ�Ʈ�� �״�� �����
    //        holding = !holding;
    //        HideImage();

    //        // ��ų �ߵ��ÿ� �޺��� �Ȳ����
    //        int random = UnityEngine.Random.Range(1, 100);

    //        if (unbreakRatio >= random)
    //        {
    //            scoreManager.IncreaseScore(2, true); // ������ break�� ��
    //            effectController.NoteHitEffect();
    //            effectController.JudgeEffect("good"); // ����Ʈ�� good����

    //        }
    //        scoreManager.IncreaseCombo(false); //�޺� ����
    //        scoreManager.IncreaseScore(2, false); // ������ break�� ��
    //        effectController.NoteHitEffect();
    //        effectController.JudgeEffect("break");
    //        return;
    //    }

    //}


    //// ��Ʈ�� ��ġ�� ������ �ֽ��ϴ�.
    //public void SetNoteInfo(float x, float y, double t, double t2, double u)
    //{
    //    this.posX = x;
    //    this.posY = y;
    //    this.checkTime = t;
    //    this.endTime = t2;
    //    this.timeUnit = u;
    //}

    //public void HideImage()
    //{
    //    Debug.Log("����");
    //    image.enabled = false;
    //    foreach (CenterNote note in gauges) // ���� ��Ʈ�� ���� ������
    //    {
    //        note.Finish();
    //    }
    //}

    //// Ŭ�� �����Ͱ� ������ �� �ߵ�
    //public void OnPointerDown(PointerEventData eventData)
    //{
    //    animator.SetTrigger("hold");
    //    isClicked = true; // �������ٰ� ǥ��
    //    holding = true; // �������ִٰ� ǥ��
    //    double pressTime = AudioSettings.dspTime; // ���� �ð� ����
    //    double[] checkList = new double[] { 0.06, 0.12 };

    //    for (int x = 0; x < checkList.Length; x++)
    //    {
    //        if (Math.Abs(pressTime - checkTime) <= checkList[x])
    //        {
    //            animator.SetTrigger("hold"); // ������ ������, scoreSum�� �÷��ְ� �ִϸ��̼��� ������ �� ��������
    //            scoreSum += x;

    //            return;
    //        }
    //    }
    //    // ���� ����� �� ������ �ƴ� ��� �ճ�Ʈ�� �״�� �����
    //    HideImage();

    //    scoreSum = 3;

    //    // ��ų �ߵ��ÿ� �޺��� �Ȳ����
    //    int random = UnityEngine.Random.Range(1, 100);
    //    if (unbreakRatio >= random)
    //    {
    //        scoreManager.IncreaseScore(2, true); // ������ break�� ��
    //        effectController.NoteHitEffect();
    //        effectController.JudgeEffect("good"); // ����Ʈ�� good����

    //    }
    //    scoreManager.IncreaseCombo(false); //�޺� ����
    //    scoreManager.IncreaseScore(2, false); // ������ break�� ��
    //    effectController.NoteHitEffect();
    //    effectController.JudgeEffect("break");
    //    return;

    //}

    //public void OnPointerUp(PointerEventData eventData)
    //{
    //    if (scoreSum == 3) return;
    //    HideImage(); // �� ���� �̹����� ������

    //    holding = false; // �������ִٰ� ǥ��
    //    double finishTime = AudioSettings.dspTime; // ���� �ð� ����
    //    double[] checkList = new double[] { 0.12 };

    //    for (int x = 0; x < checkList.Length; x++)
    //    {
    //        if (Math.Abs(finishTime - endTime) <= checkList[x])
    //        {

    //            if (scoreSum <= 1)
    //            {
    //                scoreManager.IncreaseCombo(true); // �޺� �߰�
    //                scoreManager.IncreaseScore(0, false); // ���� �߰�
    //                effectController.NoteHitEffect(); // Ÿ������Ʈ ���
    //                effectController.JudgeEffect("perfect");

    //                return;
    //            }

    //            else if (scoreSum <= 2)
    //            {
    //                scoreManager.IncreaseCombo(true); // �޺� �߰�
    //                scoreManager.IncreaseScore(1, false); // ���� �߰�
    //                effectController.NoteHitEffect(); // Ÿ������Ʈ ���
    //                effectController.JudgeEffect("good");

    //                return;
    //            }
    //        }
    //    }

    //    // ��ų �ߵ��ÿ� �޺��� �Ȳ����
    //    int random = UnityEngine.Random.Range(1, 100);

    //    if (unbreakRatio >= random)
    //    {
    //        scoreManager.IncreaseScore(2, true); // ������ break�� ��
    //        effectController.NoteHitEffect();
    //        effectController.JudgeEffect("good"); // ����Ʈ�� good����

    //    }
    //    scoreManager.IncreaseCombo(false); //�޺� ����
    //    scoreManager.IncreaseScore(2, false); // ������ break�� ��
    //    effectController.NoteHitEffect();
    //    effectController.JudgeEffect("break");
    //    return;

    //}
}