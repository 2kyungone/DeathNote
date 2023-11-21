using UnityEngine;
using UnityEngine.EventSystems; // EventTrigger ����� ���� ���ӽ����̽�

public class ClickSound : MonoBehaviour, IPointerDownHandler // IPointerDownHandler �������̽��� ����
{
    public AudioSource audioSource; // Inspector���� �Ҵ��� AudioSource ������Ʈ

    // �������̽��� OnPointerDown �޼��� ����
    public void OnPointerDown(PointerEventData eventData)
    {
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play(); // ����� �ҽ� ���
        }
    }
}
