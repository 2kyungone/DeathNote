using UnityEngine;

public class CameraSizeChanger : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Camera mainCamera;
    private float originalCameraSize; // ���� ī�޶� ũ�⸦ ������ ����

    void Start()
    {
        // �� ��ũ��Ʈ�� ÷�ε� ���� ������Ʈ���� Sprite Renderer ������Ʈ�� �����ɴϴ�.
        spriteRenderer = GetComponent<SpriteRenderer>();

        // ���� ī�޶� ã���ϴ�.
        mainCamera = Camera.main;

        // ���� ī�޶� ũ�⸦ �����մϴ�.
        originalCameraSize = mainCamera.orthographicSize;
    }

    void Update()
    {
        // Sprite�� �̸��� Ȯ���մϴ�.
        string spriteName = spriteRenderer.sprite.name;

        // �̸��� "1"�̳� "2"�� ���, ī�޶��� Size�� 148�� �����մϴ�.
        if (spriteName == "1" || spriteName == "2")
        {
            mainCamera.orthographicSize = 148;
        }
        else // �� ���� ��쿡�� ī�޶� ũ�⸦ ������� �����ϴ�.
        {
            mainCamera.orthographicSize = originalCameraSize;
        }
    }
}
