using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// ���� �ʹݿ� �ε��ϴ� �÷��̾� ����

public class StartManager : MonoBehaviour
{
    [SerializeField] GameObject signButton;
    [SerializeField] GameObject logoutButton;
    [SerializeField] Image policyButton;
    [SerializeField] TextMeshProUGUI myName;
    [SerializeField] GameObject Buttons;
    [SerializeField] TMP_InputField signInputField;
    [SerializeField] TMP_InputField logInputField;
    [SerializeField] Sprite unCheckSprite;
    [SerializeField] Sprite checkSprite;
    public bool isSignable;
    public bool check;

    public int load; // 0�� �ε� ��, 1�� ȸ������ Ȥ�� �α��� �ʿ�, 2�� ���� ��� �Ұ�, 3�� �α��� �Ϸ�

    void Start()
    {
        StartCoroutine(GetUserData(null));
    }

    // ��ǲ �ʵ尡 ����� �� ���� ȣ���Ͽ� isSignable�� �ʱ�ȭ
    public void ValueChangedCheck()
    {
        signButton.SetActive(false);
    }
     
    // ����� �����ϴ� �޼���
    public void CheckPolicy()
    {
        check = !check;
        if (check) policyButton.sprite = checkSprite;
        else policyButton.sprite = unCheckSprite;
    }

    // �ߺ��� üũ�ϴ� �޼���
    public void CheckDuplicate()
    {
        string nickname = signInputField.text;
        if (nickname.Length > 5) return;
        StartCoroutine(CheckDuple(nickname));
    }

    // ȸ������ �޼���
    public void SignUp()
    {
        string nickname = signInputField.text;
        if (nickname.Length > 5) return;
        StartCoroutine(SignUp(nickname));
    }

    public void LogIn()
    {
        string nickname = logInputField.text;
        StartCoroutine(GetUserData(nickname));
    }
    // �α׾ƿ�
    public void LogOut()
    {
        PlayerPrefs.DeleteAll();
        string sceneName = SceneManager.GetActiveScene().name;

        // ���� Ȱ�� ���� �ٽ� �ε��մϴ�.
        SceneManager.LoadScene(sceneName);

    }

    public void MoveMain()
    {
        if (load != 3) return;
        if(UserManager.instance.userData.progress > 0)
        {

        }
        else
        {
            SceneManager.LoadScene("OpeningScene 1");
        }
    }

    // ���� �����͸� �������� �ڷ�ƾ
    IEnumerator GetUserData(string nickname)
    {
        // PlayerPrefs�� Ȯ���Ͽ� userData �������� ��ȯ
        UserData saveData = JsonUtility.FromJson<UserData>(PlayerPrefs.GetString("UserData", null));

        // ���������Ͱ� ���ų�, ���޹��� ���̵� ��� 1�� ���·� ����
        if (saveData == null && nickname == null)
        {
            load = 1;
            Buttons.SetActive(true);
            yield return null;
            // SceneManager.LoadSceneAsync("WorldFourScene");
        }
        else
        {   
            if(nickname == null)
            nickname = saveData.nickname;
            // string token = PlayerPrefs.GetString("Token", null);

            // URL ����
            string url = "https://thatsnote.site/members/login";

            // JSON ������ ����
            UserData data = new UserData
            {
                nickname = nickname
            };
            string jsonData = JsonUtility.ToJson(data);
            Debug.Log(jsonData);
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
                    load = 2;
                    UserManager.instance.userData = saveData;
                }
                else
                {
                    logoutButton.SetActive(true);
                    Debug.Log(www.downloadHandler.text);
                    PlayerPrefs.SetString("UserData", www.downloadHandler.text);
                    UserManager.instance.userData = JsonUtility.FromJson<UserData>(www.downloadHandler.text);
                    myName.text = nickname;
                    load = 3;
                }
            }
        }

    }
    
    // �ߺ� Ȯ�� �ڷ�ƾ
    IEnumerator CheckDuple(string nickname)
    {
        // URL ����
        string url = "https://thatsnote.site/members/login";

        // JSON ������ ����
        UserData data = new UserData
        {
            nickname = nickname
        };

        string jsonData = JsonUtility.ToJson(data);
        Debug.Log(jsonData);
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
                // ���� �α��ο� �����Ѵٴ� ����, �� �г����� ����� �ȵǾ��ٴ� �� 
                signButton.SetActive(true);
            }
        }
    }

    // ȸ������ �ڷ�ƾ
    IEnumerator SignUp(string nickname)
    {
        // URL ����
        string url = "https://thatsnote.site/members/signup";

        // JSON ������ ����
        UserData data = new()
        {
            nickname = nickname
        };
        string jsonData = JsonUtility.ToJson(data);
        Debug.Log(jsonData);
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
                UserData userData = JsonUtility.FromJson<UserData>(www.downloadHandler.text);
                UserManager.instance.userData = userData;
                PlayerPrefs.SetString("UserData", www.downloadHandler.text);
                string sceneName = SceneManager.GetActiveScene().name;

                // ���� Ȱ�� ���� �ٽ� �ε��մϴ�.
                SceneManager.LoadScene(sceneName);
            }
        }
    }

    // ������ �г��� ����
    IEnumerator PatchUserName(string newName)
    {
        // PlayerPrefs�� Ȯ���Ͽ�, Id�� ������ ���̵�, ������ 0�� ��ȯ
        int userId = PlayerPrefs.GetInt("Id", 0);
        string token = PlayerPrefs.GetString("Token", "12");

        // ���̵� ���� ��� ������ 0���� ����
        if (userId == 0 || token == null)
        {
            load = 1;
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
                    load = 2; // ���� ��� �Ұ��� ����

                    Debug.Log(www.error);
                }
                else
                {
                    load = 3; // �α��� �Ϸ�� ���� 
                    Debug.Log(www.downloadHandler.text);

                }
            }
        }

    }


}
