using UnityEngine;

public class fly : MonoBehaviour
{
    public float floatStrength = 0.5f; // �������� ��. �� ���� �����Ͽ� �������� ������ ������ �� �ֽ��ϴ�.
    public float amplitude = 0.1f; // �������� ������ �����ϴ� ����
    private Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.position; // �ʱ� ��ġ ����
    }

    void Update()
    {
        // Mathf.Abs�� ����Ͽ� Sin �Լ��� ���� �׻� ����� �ǵ��� �մϴ�.
        // �߰��� amplitude�� ���Ͽ� �������� ������ �����մϴ�.
        transform.position = originalPosition + new Vector3(0.0f, Mathf.Abs(Mathf.Sin(Time.time) * amplitude) * floatStrength, 0.0f);
    }
}
