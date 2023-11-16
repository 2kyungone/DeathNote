using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MoveBug : MonoBehaviour
{
    public float speed = 10.0f;
    private Vector2 direction;
    private float changeDirectionTime = 2.0f;
    public Transform boundaryTransform;
    private Vector2 boundarySize;
    private bool isMoving = true;
    public Button playButton;
    private bool isDragging = false;
    private Coroutine pulseCoroutine; // Pulse �ִϸ��̼��� ���� �ڷ�ƾ ����

    void Start()
    {
        if (boundaryTransform.GetComponent<SpriteRenderer>() != null)
        {
            boundarySize = boundaryTransform.GetComponent<SpriteRenderer>().bounds.size;
        }

        ChooseDirection();
        InvokeRepeating("ChooseDirection", changeDirectionTime, changeDirectionTime);
    }

    IEnumerator PulseButtonAnimation(Button button, bool isOverButton)
    {
        RectTransform rectTransform = button.GetComponent<RectTransform>();
        Vector3 originalScale = rectTransform.localScale;
        Vector3 minScale = new Vector3(1f, 1f, 1f);
        Vector3 maxScale = new Vector3(1.2f, 1.2f, 1f);

        while (true)
        {
            // ��ư ���� ���� ���� ũ�⸦ �����մϴ�.
            if (isOverButton)
            {
                rectTransform.localScale = maxScale;
            }
            else
            {
                // ��ư���� ����� ���� �޽� �ִϸ��̼��� �����մϴ�.
                float scale = Mathf.PingPong(Time.time, maxScale.x - minScale.x) + minScale.x;
                rectTransform.localScale = new Vector3(scale, scale, 1f);
            }
            yield return null;
        }
    }

    void Update()
    {
        if (isMoving)
        {
            MoveCharacter();
        }
    }

    void DragCharacter()
    {
        Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        touchPosition.z = 0f;
        transform.position = touchPosition;
    }

    bool IsOverButton(Vector2 screenPosition, Button button)
    {
        RectTransform rectTransform = button.GetComponent<RectTransform>();
        Camera camera = null;

        // Canvas�� Render Mode�� Screen Space - Overlay���� Screen Space - Camera������ ���� 
        // ������ ī�޶� ã�Ƽ� �Ѱ���� �մϴ�.
        Canvas canvas = button.GetComponentInParent<Canvas>();
        if (canvas.renderMode == RenderMode.ScreenSpaceOverlay)
        {
            camera = null; // Overlay ��忡���� ī�޶� ������� �ʽ��ϴ�.
        }
        else if (canvas.renderMode == RenderMode.ScreenSpaceCamera)
        {
            camera = canvas.worldCamera; // Camera ��忡���� �ش� Canvas�� ī�޶� ����մϴ�.
        }

        return RectTransformUtility.RectangleContainsScreenPoint(rectTransform, screenPosition, camera);
    }

    IEnumerator PulseButtonAnimation(Button button)
    {
        RectTransform rectTransform = button.GetComponent<RectTransform>();
        Vector3 minScale = new Vector3(1f, 1f, 1f);
        Vector3 maxScale = new Vector3(1.2f, 1.2f, 1f);
        Vector3 fixedScale = new Vector3(1.2f, 1.2f, 1f); // ���� ũ��

        while (true)
        {
            yield return null;
        }
    }


    void MoveCharacter()
    {
        Vector3 moveAmount = new Vector3(direction.x, direction.y, 0) * speed * Time.deltaTime;
        Vector3 newPos = transform.position + moveAmount;
        newPos.x = Mathf.Clamp(newPos.x, -boundarySize.x / 2, boundarySize.x / 2);
        newPos.y = Mathf.Clamp(newPos.y, -boundarySize.y / 2, boundarySize.y / 2);
        transform.position = newPos;
    }

    void ChooseDirection()
    {
        float h = Random.Range(-1f, 1f);
        float v = Random.Range(-1f, 1f);

        // ���ο� ������ �ʹ� ������ �ʵ��� �ּ� ũ�⸦ �����մϴ�.
        Vector2 newDirection = new Vector2(h, v);
        if (newDirection.magnitude < 0.5f) // �ּ� ũ�Ⱑ 0.5���� Ȯ���մϴ�.
        {
            newDirection = newDirection.normalized * 0.5f; // �ּ� ũ�⸦ �����մϴ�.
        }

        direction = newDirection;
    }
}
