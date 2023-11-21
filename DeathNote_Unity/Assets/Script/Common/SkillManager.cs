using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// ��ų �Ŵ����ԴϤ�.
public class Skill
{
    public string name;
    public int tier;
    public string description;
    public int percent;
    public int[] bonus;
    public int[] combo;
    public int[] perfect;
    public int[] gold;

    public Skill(string name, int tier, string descrition, int percent, int[] bonus, int[] combo, int[] perfect, int[] gold)
    {
        this.name = name; // ��ų��
        this.tier = tier; // Ƽ��
        this.description = descrition; // ��ų����
        this.percent = percent; // �ߵ� Ȯ��
        this.bonus = bonus; // ���ʽ�
        this.combo = combo; // �޺� ��� ����
        this.perfect = perfect; // �ִ����� �߰�����
        this.gold = gold;
    }
}
public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;
    // ������ �ҿ� (�� 16��)
    public List<Soul> equip;
   
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // �� ��ȯ�� �ı����� �ʵ��� ����
        }
        else if (instance != this)
        {
            Destroy(gameObject); // �ߺ� �ν��Ͻ��� �ı�
        }

        equip = new List<Soul>(6);
        for(int i = 0; i < 6; i++)
        {
            equip.Add(null);
        }
        Debug.Log(equip[1]);
        Debug.Log("�������o��");
    }


    // ��ų�� �� ȿ���� ��ȯ�ϴ� �޼���
    public Skill GetSkillInfo(int idx)
    {
        switch (idx)
        {
            case 0:
                Console.WriteLine("num:10");
                return new Skill("�� ��Ʈ", 0, "30%�� Ȯ���� ��Ʈ���� ����� ���� ���ʽ� ������ ��´�.",
                    30, new int[] { 0, 10 }, new int[] { 0, 0 }, new int[] { 0, 0 }, new int[] { 0, 0 });
            case 1:
                return new Skill("�״���� ���", 0, "30%�� Ȯ���� ������ ����� ���� ���ʽ� ������ ��´�.",
                    30, new int[] { 1, 10 }, new int[] { 0, 0 }, new int[] { 0, 0 }, new int[] { 0, 0 });
            case 2:
                return new Skill("Ż����", 0, "30%�� Ȯ���� ���忡 ����� ���� ���ʽ� ������ ��´�.",
                    30, new int[] { 2, 10 }, new int[] { 0, 10 }, new int[] { 0, 0 }, new int[] { 0, 0 });
            case 3:
                return new Skill("���������", 0, "30%�� Ȯ���� ����Կ� ����� ���� ���ʽ� ������ ��´�.",
                    30, new int[] { 3, 10 }, new int[] { 0, 0 }, new int[] { 0, 0 }, new int[] { 0, 0 });
            case 4:
                return new Skill("�����Ӵ�", 0, "30%�� Ȯ���� ������ ����� ���� ���ʽ� ������ ��´�.",
                    30, new int[] { 4, 10 }, new int[] { 0, 0 }, new int[] { 0, 0 }, new int[] { 0, 0 });
            case 5:
                return new Skill("��ȭ�İ����̿���", 0, "30%�� Ȯ���� �����Կ� ����� ���� ���ʽ� ������ ��´�.",
                    30, new int[] { 5, 10 }, new int[] { 0, 0 }, new int[] { 0, 0 }, new int[] { 0, 0 });
            case 6:
                return new Skill("��Ʈ����ŷ", 0, "30%�� Ȯ���� ��Ʈ���� ����� ���� �޺� ������ ��´�.",
                    30, new int[] { 0, 0 }, new int[] { 0, 20 }, new int[] { 0, 0 }, new int[] { 0, 0 });
            case 7:
                return new Skill("�׶����", 0, "30%�� Ȯ���� ������ ����� ���� �޺� ������ ��´�.",
                    30, new int[] { 0, 0 }, new int[] { 1, 20 }, new int[] { 0, 0 }, new int[] { 0, 0 });
            case 8:
                return new Skill("�����", 0, "30%�� Ȯ���� ���忡 ����� ���� �޺� ������ ��´�.",
                    30, new int[] { 0, 0 }, new int[] { 2, 20 }, new int[] { 0, 0 }, new int[] { 0, 0 });
            case 9:
                return new Skill("��ٿ�", 0, "30%�� Ȯ���� ����Կ� ����� ���� �޺� ������ ��´�.",
                    30, new int[] { 0, 0 }, new int[] { 3, 20 }, new int[] { 0, 0 }, new int[] { 0, 0 });
            case 10:
                return new Skill("����յ�", 0, "30%�� Ȯ���� �����Կ� ����� ���� �޺� ������ ��´�.",
                    30, new int[] { 0, 0 }, new int[] { 5, 20 }, new int[] { 0, 0 }, new int[] { 0, 0 });
            case 11:
                return new Skill("�����ӹ�Ʈ��", 0, "30%�� Ȯ���� ������ ����� ���� �޺� ������ ��´�.",
                    30, new int[] { 0, 0 }, new int[] { 4, 20 }, new int[] { 0, 0 }, new int[] { 0, 0 });
            case 100:
                return new Skill("��Ʈ�� ���� �ð�", 1, "30%�� Ȯ���� ��Ʈ���� ����� ����Ʈ ������ ��´�.",
                    30, new int[] { 0, 0 }, new int[] { 0, 0 }, new int[] { 0, 40 }, new int[] { 0, 0 });
            case 101:
                return new Skill("��纣��", 1, "30%�� Ȯ���� ������ ����� ����Ʈ ������ ��´�.",
                    30, new int[] { 0, 0 }, new int[] { 0, 0 }, new int[] { 1, 40 }, new int[] { 0, 0 });
            case 102:
                return new Skill("���������", 1, "30%�� Ȯ���� ���尨�� ����� ����Ʈ ������ ��´�.",
                    30, new int[] { 0, 0 }, new int[] { 0, 0 }, new int[] { 2, 40 }, new int[] { 0, 0 });
            case 103:
                return new Skill("ġ���� ���", 1, "30%�� Ȯ���� ����Կ� ����� ����Ʈ ������ ��´�.",
                    30, new int[] { 0, 0 }, new int[] { 0, 0 }, new int[] { 3, 40 }, new int[] { 0, 0 });
            case 104:
                return new Skill("�αٴ�� ��Ʈ", 1, "30%�� Ȯ���� ������ ����� ����Ʈ ������ ��´�.",
                    30, new int[] { 0, 0 }, new int[] { 0, 0 }, new int[] { 4, 40 }, new int[] { 0, 0 });
            case 105:
                return new Skill("�ż��� �����", 1, "30%�� Ȯ���� �����Կ� ����� ����Ʈ ������ ��´�.",
                     30, new int[] { 0, 0 }, new int[] { 0, 0 }, new int[] { 5, 40 }, new int[] { 0, 0 });
            case 106:
                return new Skill("������ ��Ʈ", 1, "20%�� Ȯ���� ��Ʈ���� ����� ������ ��´�.",
                    20, new int[] { 0, 0 }, new int[] { 0, 0 }, new int[] { 0, 0 }, new int[] { 0, 20 });
            case 107:
                return new Skill("�յ�ȣ��", 1, "20%�� Ȯ���� ������ ����� ���� ��´�.",
                    20, new int[] { 0, 0 }, new int[] { 0, 0 }, new int[] { 0, 0 }, new int[] { 1, 20 });
            case 108:
                return new Skill("���庸�ܹ���", 1, "20%�� Ȯ���� ���尨�� ����� ������ ��´�.",
                    20, new int[] { 0, 0 }, new int[] { 0, 0 }, new int[] { 0, 0 }, new int[] { 2, 20 });
            case 109:
                return new Skill("���", 1, "20%�� Ȯ���� ����Կ� ����� ������ ��´�.",
                    20, new int[] { 0, 0 }, new int[] { 0, 0 }, new int[] { 0, 0 }, new int[] { 3, 20 });
            case 110:
                return new Skill("�����������", 1, "20%�� Ȯ���� ������ ����� ������ ��´�.",
                    20, new int[] { 0, 0 }, new int[] { 0, 0 }, new int[] { 0, 0 }, new int[] { 4, 20 });
            case 111:
                return new Skill("�߳��������", 1, "20%�� Ȯ���� �����Կ� ����� ������ ��´�.",
                     20, new int[] { 0, 0 }, new int[] { 0, 0 }, new int[] { 0, 0 }, new int[] { 5, 20 });
            case 200:
                return new Skill("�ι����� ����", 2, "��Ʈ�� ����� ���ʽ��� ���忡 ����� �޺� ������ ��´�.",
                    30, new int[] { 0, 50 }, new int[] { 2, 20 }, new int[] { 0, 0 }, new int[] { 0, 0 });
            case 201:
                return new Skill("��¼��� ����", 2, "������ ����� ���ʽ��� ����Կ� ����� �޺� ������ ��´�.",
                    30, new int[] { 1, 50 }, new int[] { 3, 20 }, new int[] { 0, 0 }, new int[] { 0, 0 });
            case 202:
                return new Skill("�����������Ѱ�", 2, "������ ����� ���ʽ��� �����Կ� ����� �޺� ������ ��´�.",
                    30, new int[] { 4, 50 }, new int[] { 5, 20 }, new int[] { 0, 0 }, new int[] { 0, 0 });
            default:
                Console.WriteLine(" num:?");
                return null;
        }
    }

    public int GetSkill(Soul turnSoul, ClickNote note)
    {
         
        // ������ ������ ��ų�� ����
        for (int i = 0; i < 3; i++)
        {
            // i ��° ��ų
            int idx = turnSoul.parameters[i];
            Skill now = SkillManager.instance.GetSkillInfo(idx);
            // �ߵ� Ȯ���� 0�ۼ�Ʈ�� ���� ��ų
            if (now.percent == 0) continue;
            // 0~99 ������ ���� ����
            int random = UnityEngine.Random.Range(0, 100);
            // Ȯ�������� �ߵ��ϸ�
            if (now.percent > random)
            {
                note.bonus = (int)(turnSoul.emotions[now.bonus[0]] * now.bonus[1] / 100.0f); // �ڿ��� ���
                note.combo = (int)(turnSoul.emotions[now.combo[0]] * now.combo[1] / 100.0f);
                note.perfect = (int)(turnSoul.emotions[now.perfect[0]] * now.perfect[1] / 100.0f);
                note.gold = (int)(turnSoul.emotions[now.gold[0]] * now.gold[1] / 100.0f);

                return UnityEngine.Random.Range(0,6);
            }          

        }
        return -1;
    }

    // ��� ��ȯ
    public void SetEquip(List<Soul> soul)
    {
        equip = soul;
    }


}