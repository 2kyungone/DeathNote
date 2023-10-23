using System;
using System.Collections;

using UnityEngine;
using UnityEngine.EventSystems;

using UnityEngine.UI;

public class WideNote : MonoBehaviour, IPointerDownHandler
{
    private Image noteImage;
    public Color originalColor = Color.white; // ���� ����
    public Color targetColor = Color.black; // ��ǥ ���� (ȸ��)

    // ��Ʈ�� ���� ��ġ(x��ǥ)
    private float posX = 0;
    // ��Ʈ�� ���� ��ġ(y��ǥ)
    private float posY = 0;
    // ��Ʈ�� ���� �ð�
    public double checkTime = 0;
    public double currentTime = 0;
    public double enabledTime = 0;
    private double collapseTime = 0;
    public float lerpValue;

    Transform image;
    EffectManager effectManager; // ����Ʈ
    ScoreManager scoreManager; // ����
    Animator[] animators;

    void Awake()
    {
        effectManager = transform.GetComponentInChildren<EffectManager>();
        scoreManager = FindObjectOfType<ScoreManager>();
        image = transform.GetChild(2);
    }
    void OnEnable()
    {
        if (noteImage == null)
            noteImage = image.GetComponent<Image>();
        Vector3 newPos = transform.localPosition;

        newPos.x = posX;
        newPos.y = posY;

        image.rotation = Quaternion.Euler(0, 0, 0);
        Color transparentOriginal = new Color(originalColor.r, originalColor.g, originalColor.b, 0); // ���� ���� ���� ����
        noteImage.color = Color.Lerp(originalColor, transparentOriginal, 1);

        float startScale = 0.5f; // ���� ũ��
        float endScale = 1f; // �� ũ��
        float currentScale = Mathf.Lerp(startScale, endScale, 0);
        transform.localScale = new Vector3(currentScale, currentScale, currentScale);
        enabledTime = AudioSettings.dspTime;
        transform.localPosition = newPos;

        noteImage.enabled = true;
    }

    void Update()
    {
        currentTime = AudioSettings.dspTime;
        float lerpValue = (float)((currentTime-enabledTime)/(checkTime - enabledTime));

        if (currentTime >= checkTime + collapseTime)
        {
                NotePool.instance.noteQueue.Enqueue(gameObject);
                gameObject.SetActive(false);
            
        }

        lerpValue = Mathf.Clamp01(lerpValue);

        // ȸ�� �ִϸ��̼�
        image.rotation = Quaternion.Euler(0, 0, lerpValue * 360f);

        // ���� �ִϸ��̼�
        Color transparentOriginal = new Color(originalColor.r, originalColor.g, originalColor.b, 0); // ���� ���� ���� ����
        noteImage.color = Color.Lerp(originalColor, transparentOriginal, 1 - lerpValue);

        float startScale = 0.5f; // ���� ũ��
        float endScale = 1f; // �� ũ��
        float currentScale = Mathf.Lerp(startScale, endScale, lerpValue);
        transform.localScale = new Vector3(currentScale, currentScale, currentScale);

    }


    // ��Ʈ�� �̹����� ���۴ϴ�.
    public void HideImage()
    {
        noteImage.enabled =  false;
    }

    public void SetNoteInfo(float x, float y, double t, double t2)
    {
        this.posX = x;
        this.posY = y;
        this.checkTime = t;
        this.collapseTime = t2;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
   
        double pressTime = AudioSettings.dspTime;
        double[] checkList = new double[] { 0.06, 0.1 };

        for (int x = 0; x < checkList.Length; x++)
        {   
            if (Math.Abs(pressTime - checkTime) <= checkList[x])
            {
                Debug.Log("�ǰ� �־");
                HideImage();

                effectManager.JudgeEffect(x);
                effectManager.NoteHitEffect();
                scoreManager.IncreaseCombo(true);
                scoreManager.IncreaseScore(x);

                return;
            }
        }
    }


}