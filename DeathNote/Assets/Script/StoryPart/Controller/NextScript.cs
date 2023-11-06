using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextScript : MonoBehaviour
{
    public TalkManager talkManager;
    public OpeningManager manager;

    public Animator scriptBox;
    public GameObject dark;
    public Text nickname; //���߿� ���� �г��� �޾ƿͼ� ��������
    public Text content;
    public Text nickname2; //���
    public Text content2;
    public GameObject book;
    public GameObject rBook;
    public GoBackR goBackR;
    public GoBackM goBackM;
    public GameObject me;
    public Button button;
    public GameObject backgroundN;

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

    public void click()
    {
        //��� �����ϸ� ��ư ��Ȱ��ȭ
        button.interactable = false;
        audioSource.Play();
        Action();
    }

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
        backgroundN.SetActive(false);
        content.text = null;
        nickname.text = null;
        content2.text = null;
        nickname2.text = null;
        int storyId = talkManager.getStoryId();
        TalkData data = talkManager.getTalk(storyId, talkIdx);
        if (talkIdx == 1)
        {

        }
        if (talkIdx == 2)
        {
            book.SetActive(false);
            rBook.SetActive(true);
        }

        if (data == null)
        {
            scriptBox.SetBool("isShow", false);
            //��ȭ �������� Ʃ�丮������ �Ѿ��.
            SceneManager.LoadScene("Tutorial");
        }
        else if (data.id == 1)
        {
            goBackR.back();
            goBackM.forward();
            nickname.text = "����� �г��� ������";
        }
        else if (data.id == 2)
        {
            backgroundN.SetActive(true);
            goBackR.forward();
            goBackM.back();
            nickname2.text = "�Ǹ�";
        }
        StartCoroutine(Typing(data.content, data.id));
    }
}
