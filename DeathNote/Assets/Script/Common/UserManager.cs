using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

// ���� �ʹݿ� �ε��ϴ� �÷��̾� ����


[Serializable]
public class UserData
{
    public int id;
    public string nickname;
    public int progress;
    public int gold;
    public List<Soul> souls = null;
    public List<Garden> gardens = null;
    public List<RecordData> record = null;
}
[System.Serializable]

public class Garden
{
    public int id;
    public int type;

    public Garden(int type)
    {
        this.type = type;
    }

}

public class UserManager : MonoBehaviour
{
    public UserData userData;
    public static UserManager instance;

    public int load; // 0�� �ε� ��, 1�� ȸ������ Ȥ�� �α��� �ʿ�, 2�� ���� ��� �Ұ�, 3�� �α��� �Ϸ�

    private void Awake()
    {
        // �̱����� ���� �ʱ�ȭ �޼���
        if (instance == null)
        {
            instance = this; // �ڱ� �ڽ��� �Ҵ�
            DontDestroyOnLoad(gameObject); // �� ��ȯ�� �ı����� �ʵ��� ����
        }
        else if (instance != this)
        {
            Destroy(gameObject); // �ߺ� �ν��Ͻ��� �ı�
        }
    }

    public void SaveData()
    {
        string nickname = userData.nickname;
        String token = JsonUtility.ToJson(userData);
        PlayerPrefs.SetString("UserData", token);

        StartCoroutine(SendToken(nickname, token));
    }

    IEnumerator SendToken(string nickname, string token)
    {
        // URL ����
        string url = "https://thatsnote.site/members/updatetoken";

        // JSON ������ ����
        UserDataDTO data = new()
        {
            nickname = nickname,
            token = token
        };
        string jsonData = JsonUtility.ToJson(data);

        // UnityWebRequest ���� �� ����
        using (UnityWebRequest www = new UnityWebRequest(url, "PATCH"))
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

        }
    }
}
