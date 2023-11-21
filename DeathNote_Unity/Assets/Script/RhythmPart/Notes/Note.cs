using System;
using System.Collections;

using UnityEngine;
using UnityEngine.EventSystems;

using UnityEngine.UI;

public class Note : MonoBehaviour   
{
    //private Image image; // �̹���
    //private EffectController effectController; // ����Ʈ ����
    //private ScoreManager scoreManager; // ���� ����

    //private Color white = Color.white; // ���
    //private Color transparent; // �����

    //private float posX = 0; // ��Ʈ�� ���� ��ġ(x��ǥ)
    //private float posY = 0; // ��Ʈ�� ���� ��ġ(y��ǥ)

    //private double currentTime = 0; // ��Ʈ�� ���� �ð� 
    //private double enabledTime = 0; // ��Ʈ�� ���� �ð�
    //private double timeUnit = 0; // ��Ʈ�� �ð�
    
    //private float lerpValue; // ������ ���� ��ġ
    //private bool isClicked; // �������� �ȴ������� �Ǵ�
    //private int unbreakRatio; // ��ų ��, �޺� �ȱ��� Ȯ��
    //public double checkTime = 0; // ��Ʈ�� ���� ����

    //void Awake()
    //{
    //    effectController = transform.GetComponentInChildren<EffectController>();
    //    scoreManager = FindObjectOfType<ScoreManager>();
    //    image = transform.GetChild(0).GetComponent<Image>();
    //    transparent = new Color(white.r, white.g, white.b, 0); // �������
    //    unbreakRatio = SkillManager.instance.comboUnbreakRatio; // ��ų ��ü���� ������
    //}
    
    //void OnEnable()
    //{
    //    image.color = Color.Lerp(white, transparent, 1); // ��������� ����

    //    transform.localPosition = new Vector3(posX, posY); // ��Ʈ�� ��ġ�� ����
    //    transform.localScale = Vector3.one; // ������ ����

    //    isClicked = false; // Ŭ������ �ʱ�ȭ
    //    image.enabled = true; // �̹��� ǥ��
    //    enabledTime = AudioSettings.dspTime; // �ð� �ʱ�ȭ
    //}

    //void Update()
    //{
    //    currentTime = AudioSettings.dspTime; // ���� �ð�
    //    lerpValue =  Mathf.Clamp01((float)((currentTime-enabledTime)/(checkTime - enabledTime))); // ����(Clamp�� 0~1�� ����)

    //    // ���� �ִϸ��̼�
    //    image.color = Color.Lerp(white, transparent, 1 - lerpValue);


    //    // �����ð��� �ݺ�Ʈ �ڿ�, Ŭ���� ���� �ʾҴٸ� �̹����� �����
    //    if (currentTime >= checkTime + 0.5 * timeUnit)
    //    {
    //        if (!isClicked)
    //        {
    //            scoreManager.IncreaseCombo(false); //�޺� ����
    //            isClicked = true; // Ŭ���Ǿ��ٰ� ����
    //            effectController.JudgeEffect("miss"); // Dismiss ���
    //        }

    //        HideImage(); // �̹��� ���߱�
    //    }

    //    // �����ð��� 2��Ʈ �ڿ�, ��Ʈ Ǯ�� ��ü����
    //    if (currentTime >= checkTime + 2 * timeUnit)
    //    {
    //        NotePool.instance.normalQueue.Enqueue(gameObject);
    //        gameObject.SetActive(false); // Active�� ���� false�� ��������

    //    }
    //}


    //// ��Ʈ�� �̹����� ����
    //public void HideImage()
    //{
    //    image.enabled =  false;
    //}

    //// ��Ʈ�� ������ �߰�
    //public void SetNoteInfo(float x, float y, double t, double u)
    //{
    //    this.posX = x;
    //    this.posY = y;
    //    this.checkTime = t;
    //    this.timeUnit = u;
    //}

    //// ��Ʈ�� Ŭ������ �� �޼���
    //public void OnPointerDown(PointerEventData eventData)
    //{
    //    isClicked = true; // Ŭ���Ǿ��ٰ� ǥ��
    //    HideImage(); // �̹��� ����
    //    double pressTime = AudioSettings.dspTime; // ���� �ð� ����
    //    double[] checkList = new double[] { 0.06, 0.12 }; // deadly, delicate ���� ����

    //    for (int x = 0; x < 2; x++)
    //    {   
    //        // ���� �ð��� �����ð����� ���̰� ���� ���غ��� ���� ���
    //        if (Math.Abs(pressTime - checkTime) <= checkList[x])
    //        {
    //            scoreManager.IncreaseCombo(true); // �޺� �߰�
    //            scoreManager.IncreaseScore(x, false); // ���� �߰�
    //            effectController.NoteHitEffect(); // Ÿ������Ʈ ���
    //            if(x==0) effectController.JudgeEffect("perfect");
    //            else effectController.JudgeEffect("good");


    //            return;
    //        }
    //    }

    //    int random = UnityEngine.Random.Range(1, 100);
    //    if(unbreakRatio >= random)
    //    {
    //        scoreManager.IncreaseScore(2, true); // ������ break�� ��
    //        effectController.NoteHitEffect();
    //        effectController.JudgeEffect("good"); // ����Ʈ�� good����
            
    //    }
    //    scoreManager.IncreaseCombo(false); //�޺� ����
    //    scoreManager.IncreaseScore(2, false);
    //    effectController.NoteHitEffect();
    //    effectController.JudgeEffect("break");
    //    return;
    //}
}