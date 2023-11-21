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


// ��ū����(JSON)�� �ޱ����ؼ� �����ϴ� DTO ����
[Serializable]
public class UserDataDTO
{
    public int id;
    public string nickname;
    public int progress;
    public int gold;
    public string token;
    public List<Soul> souls = null;
    public List<Garden> gardens = null;

    public string stringer()
    {
        return id + "/" + nickname + "/" + token;
    }
}

public class LoginManager : MonoBehaviour
{
    [SerializeField] GameObject signButton;
    [SerializeField] GameObject logoutButton;
    [SerializeField] Image policyButton;
    [SerializeField] TextMeshProUGUI myName;
    [SerializeField] GameObject buttons;
    [SerializeField] TMP_InputField signInputField;
    [SerializeField] TMP_InputField logInputField;
    [SerializeField] Sprite unCheckSprite;
    [SerializeField] Sprite checkSprite;
    [SerializeField] GameObject clickable;
    public bool isSignable;
    public bool check;

    public int load; // 0�� �ε� ��, 1�� ȸ������ Ȥ�� �α��� �ʿ�, 2�� ���� ��� �Ұ�, 3�� �α��� �Ϸ�

    public Button CloseButton;

    public float LeftMargin = 400;
    public float TopMargin = 50;
    public float RightMargin = 400;
    public float BottomMargin = 50;
    public bool IsRelativeMargin = true;

    [HideInInspector]
    public string URL;

    private WebViewObject webViewObject;


    void Start()
    {
        AlignCloseButton();
        UserData saveData = JsonUtility.FromJson<UserData>(PlayerPrefs.GetString("UserData", null));
        if(saveData != null)
        {
            StartCoroutine(GetUserData(null));
        }
        else
        {
            buttons.SetActive(true);
        }
        
    }

