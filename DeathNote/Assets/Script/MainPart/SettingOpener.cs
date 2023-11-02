using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingOpener : MonoBehaviour
{
    public GameObject modalImage1; // �����Ϳ��� ������ ��� �̹���1 ���� ������Ʈ
    public GameObject modalImage2; // �����Ϳ��� ������ ��� �̹���2 ���� ������Ʈ

    public void OnBackgroundClicked()
    {
        modalImage1.SetActive(true); // ��� �̹���1 Ȱ��ȭ
        modalImage2.SetActive(true); // ��� �̹���2 Ȱ��ȭ
    }

    public void CloseModals()
    {
        modalImage1.SetActive(false); // ��� �̹���1 ��Ȱ��ȭ
        modalImage2.SetActive(false); // ��� �̹���2 ��Ȱ��ȭ
    }
}
