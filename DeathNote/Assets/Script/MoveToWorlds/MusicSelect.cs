using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


// �뷡 ���� ������
public class MusicSelect : MonoBehaviour
{
    //x ����Ʈ�� �д�. 0, -1920, -3840, -5760
    public static int[] stage = { 0, -1, -2, -3 };
    public static int idx = 0; // ������
    public static int limit = 0;
    private int w;
    private int h;
    // ��UIS
    [SerializeField] GameObject stageUI;
    [SerializeField] GameObject[] backgrounds;
    [SerializeField] Text songTitle; // �뷡 ����
    [SerializeField] Image songThumb; // �뷡 ����
    [SerializeField] Sprite[] songThumbnailData; // ��������Ʈ
    [SerializeField] Text highScore; // �� �ְ� ����
    [SerializeField] Text highGrade; // �� �ְ� ���

    // ��ŷUI
    [SerializeField] GameObject rankPanel; // ��ŷ�� ������ �г�
    [SerializeField] GameObject[] rankUI; // ��ũ UI
    Text[] nicknames; // ��Ŀ �г���
    Text[] scores; // ��Ŀ ���� 
    Animator[] repres; // ��Ŀ ��ǥ ����
    
    // �����غ� UI
    [SerializeField] GameObject startUI; // UI �غ�
    [SerializeField] Animator[] sessions; // �� ����
    [SerializeField] Text customOffsetTxt; // �� ������

    Coroutine recordCoroutine; // ��ŷ �ڷ�ƾ
    public string[] songTitlesData; // �뷡����
    public int[] songProgressData; // �뷡�ڵ�
    float customOffset; // ���� ������
    public int world;

    public void Awake()
    {
        idx = 0; // ù ������
        // newChild�� stageUI�� �ڽ����� ����
        world = MusicManager.instance.nowWorld;
        backgrounds[world].SetActive(true);
        nicknames = new Text[20];
        scores = new Text[20];
        repres = new Animator[20];
        for(int i = 0; i < 20; i++)
        {
            int plus = 0;
            if (i < 3) plus = 1; // 1~3���� ��ĭ�� �и���.

            nicknames[i] = rankUI[i].transform.GetChild(1 + plus).GetComponent<Text>(); // �г��� ����
            scores[i] = rankUI[i].transform.GetChild(2 + plus).GetComponent<Text>(); // ���� ����
            repres[i] = rankUI[i].transform.GetChild(3 + plus).GetChild(0).GetComponent<Animator>(); // ���� ����
        }

        LoadRanking(); // UI ȣ��
        MusicManager.instance.SetMusic(songProgressData[world * 4 + idx], customOffset);

        limit = 0;
        w = Screen.width;
        h = Screen.height;
        
        if(w > 1920)
        {
            w = (int)Mathf.Round(16 * (h / 9));
        }
    }

    // UI�� ������ ä���, ��ŷ ������ ȣ��
    public void LoadRanking()
    {
        if(recordCoroutine != null) StopCoroutine(recordCoroutine); // �񵿱� ����� �������̸� �����.

        songTitle.text = songTitlesData[world*4 + idx]; // �뷡 ���� ����
      
        songThumb.sprite = songThumbnailData[world * 4 + idx]; // �뷡 ���� ����
        int score = 0;
        float grade = 0.0f;
        foreach (RecordData record in UserManager.instance.userData.record)
        {
            if (record.code == songProgressData[world * 4 + idx])
            {
                score = record.score; // �� ��Ͽ��� �ְ� ������ �����´�.
                grade = record.grade; // �� ��Ͽ��� �ְ� ����� �����´�.
            }
        }
        highScore.text = score.ToString(); // ���� ����
        highGrade.text = grade.ToString();  // ��� ����
        float savedOffset = PlayerPrefs.GetFloat(songProgressData[world * 4 + idx].ToString(), 0.0f); // �ش� �뷡�� ����� �������� �����´�.
        customOffset = savedOffset; // ������ ����
        customOffsetTxt.text = customOffset.ToString(); // ������ �ؽ�Ʈ ����
        
        RecordManager.instance.GetGlobalRanking(songProgressData[world * 4 + idx]); // �񵿱� ������� �뷡�� ��ü ��ŷ�� �ҷ��´�.

        recordCoroutine = StartCoroutine(GetRecord());
    }

