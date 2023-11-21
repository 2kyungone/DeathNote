using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NextScriptE : MonoBehaviour
{
    public TalkManager talkManager;
    public FlashBack flashback;
    public Credit credit;

    public GameObject dark;
    public Animator scriptBox;
    public Text nickname; //���߿� ���� �г��� �޾ƿͼ� ��������
    public Text content;
    public Text nickname2; //�Ǹ�
    public Text content2;
    public GameObject book;
    public GoBackR goBackR;
    public GoBackM goBackM;
    public GameObject me;
    public Button button;

    public Animator mm;
    public Animator rr;

    public GameObject obj1;
    public Image img1;
    public GameObject obj2;
    public Image img2;

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
                book.SetActive(true);
                content2.text += str[i];
            }
            yield return new WaitForSeconds(0.05f);
        }
        //��� ������ �ٽ� ��ư Ȱ��ȭ
        button.interactable = true;
    }

    async void Talk()
    {
        content.text = null;
        nickname.text = null;
        content2.text = null;
        nickname2.text = null;
        int storyId = talkManager.getStoryId();
        TalkData data = talkManager.getTalk(storyId, talkIdx);
        if (talkIdx == 2)
        {
            print("��� ����");
            //��� ��� ������.
            scriptBox.SetBool("isShow", false);
            //����ȸ����� ����
            await flashback.show(obj1, img1);
            await flashback.hide(obj1, img1);
            await flashback.show(obj2, img2);
            await flashback.hide(obj2, img2);

            scriptBox.SetBool("isShow", true);
        }
        if (data == null)
        {
            me.SetActive(false);
            scriptBox.SetBool("isShow", false);
            //���� ����ũ��������
            credit.up();
            //SceneManager.LoadScene("");
        }
        else if (data.id == 1)
        {
            mm.SetBool("turn", true);
            StartCoroutine(meSpeak());
            goBackR.back();
            goBackM.forward();
            nickname.text = "����� �г��� ������";
        }
        else if (data.id == 2)
        {
            rr.SetBool("turn", true);
            StartCoroutine(reSpeak());
            goBackR.forward();
            goBackM.back();
            nickname2.text = "�Ǹ�";
        }
        StartCoroutine(Typing(data.content, data.id));
    }

    IEnumerator meSpeak()
    {;
        yield return new WaitForSeconds(1);
        mm.SetBool("turn", false);
    }

    IEnumerator reSpeak()
    {
        yield return new WaitForSeconds(1);
        rr.SetBool("turn", false);
    }
}
