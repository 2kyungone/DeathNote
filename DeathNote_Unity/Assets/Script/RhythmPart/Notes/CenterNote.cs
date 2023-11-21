using System;
using System.Collections;

using UnityEngine;
using UnityEngine.EventSystems;

using UnityEngine.UI;

public class CenterNote : MonoBehaviour
{
    public Image image; // �̹���
    [SerializeField] Sprite original;
    [SerializeField] Sprite change;

    public Color white = Color.white; // ���� ����
    public Color transparent; // ��ǥ ���� (ȸ��)

    private float posX = 0; // ��Ʈ�� ���� ��ġ(x��ǥ)
    private float posY = 0; // ��Ʈ�� ���� ��ġ(y��ǥ)

    private double checkTime = 0;
    private double currentTime = 0;
    private double enabledTime = 0;

    public float lerpValue;

    void Awake()
    {
        image = GetComponentInChildren<Image>();
        transparent = new Color(white.r, white.g, white.b, 0); // �������

    }
    void OnEnable()
    {
        image.color = Color.Lerp(white, transparent, 1); // ��������� ����
        image.sprite = original; // ��������Ʈ �������� ����

        transform.localPosition = new Vector3(posX, posY); // ��Ʈ�� ��ġ�� ����
        transform.localScale = Vector3.one; // ������ ����

        enabledTime = AudioSettings.dspTime;
    }

    void Update()
    {
        currentTime = AudioSettings.dspTime; // ���� �ð�
        lerpValue = Mathf.Clamp01((float)((currentTime - enabledTime) / (checkTime - enabledTime))); // ����(Clamp�� 0~1�� ����)

        // ���� �ִϸ��̼�
        image.color = Color.Lerp(white, transparent, 1 - lerpValue);

        if(currentTime >= checkTime)
        {
            image.sprite = change;
        }

    }

    // ��Ʈ�� �̹����� ���۴ϴ�.
    public void HideImage()
    {
        image.enabled = false;
    }

    public void SetNoteInfo(float x, float y, double t)
    {
        this.posX = x;
        this.posY = y;
        this.checkTime = t;
    }

    public void Finish()
    {
        // NotePool.instance.centerQueue.Enqueue(gameObject);
        gameObject.SetActive(false);
    }


}