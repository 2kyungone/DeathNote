using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class RecordDataArray
{
    public List<RecordData> records;
}

[Serializable]
public class RecordData
{
    public string nickname; // �г���
    public int code; // progress
    public int score; // ����
    public float grade; // ���
    public string data; // ��ū
    public string[] soulNames;

    public RecordData(string nickname, int code, int score, float grade, string data)
    {
        this.nickname = nickname;
        this.code = code;
        this.score = score;
        this.grade = grade;
        this.data = data;
    }
}

public class RecordManager : MonoBehaviour
{
    public static RecordManager instance;
    public List<RecordData> globalRecords; // � ���� ��ü ����
    Coroutine getCoroutine;
    // Start is called before the first frame update

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
    }


    public void MakeRecord(int code)
    {
        RecordData newData = new RecordData(UserManager.instance.userData.nickname, code, 0, 0.0f, "");
        UserManager.instance.userData.record.Add(newData);
    }

    public void GetGlobalRanking(int code)
    {
        if(getCoroutine != null) StopCoroutine(getCoroutine);
        globalRecords = null;
        getCoroutine = StartCoroutine(GetRanking(code));
    }

    IEnumerator GetRanking(int code)
    {
        // URL ����
        string url = "https://thatsnote.site/rankings/"+code;

        // JSON ������ ����
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            // ��û ���� �� ���� ���
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.downloadHandler.text);
                globalRecords = null;
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                RecordDataArray recordDataArray = JsonUtility.FromJson<RecordDataArray>(www.downloadHandler.text);
                globalRecords = recordDataArray.records;
            }
        }
    }

    public void SetMyRank(int code, float grade, int score, Soul souls)
    {

        bool findData = false;
        string data = JsonUtility.ToJson(souls);
        foreach (RecordData record in UserManager.instance.userData.record)
        {
            if (code == record.code)
            {
                findData = true;
                bool isChange = false;
                record.grade = Mathf.Max(grade, record.grade);
                if (score > record.score)
                {
                    record.score = score;
                    isChange = true;
                }
                if (!isChange)
                {
                    data = record.data;
                }
                else
                {
                    record.data = data;
                }
                break;
            }
        }
        
        RecordData newRecord = new RecordData(UserManager.instance.userData.nickname, code, score, grade, data);
        if (!findData) UserManager.instance.userData.record.Add(newRecord);
        UserManager.instance.SaveData();
        UserManager.instance.StartCoroutine(PostRecord(newRecord));

    }

    IEnumerator PostRecord(RecordData data)
    {
        // URL ����
        string url = "https://thatsnote.site/logs";

        // JSON ������ ����
        string jsonData = JsonUtility.ToJson(data);

        // UnityWebRequest ���� �� ����
        using (UnityWebRequest www = new UnityWebRequest(url, "POST"))
        {
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonData);
            // GET�� �ƴ� ��쿣 upload�� download �ڵ鷯�� �������� �����������.
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            // ��û ���� �� ���� ���
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.downloadHandler.text);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);

          
            }
        }
    }
}