    // ȸ������ �޼���
    public void SignUp()
    {
        Show("https://thatsnote.site/oauth2/authorization/kakao");
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
             LoadingController.LoadScene("MainScene");
        }
        else
        {
            SceneManager.LoadScene("OpeningScene");
        }
    }

    // ���� �����͸� �������� �ڷ�ƾ
    IEnumerator GetUserData(string nickname)
    {
        // PlayerPrefs�� Ȯ���Ͽ� userData �������� ��ȯ
        UserData saveData = JsonUtility.FromJson<UserData>(PlayerPrefs.GetString("UserData", null));

        // ���������Ͱ� ����, ���޹��� ���̵� ��� 1�� ���·� ����
        if (saveData == null && nickname == null)
        {
            load = 1;
            buttons.SetActive(true);
            yield return null;
            // SceneManager.LoadSceneAsync("WorldFourScene");
        }
        else
        {   
            if(nickname == null)
            nickname = saveData.nickname; // ���޵� �г����� ������, ���̺굥���ͷ�
                
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
                    //PlayerPrefs.DeleteAll();
                    UserManager.instance.userData = saveData;
                }
                else
                {

                    

                    UserDataDTO dto = JsonUtility.FromJson<UserDataDTO>(www.downloadHandler.text);

                    if (dto.token.Length == 0)
                    {
                        Debug.Log("���⿡��");
                        UserData userData = new UserData();
                        userData.id = dto.id;
                        userData.nickname = dto.nickname;
                        userData.progress = 0;
                        userData.gold = 0;
                        userData.souls = new List<Soul>
                        {
                            new Soul(0, "ù ����", 0, new int[]{1, 0, 0, 5}, new int[]{1, 1, 0, 0}, new int[]{10, 10, 10, 10, 10, 10 }, 0, 0),
                            new Soul(1, "ù ����", 1, new int[]{1, 0, 0, 5}, new int[]{1, 1, 1, 0}, new int[]{10, 10, 10, 10, 10, 10 }, 0, 0),
                            new Soul(2, "ù ����", 2, new int[]{1, 0, 0, 5}, new int[]{1, 1, 2, 0}, new int[]{10, 10, 10, 10, 10, 10 }, 0, 0),
                            new Soul(3, "ù ����", 3, new int[]{1, 0, 0, 5}, new int[]{1, 1, 3, 0}, new int[]{10, 10, 10, 10, 10, 10 }, 0, 0),
                            new Soul(4, "ù ����", 4, new int[]{1, 0, 0, 5}, new int[]{1, 1, 4, 0}, new int[]{10, 10, 10, 10, 10, 10 }, 0, 0),
                            new Soul(5, "ù ����", 5, new int[]{1, 0, 0, 5}, new int[]{1, 1, 5, 0}, new int[]{10, 10, 10, 10, 10, 10 }, 0, 0)
                        };
                        userData.gardens = new List<Garden>
                        {
                            new Garden(0)
                        };
                        userData.record = new List<RecordData>();

                        UserManager.instance.userData = userData;
                        UserManager.instance.SaveData();
                    }
                    else
                    {
                        Debug.Log("���⿡��");
                        UserData userData = JsonUtility.FromJson<UserData>(dto.token);
                        
                        UserManager.instance.userData = userData;
                   
                        UserManager.instance.SaveData();
                    }

                    Debug.Log("���ڵ� ������ ���� ����" + www.downloadHandler.text);
                    
                    foreach(Soul soul in UserManager.instance.userData.souls)
                    {
                        if(soul.equip != -1)
                        {
                            SkillManager.instance.equip[soul.equip] = soul;
                        }
                    }

                    Debug.Log("��ų ������ ���� ����" + www.downloadHandler.text);
                    myName.text = nickname;
                    load = 3;
                    DestroyWebView();
                    logoutButton.SetActive(true);
                    clickable.SetActive(true);
                    buttons.SetActive(false);
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

    void Update()
    {
        //if (Application.platform == RuntimePlatform.Android) {
        if (Input.GetKey(KeyCode.Escape))
        {
            // �� �� �� ��, esc �� ư 
            if (webViewObject)
            {
                if (webViewObject.gameObject.activeInHierarchy)
                {
                    // webViewObject.GoBack();
                }
            }
            Hide();
            return;
        }
        //}
    }

    private void OnDestroy()
    {
        DestroyWebView();
    }

    void DestroyWebView()
    {
        if (webViewObject)
        {
            GameObject.Destroy(webViewObject.gameObject);
            webViewObject = null;
        }
    }

    void AlignCloseButton()
    {
        if (CloseButton == null)
        {
            return;
        }

        float defaultScreenHeight = 1080;
        float top = CloseButton.GetComponent<RectTransform>().rect.height * Screen.height / defaultScreenHeight;

        TopMargin = top;
    }

    public void Show(string url)
    {
        gameObject.SetActive(true);

        URL = url;

        StartWebView();
    }

    public void Hide()
    {
        // �� �� �� ��, esc �� ư 
        URL = string.Empty;

        if (webViewObject != null)
        {
            webViewObject.SetVisibility(false);

            //webViewObject.ClearCache(true);
            //webViewObject.ClearCookies();                
        }

        DestroyWebView();

        gameObject.SetActive(false);
    }

    public void StartWebView()
    {
        string strUrl = URL;  //"https://www.naver.com/";            

        try
        {
            if (webViewObject == null)
            {
                webViewObject = (new GameObject("WebViewObject")).AddComponent<WebViewObject>();

                webViewObject.Init(
                    cb: OnResultWebView,
                    err: (msg) => { Debug.Log($"WebView Error : {msg}"); },
                    httpErr: (msg) => { Debug.Log($"WebView HttpError : {msg}"); },
                    started: (msg) => { Debug.Log($"WebView Started : {msg}"); },
                    hooked: (msg) => { Debug.Log($"WebView Hooked : {msg}"); },

                    ld: (msg) =>
                    {
                        Debug.Log($"WebView Loaded : {msg}");
                        //webViewObject.EvaluateJS(@"Unity.call('ua=' + navigator.userAgent)");                    
                    }
                    , androidForceDarkMode: 1  // 0: follow system setting, 1: force dark off, 2: force dark on

#if UNITY_EDITOR
                    , separated: true
#endif

                );
            }

            webViewObject.LoadURL(strUrl);
            webViewObject.SetVisibility(true);
            webViewObject.SetMargins((int)LeftMargin, (int)TopMargin, (int)RightMargin, (int)BottomMargin, IsRelativeMargin);
        }
        catch (System.Exception e)
        {
            print($"WebView Error : {e}");
        }

    }

    void OnResultWebView(string resultData)
    {
        StartCoroutine(GetUserData(resultData));
        /*
         {
            result : string,
            data : string or json string
        }
        */

        //try
        //{
        //    JsonData json = JsonMapper.ToObject(resultData);

        //    if ((string)json["result"] == "success")
        //    {
        //        JsonData data = json["data"]["response"];
        //        long birthdayTick = (long)(data["birth"].IsLong ? (long)data["birth"] : (int)data["birth"]);
        //        string birthday = (string)data["birthday"];
        //        string unique_key = (string)data["unique_key"];

        //        // success
        //    }
        //    else if ((string)json["result"] == "failed")
        //    {
        //        Hide();

        //        // failed
        //    }
        //}
        //catch (Exception e)
        //{
        //    print("�� ���� ���� ������ �ֽ��ϴ�.");
        //}
    }
}

