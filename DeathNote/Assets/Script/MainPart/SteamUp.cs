using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SteamUp : MonoBehaviour
{
    public Image[] smokes; // ������� �̹��� �迭. Unity �����Ϳ��� �巡�� �� ������� �������ּ���.

    void Start()
    {
        // ��� ���⸦ �ʱ⿡ ����ϴ�.
        foreach (Image smoke in smokes)
        {
            smoke.gameObject.SetActive(false);
        }

        StartCoroutine(ShowSmokes());
    }


    IEnumerator ShowSmokes()
    {
        while (true)
        {
            foreach (Image smoke in smokes)
            {
                smoke.gameObject.SetActive(true); // ���⸦ ������
                yield return new WaitForSeconds(1f); // 1�� ���� ��ٸ�
                smoke.gameObject.SetActive(false); // ���⸦ ����
            }
            //yield return new WaitForSeconds(3f); // 3�� ���� ��ٸ�
        }
    }
}
