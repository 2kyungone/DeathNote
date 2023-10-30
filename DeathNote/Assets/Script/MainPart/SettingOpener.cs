using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingOpener : MonoBehaviour
{
    public GameObject modalImage; // �����Ϳ��� ������ ��� �̹��� ���� ������Ʈ

    public void OnBackgroundClicked()
    {
        modalImage.SetActive(true); // ��� �̹��� Ȱ��ȭ
    }

    public void CloseModal()
    {
        modalImage.SetActive(false); // ��� �̹��� ��Ȱ��ȭ
    }
}
