using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextScript2 : MonoBehaviour
{
    public TalkManager talkManager;
    public Opening2Manager manager;

    public GameObject dark;
    public Button button;
    public Animator scriptBox;
    public Text nickname; //���߿� ���� �г��� �޾ƿͼ� ��������
    public Text content;
    public Text nickname2; //�Ǹ�
    public Text content2;
    public GoBackR goBackR;
    public GoBackM goBackM;

    AudioSource audioSource;

    int talkIdx = -1;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }


    public void BoxAppear()
    {
        dark.SetActive(true);
        //��� �ڽ� ��Ÿ����.
        scriptBox.SetBool("isShow", true);
        Action();
    }
    public void click2()
    {
        //��� �����ϸ� ��ư ��Ȱ��ȭ
        button.interactable = false;
        audioSource.Play();
        Action();
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
        for (int i = 0; i < str.Length; i++)
        {
            if (data == 1)
            {
                content.text += str[i];
            }
            else if (data == 2)
            {
                content2.text += str[i];
            }
            yield return new WaitForSeconds(0.06f);
        }
        //��� ������ �ٽ� ��ư Ȱ��ȭ
        button.interactable = true;
    }
    void Talk()
    {
        content.text = null;
        nickname.text = null;
        content2.text = null;
        nickname2.text = null;
        int storyId = talkManager.getStoryId();
        TalkData data = talkManager.getTalk(storyId, talkIdx);
        if (data == null)
        {
            scriptBox.SetBool("isShow", false);
            //��ȭ �������� ��������
            SceneManager.LoadScene("Main");
            return;
        }
        else if (data.id == 1)
        {
            goBackR.back();
            goBackM.forward();
            nickname.text = "����� �г��� ������";
        }
        else if (data.id == 2)
        {
            goBackR.forward();
            goBackM.back();
            nickname2.text = "�Ǹ�";
        }
        StartCoroutine(Typing(data.content, data.id));
    }
}
