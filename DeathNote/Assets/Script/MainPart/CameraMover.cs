using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public float speed = 5.0f;               // ī�޶��� �̵� �ӵ�
    public SpriteRenderer backgroundSprite;  // ��� ��������Ʈ

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

            // ��ġ ��ġ�� ĳ���Ͱ� �ִ��� Ȯ���մϴ�.
            if (touch.phase == TouchPhase.Began)
            {
                // ĳ���Ͱ� ������ �̵����� �ʽ��ϴ�.
                if (IsTouchingCharacter(touch.position))
                    return;

                SetMoveDirection(touch.position);
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                moveDirection = Vector3.zero;
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                // Ŭ�� ��ġ�� ĳ���Ͱ� �ִ��� Ȯ���մϴ�.
                if (IsTouchingCharacter(Input.mousePosition))
                    return;

                SetMoveDirection(Input.mousePosition);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                moveDirection = Vector3.zero;
            }
        }

        MoveCamera();
    }

    // ĳ���Ͱ� ��ġ�Ǿ����� Ȯ���ϴ� �޼ҵ�
    private bool IsTouchingCharacter(Vector2 screenPosition)
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);
        return hit.collider != null && hit.collider.CompareTag("Character");
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
