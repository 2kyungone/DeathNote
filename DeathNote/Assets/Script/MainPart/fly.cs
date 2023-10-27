using UnityEngine;
using UnityEngine.UI; // UI ���� ���ӽ����̽�

public class fly : MonoBehaviour
{
    public float floatStrength = 0.5f;
    public float amplitude = 0.1f;
    private Vector2 originalPosition;

    private RectTransform rectTransform; // RectTransform ���� �߰�

    void Start()
    {
        rectTransform = GetComponent<RectTransform>(); // RectTransform ������Ʈ�� �����ɴϴ�.
        originalPosition = rectTransform.anchoredPosition; // �ʱ� ��ġ�� anchoredPosition���� �����մϴ�.
    }

    void Update()
    {
        rectTransform.anchoredPosition = originalPosition + new Vector2(0.0f, Mathf.Abs(Mathf.Sin(Time.time) * amplitude) * floatStrength);
        // anchoredPosition�� ����Ͽ� ��ġ�� �����մϴ�.
    }
}
