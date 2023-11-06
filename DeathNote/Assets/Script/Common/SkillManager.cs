using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Soul
{
    public string soulName;
    public bool isEquip;
    public int passive;
    public int[] active;

}

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;
    public List<Soul> souls; // �� ���� ����
    public int[] equip = { -1, -1 }; // ���� ���� ����, ������ ���� ����
    public int[] scoreBonus = { 0, 0, 0, 0 }; // �������ʽ�(deadly, delicate, discord, bonus)
    public int[] scoreCrit = { 0, 0, 0, 0 }; // ũ��Ƽ��Ȯ��(deadly, delicate, discord, bonus)
    public int comboUnbreakRatio = 0; // �޺��� �Ȳ��� Ȯ��
    public int[] comboBonus = { 0, 0 }; // �޺� �߰�����(1�޺� ��, 50�޺� ��)
    public float judgeBonus = 0.0f; // ���� ��ȭ
    public int[] expBonus = { 0, 0, 0, 0, 0, 0 };
    public float[] worldBonus = { 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f }; // ���� ���ʽ�

    private int[][] skillSheet = new int[26][];

    void Awake()
    {
        instance = this;
        skillSheet[1] = new int[] { 50, 80, 130, 200 }; // �⺻ �߰�����
        skillSheet[2] = new int[] { 100, 200, 300, 500 }; // deadly �߰�����
        skillSheet[3] = new int[] { 50, 100, 150, 300 }; // delicate �߰�����
        skillSheet[4] = new int[] { 20, 40, 60, 90 }; // discord �߰�����
        skillSheet[5] = new int[] { 2, 4, 7, 10 }; // �޺� �Ȳ��� Ȯ�� (2% ~)
        skillSheet[6] = new int[] { 10, 20, 30, 40 }; // �޺� �߰� ����
        skillSheet[7] = new int[] { 2000, 4000, 6000, 8000 }; // 50�޺� �߰� ����
        skillSheet[8] = new int[] { 5, 10, 15, 20 }; // ���� ��ȭ(0.005�� ~)
        skillSheet[9] = new int[] { 5, 10, 15, 20 }; // �⺻ ũ��Ƽ��Ȯ��(5% ~)
        skillSheet[10] = new int[] { 2, 4, 6, 8 }; // deadly ũ��Ƽ��(2.5% ~)
        skillSheet[11] = new int[] { 2, 4, 6, 8 }; // delicate ũ��Ƽ��(2.5% ~)
        skillSheet[12] = new int[] { 2, 4, 6, 8 }; // discord ũ��Ƽ��(2.5% ~)
        skillSheet[13] = new int[] { 1, 2, 3, 4 }; // ����
        skillSheet[14] = new int[] { 1, 2, 3, 4 }; // �ų�
        skillSheet[15] = new int[] { 1, 2, 3, 4 }; // ����
        skillSheet[16] = new int[] { 1, 2, 3, 4 }; // ������ ũ��Ƽ�� (5%~)
        skillSheet[17] = new int[] { 1, 2, 3, 4 }; // dismiss discord�� ���� (5% ~)
        skillSheet[18] = new int[] { 1, 2, 3, 4 }; // ������
        skillSheet[19] = new int[] { 10, 20, 30, 40 }; // ���� 1 �� (1.1�� ~)
        skillSheet[20] = new int[] { 10, 20, 30, 40 }; // ���� 2 �� (1.1�� ~)
        skillSheet[21] = new int[] { 10, 20, 30, 40 }; // ���� 3 �� (1.1�� ~)
        skillSheet[22] = new int[] { 10, 20, 30, 40 }; // ���� 4 �� (1.1�� ~)
        skillSheet[23] = new int[] { 10, 20, 30, 40 }; // ���� 5 �� (1.1�� ~)
        skillSheet[24] = new int[] { 10, 20, 30, 40 }; // ���� 6 �� (1.1�� ~)
        skillSheet[25] = new int[] { 10, 20, 30, 40 }; // ��ü �� (1.1�� ~)

        // Http ����� ���ؼ�, �� public �ʵ忡 ����
    }
    
    // ��ų�� ������ ����ϴ� �Լ�
    public void calculateSkill()
    {
        int[] skillSum = new int[26];

        foreach(Soul soul in souls)
        {
            int skillType = soul.passive / 10; // �ش��ϴ� ��ų
            int skillLevel = soul.passive % 10; // ��ų ����(Normal, Rare, Mythic, Devil) 

            skillSum[skillType] += skillSheet[skillType][skillLevel];

            // ������ ������ ���� ������
            if (soul.isEquip)
            {
                for(int i = 0; i < 3; i++)
                {
                    skillType = soul.active[i] / 10; // �ش��ϴ� ��ų
                    skillLevel = soul.active[i] % 10; // ��ų ����(Normal, Rare, Mythic, Devil) 

                    skillSum[skillType] += skillSheet[skillType][skillLevel];
                }
            }
            
        }
        
        // ���� ���ʽ�
        for(int i = 0; i < 4; i++)
        {
            scoreBonus[i] = skillSum[i + 1];
        }

        // ũ��Ƽ�� ���ʽ�
        for (int i = 0; i < 4; i++)
        {
            scoreCrit[i] += skillSum[i + 9];
        }

        // �޺� �Ȳ���
        comboUnbreakRatio = skillSum[5];

        // �޺� ���ʽ�
        for (int i = 0; i < 2; i++)
        {
            comboBonus[i] += skillSum[i + 6];
        }

        // ���� ��ȭ
        judgeBonus = (float)skillSum[8] / 1000;

        // ������ ���ʽ�
        for (int i = 0; i < 6; i++)
        {
            expBonus[i] += skillSum[i + 13];
        }

        // ���� ���ʽ�
        for (int i = 0; i < 7; i++)
        {
            worldBonus[i] += skillSum[i + 19];
        }
    }


    // ���� ����
    public void changeSouls(int idx, int now)
    {
        // ���� ���ʿ� �����ϰ� �ִٸ�
        if (equip[idx] != -1)
        {
            // ������ ������ ������ ���·� �ٲٰ� ������ ���µ� ������ ���·� �ٲ�
            souls[equip[idx]].isEquip = false;
            equip[idx] = -1;
        }

        // ���Ӱ� �����ϴ� ������ �ִٸ�
        if (now != -1)
        {
            // ������ ������ ���� ���·� �ٲٰ� ������ ���·� ���� ���·� �ٲ�
            souls[idx].isEquip = true;
            equip[idx] = now;

        }

        calculateSkill();
    }

    public void changeSkill(int idx)
    {
        // idx��°�� ������ skill�� �ٲٰ� ����
    }
}
