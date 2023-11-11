using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldOnOffController : MonoBehaviour
{
    KooksUserManager kooksUserManager;
    KooksUserManager.UserData userData;
    private int progressNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        //!!!!!!!!!!!!!@@@@@@@@@@@@@@@@@@@@@@@���ý��丮������ �ش� ���� �������� �̸� ���� �κ� !!!!!!!!!!!!!@@@@@@@@@@@@@@@@@@@@@@@
        //progressNum = PlayerPrefs.GetInt("progress");
        //!!!!!!!!!!!!!@@@@@@@@@@@@@@@@@@@@@@@���ý��丮������ �ش� ���� �������� �̸� ���� �κ� !!!!!!!!!!!!!@@@@@@@@@@@@@@@@@@@@@@@

        kooksUserManager = FindObjectOfType<KooksUserManager>();
        if (kooksUserManager != null)
        {
            userData = kooksUserManager.GetUserData(); // KooksUserManager ��ũ��Ʈ���� UserData�� �����ɴϴ�.
            progressNum = userData.progress;
            if(progressNum == 0)
            {
                // ���� flow �����
            }else if((progressNum / 100) == 1) // 1���� 6������ �ش��ϴ� �������� MoveToStage??���� fly ��ũ��Ʈ ���ְ� LockedStage?? ���ش�.
            {
                GameObject beUnlockGameObjects = GameObject.Find("LockedStageOne");
                beUnlockGameObjects.SetActive(false);
                GameObject beFlyGameObject = GameObject.Find("MoveToStageOne");
                beFlyGameObject.GetComponent<fly> ().enabled = true;
            }
            else if ((progressNum / 100) == 2)
            {
                //2���� �����־���Ѵ�.
                GameObject[] beUnlockGameObjects = { GameObject.Find("LockedStageOne"), GameObject.Find("LockedStageTwo") };
                for (int i = 0; i < beUnlockGameObjects.Length; i++)
                {
                    beUnlockGameObjects[i].SetActive(false);
                }

                GameObject[] beFlyGameObjects = { GameObject.Find("MoveToStageOne"), GameObject.Find("MoveToStageTwo") };
                for (int i = 0; i < beFlyGameObjects.Length; i++)
                {
                    beFlyGameObjects[i].GetComponent<fly>().enabled = true;
                }

            }
            else if ((progressNum / 100) == 3)
            {
                //3���� �����־���Ѵ�.
                GameObject[] otherGameObjects = { GameObject.Find("LockedStageOne"), GameObject.Find("LockedStageTwo"), GameObject.Find("LockedStageThree") };
                for (int i = 0; i < otherGameObjects.Length; i++)
                {
                    otherGameObjects[i].SetActive(false);
                }

                GameObject[] beFlyGameObjects = { GameObject.Find("MoveToStageOne"), GameObject.Find("MoveToStageTwo"), GameObject.Find("MoveToStageThree") };
                for (int i = 0; i < beFlyGameObjects.Length; i++)
                {
                    beFlyGameObjects[i].GetComponent<fly>().enabled = true;
                }
            }
            else if ((progressNum / 100) == 4)
            {
                //4���� �����־���Ѵ�.
                GameObject[] otherGameObjects = { GameObject.Find("LockedStageOne"), GameObject.Find("LockedStageTwo"), GameObject.Find("LockedStageThree"), GameObject.Find("LockedStageFour") };
                for (int i = 0; i < otherGameObjects.Length; i++)
                {
                    otherGameObjects[i].SetActive(false);
                }

                GameObject[] beFlyGameObjects = { GameObject.Find("MoveToStageOne"), GameObject.Find("MoveToStageTwo"), GameObject.Find("MoveToStageThree"), GameObject.Find("MoveToStageFour") };
                for (int i = 0; i < beFlyGameObjects.Length; i++)
                {
                    beFlyGameObjects[i].GetComponent<fly>().enabled = true;
                }
            }
            else if ((progressNum / 100) == 5)
            {
                //5���� �����־���Ѵ�.
                GameObject[] otherGameObjects = { GameObject.Find("LockedStageOne"), GameObject.Find("LockedStageTwo"), GameObject.Find("LockedStageThree"), GameObject.Find("LockedStageFour"), GameObject.Find("LockedStageFive") };
                for (int i = 0; i < otherGameObjects.Length; i++)
                {
                    otherGameObjects[i].SetActive(false);
                }

                GameObject[] beFlyGameObjects = { GameObject.Find("MoveToStageOne"), GameObject.Find("MoveToStageTwo"), GameObject.Find("MoveToStageThree"), GameObject.Find("MoveToStageFour"), GameObject.Find("MoveToStageFive") };
                for (int i = 0; i < beFlyGameObjects.Length; i++)
                {
                    beFlyGameObjects[i].GetComponent<fly>().enabled = true;
                }
            }
            else if ((progressNum / 100) == 6)
            {
                //6���� �����־���Ѵ�.
                GameObject[] otherGameObjects = { GameObject.Find("LockedStageOne"), GameObject.Find("LockedStageTwo"), GameObject.Find("LockedStageThree"), GameObject.Find("LockedStageFour"), GameObject.Find("LockedStageFive"), GameObject.Find("LockedStageSix") };
                for (int i = 0; i < otherGameObjects.Length; i++)
                {
                    otherGameObjects[i].SetActive(false);
                }

                GameObject[] beFlyGameObjects = { GameObject.Find("MoveToStageOne"), GameObject.Find("MoveToStageTwo"), GameObject.Find("MoveToStageThree"), GameObject.Find("MoveToStageFour"), GameObject.Find("MoveToStageFive"), GameObject.Find("MoveToStageSix") };
                for (int i = 0; i < beFlyGameObjects.Length; i++)
                {
                    beFlyGameObjects[i].GetComponent<fly>().enabled = true;
                }
            }
            else if ((progressNum / 100) == 7)
            {
                //6���� �����־���Ѵ�.
                GameObject[] otherGameObjects = { GameObject.Find("LockedStageOne"), GameObject.Find("LockedStageTwo"), GameObject.Find("LockedStageThree"), GameObject.Find("LockedStageFour"), GameObject.Find("LockedStageFive"), GameObject.Find("LockedStageSix") };
                for (int i = 0; i < otherGameObjects.Length; i++)
                {
                    otherGameObjects[i].SetActive(false);
                }

                GameObject[] beFlyGameObjects = { GameObject.Find("MoveToStageOne"), GameObject.Find("MoveToStageTwo"), GameObject.Find("MoveToStageThree"), GameObject.Find("MoveToStageFour"), GameObject.Find("MoveToStageFive"), GameObject.Find("MoveToStageSix") };
                for (int i = 0; i < beFlyGameObjects.Length; i++)
                {
                    beFlyGameObjects[i].GetComponent<fly>().enabled = true;
                }
            }
            else if ((progressNum / 100) == 8)
            {
                //6���� �����־���Ѵ�.
                GameObject [] otherGameObjects = { GameObject.Find("LockedStageOne"), GameObject.Find("LockedStageTwo"), GameObject.Find("LockedStageThree"), GameObject.Find("LockedStageFour"), GameObject.Find("LockedStageFive"), GameObject.Find("LockedStageSix") };
                for(int i = 0; i< otherGameObjects.Length; i++)
                {
                    otherGameObjects[i].SetActive(false);
                }

                GameObject[] beFlyGameObjects = { GameObject.Find("MoveToStageOne"), GameObject.Find("MoveToStageTwo"), GameObject.Find("MoveToStageThree"), GameObject.Find("MoveToStageFour"), GameObject.Find("MoveToStageFive"), GameObject.Find("MoveToStageSix") };
                for (int i = 0; i < beFlyGameObjects.Length; i++)
                {
                    beFlyGameObjects[i].GetComponent<fly>().enabled = true;
                }
            }
        }
        else
        {
            Debug.LogError("KooksUserManager not found!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
