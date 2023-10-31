using UnityEngine;

public class RandomJumpingMovement : MonoBehaviour
{
    public float speed = 5.0f; // �������� �ӵ�
    public float jumpForce = 5.0f; // ���� ��
    public float jumpInterval = 1.5f; // ���� ���� (��)

    private float direction; // �������� ���� (1: ������, -1: ����)
    private Rigidbody2D rb;
    private float nextJumpTime = 0; // ���� ���� �ð�

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // ���� �� ������ �������� ����
        direction = Random.Range(0, 2) == 0 ? -1 : 1;
    }

    private void Update()
    {
        // ���� �������� ������
        transform.Translate(direction * speed * Time.deltaTime, 0, 0);

        // ����
        if (Time.time > nextJumpTime)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            nextJumpTime = Time.time + jumpInterval;
        }
    }
}
