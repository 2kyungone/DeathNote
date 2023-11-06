using System;
using System.Collections;

using UnityEngine;
using UnityEngine.EventSystems;

using UnityEngine.UI;

public class CenterNote : MonoBehaviour
{
    public Color white = Color.white; // ���� ����
    public Color transparent; // ��ǥ ���� (ȸ��)

    // ��Ʈ�� ���� ��ġ(x��ǥ)
    private float posX = 0;
    // ��Ʈ�� ���� ��ġ(y��ǥ)
    private float posY = 0;
    // ��Ʈ�� ���� �ð�
    private float length = 0;
    private bool isLeft;

    public double enterTime = 0;
    public double checkTime = 0;
    public double currentTime = 0;
    public double enabledTime = 0;

    public float lerpValue;

    [SerializeField] Image image;
    [SerializeField] Sprite original;
    [SerializeField] Sprite change;
    Animator animator;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        image = GetComponentInChildren<Image>();
        
    }
    void OnEnable()
    {
        // ��������� ����
        image.color = Color.Lerp(white, transparent, 1);
        image.sprite = original;

        // ��Ʈ�� ��ġ�� �����ϰ�, �������� 1�� ����
        transform.localPosition = new Vector3(posX, posY);
        transform.localScale = Vector3.one;
        

        if (!isLeft)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

        enabledTime = AudioSettings.dspTime;
        animator.Play("Idle");
    }

    void Update()
    {
        currentTime = AudioSettings.dspTime;

        float lerpValue = (float)((currentTime - enabledTime) / (enterTime - enabledTime));
        lerpValue = Mathf.Clamp01(lerpValue);
        image.color = Color.Lerp(white, transparent, 1 - lerpValue);

    }


    // ��Ʈ�� �̹����� ���۴ϴ�.
    public void HideImage()
    {
        image.enabled = false;
    }

    public void Check()
    {
        image.sprite = change;
    }

    public float Length()
    {
        return length;
    }

    public void SetNoteInfo(float x, float y, double e, double t, float len, bool l)
    {
        this.posX = x;
        this.posY = y;
        this.enterTime = e;
        this.checkTime = t;
        this.isLeft = l;
        this.length = len;
    }


    public void Finish()
    {
        NotePool.instance.centerQueue.Enqueue(gameObject);
        gameObject.SetActive(false);
    }


}