using UnityEngine;

public class RandomJumpingMovement : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpForce = 5.0f;
    private Rigidbody2D rb;
    private float direction;
    private float nextActionTime = 0;
    private float actionInterval = 2.0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        direction = Random.Range(0, 2) == 0 ? -1 : 1;
    }

    private void Update()
    {
        // ������ �ð� �������� ������ �ൿ�� ����
        if (Time.time > nextActionTime)
        {
            int action = Random.Range(0, 3); // 0: ���� ����, 1: ����, 2: �״��

            switch (action)
            {
                case 0: // ���� ����
                    direction *= -1;
                    break;
                case 1: // ����
                    rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                    break;
                case 2: // �״��
                    // �ƹ��͵� ���� ����
                    break;
            }

            nextActionTime = Time.time + actionInterval;
        }

        // ���� �������� ������
        transform.Translate(direction * speed * Time.deltaTime, 0, 0);
    }
}
