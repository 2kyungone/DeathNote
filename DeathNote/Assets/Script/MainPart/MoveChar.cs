using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveChar : MonoBehaviour
{
    public float speed = 10.0f;  // ĳ������ ������ �ӵ�
    private Vector2 direction;  // ĳ���Ͱ� ������ ����
    private float changeDirectionTime = 2.0f;  // ������ �ٲٴ� �ð� ����
    public RectTransform boundaryRectTransform; // �������� ������ �̹����� RectTransform
    private RectTransform rectTransform; // ĳ������ RectTransform ������Ʈ
    private Vector2 parentSize;  // �θ� �̹����� ũ��

    void Start()
    {
        // ���� ���� ������Ʈ�� �پ��ִ� RectTransform ������Ʈ�� �����ɴϴ�.
        rectTransform = GetComponent<RectTransform>();

        // �θ� �̹����� ũ�⸦ �����ɴϴ�.
        parentSize = boundaryRectTransform.sizeDelta;

        // �ʱ� ������ �����մϴ�.
        ChooseDirection();

        // 2�ʸ��� ������ �ٲٵ��� �����մϴ�.
        InvokeRepeating("ChooseDirection", changeDirectionTime, changeDirectionTime);
    }

    void Update()
    {
        // ������ ���� ����մϴ�.
        Vector2 moveAmount = direction * speed * Time.deltaTime;

        // ���ο� ��ġ�� ����մϴ�.
        Vector2 newPos = rectTransform.anchoredPosition + moveAmount;

        // ĳ���Ͱ� �θ� �̹����� �Ѿ�� �ʵ��� x�� y ��ġ�� �����մϴ�.
        newPos.x = Mathf.Clamp(newPos.x, -parentSize.x / 2 + rectTransform.sizeDelta.x / 2, parentSize.x / 2 - rectTransform.sizeDelta.x / 2);
        newPos.y = Mathf.Clamp(newPos.y, -parentSize.y / 2 + rectTransform.sizeDelta.y / 2, parentSize.y / 2 - rectTransform.sizeDelta.y / 2);

        // ĳ������ ��ġ�� ������Ʈ�մϴ�.
        rectTransform.anchoredPosition = newPos;
    }

    // ������ ������ �����ϴ� �Լ��Դϴ�.
    void ChooseDirection()
    {
        float h = Random.Range(-1f, 1f);  // ���� ���� ���� ��
        float v = Random.Range(-1f, 1f);  // ���� ���� ���� ��
        direction = new Vector2(h, v).normalized;  // ������ ����ȭ�Ͽ� ���̰� 1�� �ǵ��� �մϴ�.
    }
}
