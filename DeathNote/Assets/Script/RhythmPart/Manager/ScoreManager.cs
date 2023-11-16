using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI score = null;
    [SerializeField] public TextMeshProUGUI grade = null;
    [SerializeField] Image[] blur = null;
    [SerializeField] Image background = null; 
   
    private int totalNote; // ��ü ��Ʈ ����


    public int currentScore = 0; // ���� ����
    public int currentCombo = 0; // ���� �޺�
    public int maxCombo = 0; // �ִ� �޺�
    public int totalPerfect = 0; // �� ����Ʈ
    public int totalInspirit = 0; // �� ���


    public int totalPercent = 0; // �� �ۼ�Ʈ

    public Color white = Color.white; // ��ο� ��
    public Color[] originColor;
    public Color[] finalColor;
    public Color endparent; // ���� ����
    public Color transparent; // ó�� ���� (�����)


    void Start()
    {
        totalNote = MusicManager.instance.totalNote;//��ü ��Ʈ

        originColor = new Color[12];
        finalColor = new Color[12];
        transparent = new Color(Color.white.r, Color.white.g, Color.white.b, 0);
        endparent = new Color(Color.white.r, Color.white.g, Color.white.b, 0.8f);

        for (int i = 0; i < originColor.Length; i++)
        {
            originColor[i] = blur[i].color; // ���� Į�� originColor[i]
        }

        for (int i = 0; i < 6; i++)
        {
            Soul soul = SkillManager.instance.equip[i];
            // ���� Į�� �ٽ� �缳��
            if (soul == null)
            {
                blur[2 * i].color = new Color(originColor[11].r, originColor[11].b, originColor[11].b, 0);
                blur[2 * i + 1].color = new Color(originColor[11].r, originColor[11].b, originColor[11].b, 0);
            }
            else
            {
                int body = soul.customizes[2];
                blur[2 * i].color = new Color(originColor[body].r, originColor[body].b, originColor[body].b, 0);
                int eyes = soul.customizes[3];
                blur[2 * i + 1].color = new Color(originColor[eyes].r, originColor[eyes].b, originColor[eyes].b, 0);
            }
        }

        for(int i = 0; i < 12; i++)
        {
            originColor[i] = new Color(blur[i].color.r, blur[i].color.g, blur[i].color.b, 0); // ���ۻ���
            finalColor[i] = new Color(blur[i].color.r, blur[i].color.g, blur[i].color.b, 0.3f); // �������
        }

        score.text = "000,000"; // ���ھ� �ؽ�Ʈ �ʱ�ȭ
        grade.text = "0.00%"; // �׷��̵� �ؽ�Ʈ �ʱ�ȭ
    }

    // ������ �ø��� �޼���
    public void IncreaseScore(int percent, int bonus, int combo, int perfectB, int gold)
    {
        totalPercent += percent; // �ۼ�Ʈ ������ ����
        float basicScore = (percent / 10.0f) + bonus; // �⺻����x��� + �������ʽ� + �⺻���ʽ�  

        if (percent == 100)
        {
            totalPerfect++; // ����Ʈ�� ��� ����Ʈ�� ����
            basicScore += perfectB;

        }
            
        if(currentCombo != 0) // 50 �޺����� �߰�����
        {
            basicScore += combo;
        }

        totalInspirit += gold;

        currentScore += (int)(basicScore); // ���� ���ھ �߰�
        score.text = currentScore.ToString("000,000");  // ����
      
        float realGrade = (float)totalPercent / totalNote; // �����
        realGrade = Mathf.Round(realGrade * 1000f) / 1000f; // �Ҽ��� ��°�ڸ����� �ݿø�
        grade.text = realGrade.ToString("F2")+"%"; // ���


        for (int i = 0; i < 12; i++)
        {
            // lerpValue ���

            // �� blur[i]�� ���� �ִ� lerpValue ���� ���� (i�� �����Կ� ���� �� õõ�� �ٲ��)
            float maxLerpRange = 100f - (i * 5f); // i�� �����Ҽ��� maxLerpRange ����

            // baseLerpValue�� ������ ������ �°� �����ϸ�
            float scaledLerpValue = Mathf.Clamp01(realGrade / maxLerpRange);

            // ���� ����
            blur[i].color = Color.Lerp(originColor[i], finalColor[i], scaledLerpValue);
            if(i==0) background.color = Color.Lerp(transparent, endparent, scaledLerpValue);
            // ũ�� ����
            Vector3 newScale = Vector3.one * 4 * scaledLerpValue; // Vector3.one�� (1, 1, 1)�� �ǹ�
            blur[i].GetComponent<RectTransform>().localScale = newScale;

        }


    }

    // �޺��� �ø��ų� �ʱ�ȭ 

    public void IncreaseCombo(bool isIncrease)
    {
        if (isIncrease)
        {
            currentCombo++;
            if (currentCombo > maxCombo) maxCombo++;

        }
        else
        {
            currentCombo = 0;
        }
    }
}


/**
 * ���� �ý���
 */

//public class ScoreManager : MonoBehaviour
//{
//    public static ScoreManager instance;
//    [SerializeField] TextMeshProUGUI score = null;

//    private int increaseScore = 100; // �뷡�� �⺻ ��ġ
//    private int[] weight = { 5, 3, 1 }; // ������ ���
//    private int[] scoreBonus;
//    private int[] scoreCrit;
//    private int[] comboBonus;
//    public int currentScore = 0; // ���� ����
//    public int currentCombo = 0; // ���� �޺�


//    void Start()
//    {
//        instance = this;
//        score.text = "000,000,000"; // ���ھ� �ؽ�Ʈ �ʱ�ȭ
//        scoreBonus = SkillManager.instance.scoreBonus;
//        scoreCrit = SkillManager.instance.scoreCrit;
//        comboBonus = SkillManager.instance.comboBonus;
//    }



//    // ������ �ø��� �޼���
//    public void IncreaseScore(int judge, bool safe)
//    {
//        int basicScore = (increaseScore * weight[judge]) + scoreBonus[judge] + scoreBonus[2]; // �⺻����x��� + �������ʽ� + �⺻���ʽ�
//        float critical = (float)(1 + (scoreCrit[judge] + scoreCrit[2]) / 100.0); // 1 + (�������ʽ�+�⺻���ʽ�)/100
//        int comboScore = (currentCombo/10) * 10 + comboBonus[0]; // �޺��� �����ڸ�����ŭ ����( 251 -> 250) + �⺻���ʽ�
//        if (currentCombo != 0 && currentCombo % 50 == 0 && !safe) comboScore += comboBonus[1]; // 50�� ���ʽ� �߰� : 0�̰ų�, safe������ ��쿣 �ȵ�
//        currentScore += (int)(basicScore*critical+comboScore);
//        score.text = currentScore.ToString("N0").PadLeft(9, '0');
//    }

//    // �޺��� �ø��ų� �ʱ�ȭ 

//    public void IncreaseCombo(bool isIncrease)
//    {
//        if (isIncrease)
//        {
//            currentCombo++;

//        }
//        else
//        {
//            currentCombo = 0;
//        }
//    }
//}
