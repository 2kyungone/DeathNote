using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;
    public int[] scoreBonus = new int[5]; // �������ʽ�
    public float[] scoreCrit = { 1.0f, 1.0f, 1.0f, 1.0f, 1.0f }; // ũ��Ƽ��
    public int comboBonus = 0; // �޺� �߰�����
    public int comboCool = 0; // �޺� ��ȭ
    public float comboChance = 0; // �޺� �Ȳ����
    public float addTime = 0; // ���ʽ���Ʈ �߰��ð�
    public float addLevel = 1; // ���ʽ����� ����
    
    public float expBonus ; // ����ġ
    public float expCrit; // ����ġ�ν�Ʈ
    public float[] worldBonus = new float[7]; // 1����


    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
