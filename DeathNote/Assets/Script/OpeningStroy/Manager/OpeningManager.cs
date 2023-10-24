using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OpeningManager : MonoBehaviour
{
    public TalkManager talkManager;
    //public GameObject scriptBox;
    public Animator scriptBox;
    public Text nickname; //���߿� ���� �г��� �޾ƿͼ� ��������
    public Text content;

    int talkIdx=-1;

    void Awake()
    {
        //scriptBox.SetActive(false);
    }

    public void BoxAppear()
    {
        //��� �ڽ� ��Ÿ����.
        scriptBox.SetBool("isShow", true);
        Invoke("Action", 1);
        //return;
    }

    //���� ���� �Ѿ�� ���� talkIdx++ ���ش�.
    public void Action()
    {
        talkIdx++;
        Talk();
    }

    IEnumerator Typing(string str)
    {
        content.text = null;
        for(int i=0; i<str.Length; i++)
        {
            content.text += str[i];
            yield return new WaitForSeconds(0.05f);
        }
    }
    void Talk()
    {
        int storyId = talkManager.getStoryId();
        TalkData data = talkManager.getTalk(storyId, talkIdx);
        if (data == null) 
        {
            scriptBox.SetBool("isShow", false);
            //��ȭ �������� ���� ȭ������ �Ѿ��.
            return;
        }
        if(data.id == 0)
        {
            nickname.text = "";
        }
        else if(data.id == 1)
        {
            nickname.text = "����� �г��� ������";
        }
        else if(data.id == 2)
        {
            nickname.text = "���";
        }
        StartCoroutine(Typing(data.content));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
