using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public float speed = 5.0f;               // ī�޶��� �̵� �ӵ�
    public SpriteRenderer backgroundSprite;  // ��� ��������Ʈ
    public float minSize = 5.0f; // ī�޶��� �ּ� Orthographic Size
    public float maxSize = 100.0f; // ī�޶��� �ִ� Orthographic Size
    private Camera cam;
    private Vector2 spriteHalfSize;
    private float camHalfHeight;
    private float camHalfWidth;
    private Vector3 moveDirection = Vector3.zero;

    void Start()
    {
        cam = GetComponent<Camera>();
        spriteHalfSize = backgroundSprite.bounds.extents;

        camHalfHeight = cam.orthographicSize;
        camHalfWidth = camHalfHeight * cam.aspect;
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                SetMoveDirection(touch.position);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                moveDirection = Vector3.zero;
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                SetMoveDirection(Input.mousePosition);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                moveDirection = Vector3.zero;
            }
        }

        MoveCamera();
        HandleZoom();
    }

    void HandleZoom()
    {
        // ���콺 �� �Է��� ������� �ϴ� ��
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0.0f)
        {
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize - scroll * speed, minSize, maxSize);
        }

        // ��ġ �� �Է��� ������� �ϴ� �� - ����� ��ġ���� ���
        if (Input.touchCount == 2)
        {
            // �� ��ġ�� ���� ��ġ�� ���� ��ġ�� �����ɴϴ�.
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // �� ��ġ ������ ������ ũ�⸦ ���մϴ�.
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // �� ������ ũ�� ���̸� ���մϴ�.
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            // ī�޶��� orthographicSize�� �����մϴ�.
            cam.orthographicSize += deltaMagnitudeDiff * speed;

            // ī�޶��� ũ�Ⱑ �ּҰ� �Ǵ� �ִ밪�� ���� �ʵ��� �մϴ�.
            cam.orthographicSize = Mathf.Max(cam.orthographicSize, minSize);
            cam.orthographicSize = Mathf.Min(cam.orthographicSize, maxSize);
        }
    }

void SetMoveDirection(Vector2 screenPosition)
    {
        Vector2 viewportPosition = cam.ScreenToViewportPoint(screenPosition);
        if (viewportPosition.x < 0.25f) moveDirection = Vector3.left;
        else if (viewportPosition.x > 0.75f) moveDirection = Vector3.right;
        else if (viewportPosition.y < 0.25f) moveDirection = Vector3.down;
        else if (viewportPosition.y > 0.75f) moveDirection = Vector3.up;
        else moveDirection = Vector3.zero;
    }

    void MoveCamera()
    {
        Vector3 moveAmount = moveDirection * speed * Time.deltaTime;
        transform.position += moveAmount;

        // ī�޶��� ��ġ�� ��������Ʈ�� ��� ������ ����
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(transform.position.x, -spriteHalfSize.x + camHalfWidth, spriteHalfSize.x - camHalfWidth);
        clampedPosition.y = Mathf.Clamp(transform.position.y, -spriteHalfSize.y + camHalfHeight, spriteHalfSize.y - camHalfHeight);
        transform.position = clampedPosition;
    }
}
