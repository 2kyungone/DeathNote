using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

using UnityEngine.UI;

public class LongNote : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public Color white = Color.white; // ���� ����
    public Color transparent; // ��ǥ ���� (ȸ��)

    private int noteNum = 0;
    // ��Ʈ�� ���� ��ġ(x��ǥ)
    private float posX = 0;
    // ��Ʈ�� ���� ��ġ(y��ǥ)
    private float posY = 0;
    // �Ÿ� ���� �⺻ ����
    private float deltaX = 0;
    // ��Ʈ�� ���� ���� �ð�
    private double checkTime = 0;
    // ��Ʈ�� ���� ���� �ð�
    private double endTime = 0;
    // ���� �ð�
    private double currentTime = 0;
    // �������ϴ� �ð�
    private double enabledTime = 0;
    // ��Ʈ�� ������� �������� �ð��� ���ϱ� ���� ����(��Ʈ�� �ð�)
    private double timeUnit = 0;
    // ��Ʈ�� ��/��
    public bool isLeft;
 

    private bool holding = false;
    private bool isPressed = false;

    private EndNote endNote = null;

    Image image;

    Animator animator;
    EffectController effectController; // ����Ʈ
    ScoreManager scoreManager; // ����
    StageManager stageManager; // ���������Ŵ���


    void Awake()
    {
        effectController = transform.GetComponentInChildren<EffectController>();
        scoreManager = FindObjectOfType<ScoreManager>();
        stageManager = FindObjectOfType<StageManager>();
        animator = GetComponentInChildren<Animator>();
        image = GetComponentInChildren<Image>();

        // ť �ȿ� �˻��� �߾� ��Ʈ�� �ð��� �־��

        transparent = new Color(white.r, white.g, white.b, 0);

    }
    void OnEnable()
    {
 
        // ��������� ����
        image.color = Color.Lerp(white, transparent, 1);
       
        // ��Ʈ�� ��ġ�� �����ϰ�, �������� 1�� ����
        transform.localPosition = new Vector3 (posX, posY);
        transform.localScale = Vector3.one;

        // �ʿ��� ����(������ �ִ��� / ��������) �ʱ�ȭ
        isPressed = false;
        holding = false;



        //if (!isLeft)
        //{
        //    transform.localRotation = Quaternion.Euler(0, 180, 0);
        //}
        //else
        //{
        //    transform.localRotation = Quaternion.Euler(0,0, 0);
        //}

        // �ð� ����, �ִϸ����� ��� �� �̹��� ���
        enabledTime = AudioSettings.dspTime;
        animator.Play("Idle");
        image.enabled = true;
    }

    void Update()
    {
        currentTime = AudioSettings.dspTime;
        

        // ���� ���� ���� ���¿���, ������ �ð����� 0.5��Ʈ ���Ŀ� ������� 
        if (!isPressed && currentTime >= checkTime + 0.5 * timeUnit)
        {
            if (!isPressed)
            {
                isPressed = true;
                effectController.JudgeEffect("Dismiss");
            }

            HideImage();
            endNote.HideImage();
        }

        // ����Ÿ�� 1�� �ڿ�, ��Ʈ Ǯ�� �������
        if (currentTime >= endTime + 1)
        {
            NotePool.instance.longQueue.Enqueue(gameObject);
            gameObject.SetActive(false);
            
            endNote.Finish();

        }


        // ���� �ִϸ��̼�
        float lerpValue = (float)((currentTime - enabledTime) / (checkTime - enabledTime));
        lerpValue = Mathf.Clamp01(lerpValue);
        image.color = Color.Lerp(white, transparent, 1 - lerpValue);
    }


    // ��Ʈ�� ��ġ�� ������ �ֽ��ϴ�.
    public void SetNoteInfo(int i, float x, float y, double t, double t2, double t3, bool l, EndNote ed)
    {
        this.noteNum = i;
        this.posX = x;
        this.posY = y;
        this.checkTime = t;
        this.endTime = t2;
        this.timeUnit = t3;
        this.isLeft = l;
        this.endNote = ed;
    }

    public void HideImage()
    {
        image.enabled = false;
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
        holding = true;
        stageManager.noteDown(noteNum, true);
        double pressTime = AudioSettings.dspTime;
        double[] checkList = new double[] { 0.08, 0.13 };

        for (int x = 0; x < checkList.Length; x++)
        {
            if (Math.Abs(pressTime - checkTime) <= checkList[x])
            {
                HideImage();

                effectController.NoteHitEffect();
                if (x == 0) effectController.JudgeEffect("Deadly");
                else effectController.JudgeEffect("Delicate");
                scoreManager.IncreaseCombo(true);
                scoreManager.IncreaseScore(x);

                return;
            }
        }

        effectController.NoteHitEffect();
        effectController.JudgeEffect("Discord");
        scoreManager.IncreaseCombo(false);
        scoreManager.IncreaseScore(2);

        return;

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        holding = false;
        stageManager.noteDown(noteNum, false);
        double taptime = AudioSettings.dspTime;
        endNote.Check(taptime);
        HideImage();
        endNote.HideImage();
    }
}