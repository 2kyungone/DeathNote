using UnityEngine;

public class MoveBackground : MonoBehaviour
{
    public float speed = 5.0f;   // ����� �̵� �ӵ�
    public Vector3 moveDirection = Vector3.left;  // �⺻������ �������� �̵�

    void Update()
    {
        transform.position += moveDirection * speed * Time.deltaTime;
    }
}