    // �񉥱� ����� �ڷ�ƾ
    IEnumerator GetRecord()
    {
        List<RecordData> rank = RecordManager.instance.globalRecords; // ��ü ����� �����´�.
        while( rank == null )
        {
            rank = RecordManager.instance.globalRecords; // ��ü ����� �����´�.
            yield return null; // ����� ���� ���� �ڷ�ƾ �ݺ�
        }
        for (int i = 0; i < 20; i++)
        {
            if (rank.Count > i && rank[i] != null)
            {
                nicknames[i].text = rank[i].nickname; // ��Ŀ �г����� �����Ѵ�.
                scores[i].text = rank[i].score.ToString();  // ��Ŀ ������ �����Ѵ�.
                Soul soul = JsonUtility.FromJson<Soul>(rank[i].data); // ��Ŀ ��ǥ ���� ������ �����Ѵ�.
                repres[i].SetInteger("body", soul.customizes[0]);
                repres[i].SetInteger("eyes", soul.customizes[1]);
                repres[i].SetInteger("bcol", soul.customizes[2]);
                repres[i].SetInteger("ecol", soul.customizes[3]);
            }
            else
            {
                nicknames[i].text = "?????"; // ��Ŀ �г����� �����Ѵ�.
                scores[i].text = "??????";  // ��Ŀ ������ �����Ѵ�.
                repres[i].SetInteger("body", 0);
                repres[i].SetInteger("eyes", -1);
                repres[i].SetInteger("bcol", -1);
                repres[i].SetInteger("ecol", 0);
            }

        }

    }

    // �뷡�� �غ��ϴ� �޼���
    public void ReadyToStart()
    {
        if (startUI.activeSelf) startUI.SetActive(false); // ���������� ����
        else
        {
            startUI.SetActive(true);
            // �ִϸ����͸� �����ϱ� ���� �� ���� ������ �ҷ��´�.
            List<Soul> mySession = SkillManager.instance.equip;

            startUI.SetActive(true);
            for (int i = 0; i < 6; i++)
            { 
                if (mySession[i] != null)
                {
                    sessions[i].SetInteger("body", mySession[i].customizes[0]);
                    sessions[i].SetInteger("eyes", mySession[i].customizes[1]);
                    sessions[i].SetInteger("bcol", mySession[i].customizes[2]);
                    sessions[i].SetInteger("ecol", mySession[i].customizes[3]);
                }
                else
                {
                    sessions[i].SetInteger("body", 0);
                    sessions[i].SetInteger("eyes", -1);
                    sessions[i].SetInteger("bcol", -1);
                    sessions[i].SetInteger("ecol", 0);
                }
            }
        }

    }
    // ������ ��
   public void OffsetUp()
    {
        customOffset += 0.01f;
        customOffset = Mathf.Min(customOffset, 2); // customOffset�� 2�� ���� �ʵ��� ��
        customOffset = Mathf.Round(customOffset * 100f) / 100f; // �Ҽ��� ��° �ڸ����� �ݿø�
        customOffsetTxt.text = customOffset.ToString("F2");
    }
    // ������ �ٿ�
    public void OffsetDown()
    {
        customOffset -= 0.01f;
        customOffset = Mathf.Max(customOffset, -22); // customOffset�� -2�� ���� �ʵ��� ��
        customOffset = Mathf.Round(customOffset * 100f) / 100f; // �Ҽ��� ��° �ڸ����� �ݿø�
        customOffsetTxt.text = customOffset.ToString("F2");
    }

    // ���� ����
    public void StartGame()
    {
        Debug.Log(songProgressData[world * 4 + idx]);
        PlayerPrefs.SetFloat(songProgressData[idx].ToString(), customOffset); // Ŀ���� ������ ����
        LoadingController.LoadScene("RhythmGameScene");
    }



    public void MoveNext()
    {
        if (idx < 3) 
        {
            idx++;
            limit = stage[idx] * w;
            if(Mathf.Abs(limit % 8) == 1)
            {
                limit += 1;
            }else if (Mathf.Abs(limit % 8) == 2)
            {
                limit += 2;
            }
            else if (Mathf.Abs(limit % 8) == 3)
            {
                limit += 3;
            }
            else if (Mathf.Abs(limit % 8) == 4)
            {
                limit += 4;
            }
            else if (Mathf.Abs(limit % 8) == 5)
            {
                limit -= 3;
            }
            else if (Mathf.Abs(limit % 8) == 6)
            {
                limit -= 2;
            }
            else if (Mathf.Abs(limit % 8) == 7)
            {
                limit -= 1;
            }
            else
            {
            }
        }
        LoadRanking();
        MusicManager.instance.SetMusic(songProgressData[world * 4 + idx], customOffset);

    }

    public void MoveBack()
    {
        if( idx > 0 )
        {
            idx--;
            limit = stage[idx] * w;
            if (Mathf.Abs(limit % 8) == 1)
            {
                limit -= 1;
            }
            else if (Mathf.Abs(limit % 8) == 2)
            {
                limit -= 2;
            }
            else if (Mathf.Abs(limit % 8) == 3)
            {
                limit -= 3;
            }
            else if (Mathf.Abs(limit % 8) == 4)
            {
                limit -= 4;
            }
            else if (Mathf.Abs(limit % 8) == 5)
            {
                limit += 3;
            }
            else if (Mathf.Abs(limit % 8) == 6)
            {
                limit += 2;
            }
            else if (Mathf.Abs(limit % 8) == 7)
            {
                limit += 1;
            }
        }
        LoadRanking();
        MusicManager.instance.SetMusic(songProgressData[world * 4 + idx], customOffset);
    }
}
