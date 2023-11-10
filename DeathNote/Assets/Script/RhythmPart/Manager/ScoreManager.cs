using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    SoulManager soulManager;
    MusicManager musicManager;
    [SerializeField] public TextMeshProUGUI score = null;
    [SerializeField] public TextMeshProUGUI grade = null;

    private int totalNote;
    private int increaseScore = 10; // �뷡�� �⺻ ��ġ
    private int[] scoreBonus;
    private int[] scoreCrit;
    private int[] comboBonus;
    public int currentScore = 0; // ���� ����
    public int currentCombo = 0; // ���� �޺�
    public int maxCombo = 0; // �ִ� �޺�
    public int totalPerfect = 0; // �� ����Ʈ
 
    public int totalPercent = 0; // �� �ۼ�Ʈ


    void Start()
    {
        instance = this;
        score.text = "000,000"; // ���ھ� �ؽ�Ʈ �ʱ�ȭ
        grade.text = "0.00%"; // �׷��̵� �ؽ�Ʈ �ʱ�ȭ
    }

    

    // ������ �ø��� �޼���
    public void IncreaseScore(int percent)
    {
        totalPercent += percent; // �ۼ�Ʈ ������ ����
        
        if (percent == 100) totalPerfect++; // ����Ʈ�� ��� ����Ʈ�� ����

        int basicScore = (increaseScore * percent); // �⺻����x��� + �������ʽ� + �⺻���ʽ�
        float critical = (float)(1  / 100.0); // 1 + (�������ʽ�+�⺻���ʽ�)/100      
        int comboScore = (currentCombo / 50) * 10; // �޺��� 50�� ���� �޺����� 10���� �߰� 
        
        currentScore += (int)(basicScore * critical + comboScore);
        score.text = currentScore.ToString("000,000");
        Debug.Log(totalPercent);
        float realGrade = (float)totalPercent / (MusicManager.instance.totalNote * 100);
        grade.text = realGrade.ToString("F2")+"%";
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
