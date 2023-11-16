using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // �� ������ ���� ���ӽ����̽�

public class Interactive : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isDragging = false;
    private Vector2 startPosition;

    // �߰��� ����
    private Vector2 previousMousePosition;
    private Vector2 throwVelocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;  // �ʱ⿡ �߷��� ������ ���� �ʵ��� ����
    }

    private void Update()
    {
        Vector2 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            if (GetComponent<Collider2D>().OverlapPoint(currentMousePosition))
            {
                isDragging = true;
                startPosition = currentMousePosition;
            }
        }

        if (isDragging)
        {
            // ������ �ӵ� ���
            throwVelocity = (currentMousePosition - previousMousePosition) / Time.deltaTime;
            transform.position = currentMousePosition;
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            isDragging = false;
            rb.isKinematic = false;
            rb.velocity = throwVelocity * 0.1f; // ���⿡�� 0.5f�� �����ϸ� �����Դϴ�. �� ���� �����Ͽ� ������ ���� ũ�⸦ ������ �� �ֽ��ϴ�.
        }


        previousMousePosition = currentMousePosition;
    }
}