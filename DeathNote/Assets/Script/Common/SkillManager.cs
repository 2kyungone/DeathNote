using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Skill
{
    public string name;
    public string tier;
    public string description;
    public int percent;
    public int[] bonus;
    public int[] combo;
    public int[] perfect;

    public Skill(string name, string tier, string descrition, int percent, int[] bonus, int[] combo, int[] perfect)
    {
        this.name = name; // ��ų��
        this.tier = tier; // Ƽ��
        this.description = descrition; // ��ų����
        this.percent = percent; // �ߵ� Ȯ��
        this.bonus = bonus; // ���ʽ�
        this.combo = combo; // �޺� ��� ����
        this.perfect = perfect; // �ִ����� �߰�����
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
    }

    void Start()
    {
        equip = SoulManager.instance.Souls;
    }

    // ��ų�� �� ȿ���� ��ȯ�ϴ� �޼���
    public Skill GetSkill(int idx)
    {
        switch (idx)
        {
            case 1:
                Console.WriteLine("num:10");
                return new Skill("�� ��Ʈ", "�Ϲ�", "30%�� Ȯ���� ��Ʈ���� ����� ���� ���ʽ� ������ ��´�.",
                    30, new int[] { 0, 10 }, new int[] { 0, 0 }, new int[] { 0, 0 });
            case 2:
                return new Skill("�״���� ���", "�Ϲ�", "30%�� Ȯ���� ������ ����� ���� ���ʽ� ������ ��´�.",
                    30, new int[] { 0, 0 }, new int[] { 0, 0 }, new int[] { 1, 10 });
            case 3:
                return new Skill("�޺���", "�Ϲ�", "50%�� Ȯ���� ���忡 ����� ���� �޺� ������ ��´�.",
                    30, new int[] { 0, 0 }, new int[] { 2, 10 }, new int[] { 0, 0 });
            case 4:
                return new Skill("�����", "�Ϲ�", "30%�� Ȯ���� ����Կ� ����� ���� �޺� ������ ��´�.",
                    30, new int[] { 0, 0 }, new int[] { 3, 10 }, new int[] { 0, 0 });
            case 5:
                return new Skill("�����Ӵ�", "�Ϲ�", "30%�� Ȯ���� ������ ����� ���� ���ʽ� ������ ��´�.",
                    30, new int[] { 0, 0 }, new int[] { 0, 0 }, new int[] { 4, 10 }, new int[] { 0, 0 }, new int[] { 0, 0 });
            case 6:
                return new Skill("��ȭ�İ����̿���", "�Ϲ�", "30%�� Ȯ���� �����Կ� ����� ���� ���ʽ� ������ ��´�.",
                    30, new int[] { 0, 0 }, new int[] { 0, 0 }, new int[] { 5, 10 }, new int[] { 0, 0 }, new int[] { 0, 0 });
            default:
                Console.WriteLine(" num:?");
                return null;
        }
    }

    // ��� ��ȯ
    public void SetEquip(List<Soul> soul)
    {
        equip = soul;
    }


}