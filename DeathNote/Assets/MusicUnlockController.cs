using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static KooksUserManager;

public class MusicUnlockController : MonoBehaviour
{
    KooksUserManager kooksUserManager;
    KooksUserManager.UserData userData;
    private int progressNum = 0;
    public int WorldNum;    // ���� ���° �������� !
    void Start()
    {
        Debug.Log(WorldNum);
        //!!!!!!!!!!!!!@@@@@@@@@@@@@@@@@@@@@@@���ý��丮������ �ش� ���� �������� �̸� ���� �κ� !!!!!!!!!!!!!@@@@@@@@@@@@@@@@@@@@@@@
        //progressNum = PlayerPrefs.GetInt("progress");
        //!!!!!!!!!!!!!@@@@@@@@@@@@@@@@@@@@@@@���ý��丮������ �ش� ���� �������� �̸� ���� �κ� !!!!!!!!!!!!!@@@@@@@@@@@@@@@@@@@@@@@

        kooksUserManager = FindObjectOfType<KooksUserManager>();
        if (kooksUserManager != null)
        {
            userData = kooksUserManager.GetUserData(); // KooksUserManager ��ũ��Ʈ���� UserData�� �����ɴϴ�.
            progressNum = userData.progress;
            GameObject[] Lockers = { GameObject.Find("LockedMusic1"), GameObject.Find("LockedMusic2"), GameObject.Find("LockedMusic3"), GameObject.Find("LockedMusic4") };
            GameObject[] Musics = { GameObject.Find("MusicInform1"), GameObject.Find("MusicInform2"), GameObject.Find("MusicInform3"), GameObject.Find("MusicInform4") };
            if ((progressNum / 100) > WorldNum) 
            {
                //������ �� �����߰���?
                for (int i = 0; i < Lockers.Length; i++)
                {
                    Lockers[i].SetActive(false);
                }
            }
            else if ((progressNum / 100) == WorldNum)
            {
                // 1���ڸ��� ( ����� ���� Ŭ�����ߴ��� ) Ȯ���ϰ� ������߰���?
                if((progressNum % 10) == 0){
                    for (int i = 0; i < 1; i++)
                    {
                        Lockers[i].SetActive(false);
                    }
                    for (int i = 1; i < 4; i++)
                    {
                        Musics[i].SetActive(false);
                    }
                }
                else if ((progressNum % 10) == 1)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        Lockers[i].SetActive(false);
                    }
                    for (int i = 2; i < 4; i++)
                    {
                        Musics[i].SetActive(false);
                    }
                }
                else if ((progressNum % 10) == 2)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        Lockers[i].SetActive(false);
                    }
                    for (int i = 3; i < 4; i++)
                    {
                        Musics[i].SetActive(false);
                    }
                }
                else if ((progressNum % 10) == 3)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        Lockers[i].SetActive(false);
                    }
                }
                else if ((progressNum % 10) == 4) // �갡 ���ɵ� �رݽ�Ű�� �Ƹ��װ�?
                {
                    for (int i = 0; i < 4; i++)
                    {
                        Lockers[i].SetActive(false);
                    }
                }
            }
        }
    }
}
