using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingOpener : MonoBehaviour
{
    public GameObject modalImage1; // �����Ϳ��� ������ ��� �̹���1 ���� ������Ʈ

    public void OnBackgroundClicked()
    {
        modalImage1.SetActive(true); // ��� �̹���1 Ȱ��ȭ
    }

    public void CloseModals()
    {
        modalImage1.SetActive(false); // ��� �̹���1 ��Ȱ��ȭ
    }
}
