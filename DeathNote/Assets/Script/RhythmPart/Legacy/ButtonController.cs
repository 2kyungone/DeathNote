using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Image image; // ��ư ������Ʈ�� ����� �̹���
    public Sprite defaultButton; // ��������Ʈ �⺻��
    public Sprite pressedButton; // ���������� ��������Ʈ
    public bool isLeft; // ��������, ����������
    public int number; // ������� 0,1,2,3
    public int songSpeed; // ���ǵ�

    EffectController effectManager; // ����Ʈ
    ScoreManager scoreManager; // ����

    public Queue<GameObject> noteList = new Queue<GameObject>();

    public KeyCode keyToPress; // �������� Ű

    void Start()
    {
        effectManager = transform.GetComponentInChildren<EffectController>();
        scoreManager = FindObjectOfType<ScoreManager>();

        image = GetComponent<Image>();    
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyToPress)){ // Ű�� ������ ��
            CheckTiming(isLeft);
            image.sprite = pressedButton;
        }

        if (Input.GetKeyUp(keyToPress)) // Ű�� ���� ��
        {
            image.sprite = defaultButton;
        }
    }

    public void CheckTiming(bool isLeft)
    {
        double pressTime = AudioSettings.dspTime;
        int effectIdx = isLeft ? 0 : 1;
        bool findNote = false;
        while (!findNote)
        {
            if (noteList.TryPeek(out GameObject result))
            {
                LegacyNote note = result.GetComponent<LegacyNote>();
                double checkTime = note.startTime + 2;
                if (pressTime - checkTime > 0.04)
                {
                    noteList.Dequeue();
                    continue;

                }
                double[] checkList = new double[] { 0.06, 0.08, 0.12, 0.2 };

                for (int x = 0; x < checkList.Length; x++)
                {
                    if (Math.Abs(pressTime - checkTime) <= checkList[x])
                    {
                        Debug.Log(isLeft + ":" + note.beatNum);
                        note.HideImage();
                        noteList.Dequeue();
                        effectManager.JudgeEffect("Discord");
                        effectManager.NoteHitEffect();
                        if (x < 3) scoreManager.IncreaseCombo(true);
                        else scoreManager.IncreaseCombo(false);
                        scoreManager.IncreaseScore(x);

                        return;
                    }
                }
                findNote = true;
            }
            else { findNote = true; }
        }
        
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        CheckTiming(isLeft);
        image.sprite = pressedButton;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        image.sprite = defaultButton;
    }
}
