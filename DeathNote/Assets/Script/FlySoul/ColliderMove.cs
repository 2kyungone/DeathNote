using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderMove : MonoBehaviour
{
    public float speed;
    private bool isMoving = true; // �������� �����ϱ� ���� �÷��׸� �߰��մϴ�.

    // �������� �����ϰų� �����ϱ� ���� �޼��带 �߰��մϴ�.
    public void StartMoving()
    {
        isMoving = true;
    }

    public void StopMoving()
    {
        isMoving = false;
    }

    void Update()
    {
        if (isMoving) // ������ �÷��׸� üũ�մϴ�.
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
    }
}
