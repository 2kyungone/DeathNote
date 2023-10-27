using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class OpeningManager : MonoBehaviour
{
    public TalkManager talkManager;
    public GameObject dark;
    public Animator scriptBox;
    public Text nickname; //���߿� ���� �г��� �޾ƿͼ� ��������
    public Text content;
    public Text nickname2; //���
    public Text content2;
    public GameObject book;
    public GameObject rBook;
    public GoBackR goBackR;
    public GoBackM goBackM;

    int talkIdx=-1;

    void Awake()
    {
        rBook.SetActive(false);
        dark.SetActive(false);
    }

    public void BoxAppear()
    {
        dark.SetActive(true);
        //��� �ڽ� ��Ÿ����.
        scriptBox.SetBool("isShow", true);
        Invoke("Action", 1);
    }

    //���� ���� �Ѿ�� ���� talkIdx++ ���ش�.
    public void Action()
    {
        talkIdx++;
        Talk();
    }

    IEnumerator Typing(string str, int data)
    {
        content.text = null;
        content2.text = null;
        for(int i=0; i<str.Length; i++)
        {
            if (data == 1)
            {
                content.text += str[i];
            }
            else if (data == 2)
            {
                content2.text += str[i];
            }
            yield return new WaitForSeconds(0.05f);
        }
    }
    void Talk()
    {
        content.text = null;
        nickname.text = null;
        content2.text = null;
        nickname2.text = null;
        int storyId = talkManager.getStoryId();
        TalkData data = talkManager.getTalk(storyId, talkIdx);
        if(talkIdx == 1)
        {
            book.SetActive(false);
        }
        if(talkIdx == 2)
        {
            rBook.SetActive(true);
        }

        if (data == null) 
        {
            scriptBox.SetBool("isShow", false);
            //��ȭ �������� Ʃ�丮������ �Ѿ��.
            //SceneManager.LoadScene("");
            return;
        }
        if(data.id == 0)
        {
            nickname.text = "";
        }
        else if(data.id == 1)
        {
            goBackR.back();
            goBackM.forward();
            nickname.text = "����� �г��� ������";
        }
        else if(data.id == 2)
        {
            goBackR.forward();
            goBackM.back();
            nickname2.text = "���";
        }
        StartCoroutine(Typing(data.content, data.id));
    }
}
