using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GetSouls : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Start()
    {
        string url = "https://thatsnote.site/souls";

        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            // ��û�� ������ ������ ��ٸ��ϴ�.
            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                // ������ �߻����� ���, ���� �޽����� �α׿� ����մϴ�.
                Debug.LogError("Error: " + webRequest.error);
            }
            else
            {
                // ���������� �����͸� �޾��� ���, �����͸� �α׿� ����մϴ�.
                Debug.Log("Received: " + webRequest.downloadHandler.text);

                // �޾ƿ� JSON �����͸� C# ��ü�� ��ȯ�մϴ�.
                SoulsList soulsList = JsonUtility.FromJson<SoulsList>("{\"souls\":" + webRequest.downloadHandler.text + "}");

                // ��ȯ�� �����͸� ����Ͽ� UI�� ������Ʈ�ϰų� �ٸ� ó���� �� �� �ֽ��ϴ�.
                // ���� ���, ��ȯ�� �����͸� ����غ� �� �ֽ��ϴ�.
                foreach (var soul in soulsList.souls)
                {
                    Debug.Log("Soul Name: " + soul.soulName);
                }
            }
        }
    }
}
