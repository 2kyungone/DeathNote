using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

// ���� �ʹݿ� �ε��ϴ� �÷��̾� ����

public class UserData
{
    public int id;
    public string email;
    public string name;
    public int progress;
    public int gold;
    public List<Soul> souls = null;

    public UserData(int id, string email, string name, int progress, int gold)
    {
        this.id = id;
        this.email = email;
        this.name = name;
        this.progress = progress;  
        this.gold = gold;

    }
}
[System.Serializable]
public class GardenData
{
    public int id;
    public int type;

    public GardenData(int id, int type)
    {
        this.id = id;
        this.type = type;
    }
}

public class Garden
{
    public int type;
    public string name;
}


public class UserManager : MonoBehaviour
{
    public UserData userData;
    public List<GardenData> gardenData;
    public static UserManager instance;  
    public int Load 
    {
        get; private set;
    }

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

        userData = new UserData(1, "���ǿ�", "���Ĵ�", 600, 130000);
        gardenData = new List<GardenData>
        {
            new GardenData(0, 1),
            new GardenData(1, 2),
            new GardenData(2, 3)
    };

        Load = 0; // 
    }

    void Start()
    {

        // StartCoroutine(GetUserData());
    }

    IEnumerator GetUserData()
    {
        // PlayerPrefs�� Ȯ���Ͽ�, Id�� ������ ���̵�, ������ 0�� ��ȯ
        int userId = PlayerPrefs.GetInt("Id", 0);
        string token = PlayerPrefs.GetString("Token", "12");

        // ���̵� ���� ��� �α��� ������ �̵�
        if (userId == 0 || token == null)
        {
            // SceneManager.LoadSceneAsync("WorldFourScene");
        }
        else
        {
            // ������ ������ ��� �������� HTTP���
            string url = "https://thatsnote.site/members/" + userId;

            using (UnityWebRequest www = UnityWebRequest.Get(url))
            {
                www.SetRequestHeader("Authorization", "Bearer " + token);

                yield return www.SendWebRequest();

                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log(www.error);
                    Load = 1; // ������ �߻���
                }
                else
                {
                    Debug.Log(www.downloadHandler.text);
                    // userData ��ü�� JSON�� parsing
                    List<Soul> souls = JsonUtility.FromJson<List<Soul>>(www.downloadHandler.text);
                    // SoulManager.instance.Souls = souls;

                    Load = 2; // �ε忡 ������
                    SceneManager.LoadSceneAsync("RaMain");
                }
            }
        }

    }

    // ������ �г��� ����
    IEnumerator PatchUserName(string newName)
    {
        // PlayerPrefs�� Ȯ���Ͽ�, Id�� ������ ���̵�, ������ 0�� ��ȯ
        int userId = PlayerPrefs.GetInt("Id", 0);
        string token = PlayerPrefs.GetString("Token", "12");

        // ���̵� ���� ��� �α��� ������ �̵�
        if (userId == 0 || token == null)
        {
            // SceneManager.LoadSceneAsync("WorldFourScene");
        }
        else
        {
            // URL ����
            string url = "https://thatsnote.site/members/" + userId;

            // JSON ������ ����
            var nickname = new
            {
                nickname = newName
            };

            string jsonData = JsonUtility.ToJson(nickname);

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
                    Debug.Log(www.error);
                }
                else
                {
                    Debug.Log("JSON upload complete!");
                }
            }
        }

    }

}
