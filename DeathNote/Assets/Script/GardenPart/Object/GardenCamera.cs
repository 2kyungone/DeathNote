using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GardenCamera : MonoBehaviour
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

    public Transform followTarget; // ���� ���
    public bool isFollowing = false; // ����� ���󰡴��� ����

    private Vector3 lastMousePosition;
    private bool isDragging = false; // �巡�� ������ ����

    void Start()
    {
        cam = GetComponent<Camera>();
        spriteHalfSize = backgroundSprite.bounds.extents;

        camHalfHeight = cam.orthographicSize;
        camHalfWidth = camHalfHeight * cam.aspect;
    }

    void Update()
    {
        // isFollowing�� Ȱ��ȭ�� �Ǿ��ִٸ�, ��ü�� ���� ī�޶� �̵�.
        if (isFollowing && followTarget != null)
        {
            Vector3 targetPosition = followTarget.position; // Ÿ���� �������� ����
            targetPosition.z = transform.position.z; // ī�޶��� ������ ������ ����
            cam.orthographicSize = 35.0f; // ķ�� ����� ���Ʒ� 35��������
            transform.position = targetPosition; // ī�޶��� �������� �ٲ�
        }

        else if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // ��ġ�� ó�������Ǿ��� �� �� ������ �����ϰ� DragȰ��


            if (touch.phase == TouchPhase.Began)
            {
                // ĳ���Ͱ� ������ �̵����� �ʽ��ϴ�.
                if (IsTouchingCharacter(touch.position) || IsClickingUI(touch.position))
                    return;
                lastMousePosition = cam.ScreenToWorldPoint(touch.position);
                isDragging = true;

                
            }
            // ���� ��ġ�� �����ų� ��ҵǸ� Drag��Ȱ��
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                isDragging = false;
            }

            HandleTouchCameraDrag(touch);
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
    
                // ��ġ�� �� ���� ĳ���Ͱų� UI�� �� ��ũ��Ʈ�� ����
                if (!isDragging && (IsTouchingCharacter(Input.mousePosition) || IsClickingUI(Input.mousePosition)))
                    return;
                lastMousePosition = cam.ScreenToViewportPoint(Input.mousePosition);
                isDragging = true;

            }
            
        // ���콺�� ���� Drag ��Ȱ��ȭ
            else if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
            }
            HandleCameraDrag();
        }

        HandleZoom();
    }
    
    // ī�޶� ���󰩴ϴ�.
    public void SetTarget(Transform target)
    {
        if(!isFollowing)
        {
            isFollowing = true;
            followTarget = target;
        }

        else
        {
            if(followTarget.Equals(target))
            {
                isFollowing = false;
                followTarget = null;
            }
            else
            {
                followTarget = target;
            }
            
        }
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

            camHalfHeight = cam.orthographicSize;
            camHalfWidth = camHalfHeight * cam.aspect;
        }
    }

    // ĳ���Ͱ� ��ġ�Ǿ����� Ȯ���ϴ� �޼ҵ�
    private bool IsTouchingCharacter(Vector2 screenPosition)
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);
        return hit.collider != null && hit.collider.CompareTag("Character");
    }

    private bool IsClickingUI(Vector2 screenPosition)
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(screenPosition.x, screenPosition.y);

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

        return results.Count > 0;
    }


    void HandleCameraDrag()
    {
        // ù��° ���콺 �ٿ��� ��� �� ������ �����ϰ� Drag Ȱ��

        if (isDragging && Input.GetMouseButton(0))
        {
            Vector3 currentTouchPosition = cam.ScreenToViewportPoint(Input.mousePosition); // ���� ��ġ
            Vector3 difference = lastMousePosition - currentTouchPosition; // ��ġ����
            
            // ī�޶��� ��ġ�� ���������� ������ �ʰ� ����
            Vector3 clampedPosition = transform.position;
            clampedPosition.x = Mathf.Clamp(transform.position.x + difference.x, -spriteHalfSize.x + camHalfWidth, spriteHalfSize.x - camHalfWidth);
            clampedPosition.y = Mathf.Clamp(transform.position.y + difference.y, -spriteHalfSize.y + camHalfHeight, spriteHalfSize.y - camHalfHeight);
            transform.position = clampedPosition;

            
        }
    }
    void HandleTouchCameraDrag(Touch touch)
    {
       

        if (isDragging && touch.phase == TouchPhase.Moved)
        {
            Vector3 currentTouchPosition = cam.ScreenToWorldPoint(touch.position); // ���� ��ġ
            Vector3 difference = lastMousePosition - currentTouchPosition; // ��ġ����
            transform.position += difference; // ķ�� ��ġ ����

            // ī�޶��� ��ġ�� ���������� ������ �ʰ� ����
            Vector3 clampedPosition = transform.position;
            clampedPosition.x = Mathf.Clamp(transform.position.x, -spriteHalfSize.x + camHalfWidth, spriteHalfSize.x - camHalfWidth);
            clampedPosition.y = Mathf.Clamp(transform.position.y, -spriteHalfSize.y + camHalfHeight, spriteHalfSize.y - camHalfHeight);
            transform.position = clampedPosition;
        }
    }
}
