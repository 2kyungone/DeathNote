using System;
using System.Collections;

using UnityEngine;
using UnityEngine.EventSystems;

using UnityEngine.UI;

public class EndNote : MonoBehaviour{
    public Color white = Color.white; // ���� ����
    public Color transparent; // ��ǥ ���� (ȸ��)

    // ��Ʈ�� ���� ��ġ(x��ǥ)
    private float posX = 0;
    // ��Ʈ�� ���� ��ġ(y��ǥ)
    private float posY = 0;
    // ��Ʈ�� ���� �ð�
    public double checkTime = 0;
    public double currentTime = 0;
    public double enabledTime = 0;
    private double collapseTime = 0;

    public bool isChecked = false;

    Image image;
    EffectController effectController; // ����Ʈ
    ScoreManager scoreManager; // ����
    Animator animator;

    void Awake()
    {
        effectController = transform.GetComponentInChildren<EffectController>();
        scoreManager = FindObjectOfType<ScoreManager>();
        animator = GetComponentInChildren<Animator>();
        image = GetComponentInChildren<Image>();
    }
    void OnEnable()
    {
        // ��������� ����
        image.color = Color.Lerp(white, transparent, 1);

        // ��Ʈ�� ��ġ�� �����ϰ�, �������� 1�� ����
        transform.localPosition = new Vector3(posX, posY);
        transform.localScale = Vector3.one;


        enabledTime = AudioSettings.dspTime;
        isChecked = false;
        animator.Play("Idle");
    }

    void Update()
    {
        currentTime = AudioSettings.dspTime;

        if (currentTime >= checkTime + 0.5 * collapseTime)
        {
            if (!isChecked)
            {
                isChecked = true;
                effectController.JudgeEffect("Dismiss");
            }

            HideImage();

        }


        float lerpValue = (float)((currentTime - enabledTime) / (checkTime - enabledTime));
        lerpValue = Mathf.Clamp01(lerpValue);
        image.color = Color.Lerp(white, transparent, 1 - lerpValue);

    }


    // ��Ʈ�� �̹����� ���۴ϴ�.
    public void HideImage()
    {
        image.enabled = false;
    }

    public void SetNoteInfo(float x, float y, double t, double t2)
    {
        this.posX = x;
        this.posY = y;
        this.checkTime = t;
        this.collapseTime = t2;
    }

    public void Check(double compareTime)
    {

        double[] checkList = new double[] { 0.08, 0.13 };
        isChecked = true;

        for (int x = 0; x < checkList.Length; x++)
        {
            if (Math.Abs(compareTime - checkTime) <= checkList[x])
            {
                effectController.NoteHitEffect();
                if (x == 0) effectController.JudgeEffect("Deadly");
                else effectController.JudgeEffect("Delicate");
                scoreManager.IncreaseCombo(true);
                scoreManager.IncreaseScore(x, false);

                return;
            }
        }

        effectController.NoteHitEffect();
        effectController.JudgeEffect("Discord");
        scoreManager.IncreaseCombo(false);
        scoreManager.IncreaseScore(2, false);

        return;

    }

    public void Finish()
    {
        NotePool.instance.endQueue.Enqueue(gameObject);
        gameObject.SetActive(false);
    }


}