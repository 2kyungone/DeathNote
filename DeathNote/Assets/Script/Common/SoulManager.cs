using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;


// ����
[System.Serializable]
public class Soul
{
    public int id; // ������
    public string name; // �̸�
    public int equip; // ���� ��ġ
    public int[] parameters; // ��ų1, ��ų2, ��ų3, ����
    public int[] customizes; // �ٵ�, ��, �Ǽ�����, ��Ų
    public int[] emotions; // 6���� ��ġ
    public int revive; // �� ȯ�� Ƚ��
    public int garden; // ���� ���� ��ġ

    public Soul(string name, int equip, int[] parameters, int[] customizes, int[] emotions, int revive, int garden)
    {
        this.name = name;
        this.equip = equip;
        this.parameters = parameters;
        this.customizes = customizes;
        this.emotions = emotions;
        this.revive = revive;
        this.garden = garden;
    }
}

public class GardenData
{
    public int id;
    public int type;
}

public class SoulManager : MonoBehaviour
{
    public static SoulManager instance;
    [SerializeField] public List<Soul> Souls;
    public Soul jumpSoul;
    
    public Soul[] Equip // �� ����
    {
        get; private set;
    }


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
        
        // ���� ���� �ʱ�ȭ
        Equip = new Soul[16];
    }
    
    // �ҿ��� ���Ӱ� �߰��մϴ�.
    public void AddSoul(Soul soul)
    {
        UserData userData = UserManager.instance.userData;
        userData.souls.Add(soul);

        String token = JsonUtility.ToJson(userData);

        PlayerPrefs.SetString("UserData", token);
        UserManager.instance.SaveData();
    }



    // �� ������ ���
    public void SetSoul(List<Soul> souls)
    {
        // ������ ������ ���, Equip ������Ƽ�� ����
        foreach(Soul soul in souls)
        {
            if (soul.equip != 0)
            {
                Equip[soul.equip - 1] = soul;
            }       
        }

        Souls = souls;
    }

    // ���� ����
    public void reviveSouls(int idx, int now)
    {
        Debug.Log("���� ȯ��");
    }

    //JSON ������ ������ �ڷ�ƾ


}
