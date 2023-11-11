using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MoveSetting : MonoBehaviour
{
    public RectTransform uiElement; // �̵���ų UI ���
    public Vector3 offScreenPosition; // ȭ�� ���� ��ġ
    public Vector3 onScreenPosition; // ȭ�� ���� ��ġ
    public float moveSpeed = 1.0f; // �̵� �ӵ�

    // ȭ�� ������ �̵�
    public void MoveIn()
    {
        StartCoroutine(Move(uiElement, onScreenPosition, moveSpeed));
    }

    // ȭ�� ������ �̵�
    public void MoveOut()
    {
        StartCoroutine(Move(uiElement, offScreenPosition, moveSpeed));
    }

    // �̵� �ڷ�ƾ
    IEnumerator Move(RectTransform element, Vector3 targetPosition, float speed)
    {
        while (Vector3.Distance(element.localPosition, targetPosition) > 0.01f)
        {
            element.localPosition = Vector3.MoveTowards(element.localPosition, targetPosition, speed * Time.deltaTime);
            yield return null;
        }
    }
}
