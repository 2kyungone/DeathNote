using UnityEngine;
using UnityEngine.UI;

public class DragHandler : MonoBehaviour
{
    private bool isDragging = false;
    private Vector2 offset; // ���콺 �����Ϳ� �̹����� ��ġ ����
    private Vector2 lastMousePosition;
    private Vector2 dragStartPos;
    private Vector2 dragEndPos;
    private RectTransform rectTransform;
    private Rigidbody2D rb;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // ��ġ �Է��̳� ���콺 �Է��� ����
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 localMousePosition;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, Input.mousePosition, null, out localMousePosition))
            {
                if (rectTransform.rect.Contains(localMousePosition))
                {
                    // �巡�� ����
                    isDragging = true;
                    offset = localMousePosition; // ������ ���콺 ��ġ�� ���
                    dragStartPos = Input.mousePosition;
                    rb.isKinematic = true; // ���� ����� ����
                }
            }
        }

        // �巡�� ��
        if (isDragging)
        {
            Vector2 localCursor;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent as RectTransform, Input.mousePosition, null, out localCursor))
            {
                rectTransform.anchoredPosition = localCursor - offset; // ��ġ ���̸� �����ϸ鼭 �̹����� ������
            }
        }

        // �巡�� ����
        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            isDragging = false;
            dragEndPos = Input.mousePosition;
            Vector2 throwDirection = (dragEndPos - dragStartPos).normalized; // ������ ����
            float throwForce = Vector2.Distance(dragStartPos, dragEndPos) / Time.deltaTime; // ������ ��
            rb.isKinematic = false; // ���� ����� ����
            rb.AddForce(throwDirection * throwForce); // ���� ����� ������ ����
        }

        lastMousePosition = Input.mousePosition; // ������ ���콺 ��ġ�� ���
    }
}
