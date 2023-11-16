using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Opening2Manager : MonoBehaviour
{
    public TalkManager talkManager;
    //public NextScript2 next;

    int talkIdx = -1;

    void Start()
    { 
        BoxAppear();
    }

    public void BoxAppear()
    {
        talkManager.BoxAppear(true);
        //scriptBox.SetBool("isShow", true);
        Action();
    }
    public void click()
    {
        print("Ŭ��");
        TalkManager.instance.click();
        //button.interactable = false;
        //audioSource.Play();
        Action();
    }

    //���� ���� �Ѿ�� ���� talkIdx++ ���ش�.
    public void Action()
    {
        talkIdx++;
        Talk();
    }

    void Talk()
    {
        bool isFinish = TalkManager.instance.ChangeUI(-1, talkIdx);
        print("��簡 �����ߴ�");
        if (isFinish)
        {
            //���൵ 100���� ������Ʈ
            //UserManager.instance.SaveData();
            TalkManager.instance.BoxAppear(false);
            //��ȭ �������� Ʃ�丮������ �Ѿ��.
            //SceneManager.LoadScene("Tutorial");
            LoadingController.LoadScene("RaMain 1");
        }
        else
        {
            switch (talkIdx)
            {

            }
        }
    }
}
