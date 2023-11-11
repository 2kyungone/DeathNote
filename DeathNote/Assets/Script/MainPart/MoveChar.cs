using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI ���ӽ����̽� �߰�
using UnityEngine.SceneManagement;

public class MoveChar : MonoBehaviour
{
    public float speed = 10.0f;  // ĳ������ ������ �ӵ�
    private Vector2 direction;  // ĳ���Ͱ� ������ ����
    private float changeDirectionTime = 2.0f;  // ������ �ٲٴ� �ð� ����
    public Transform boundaryTransform; // �������� ������ �̹����� Transform
    private Vector2 boundarySize;  // ������ �̹����� ũ��
    private bool isMoving = true; // ĳ���Ͱ� �����̰� �ִ��� �����ϴ� ����
    private MoveChar selectedCharacter; // ���� ��ġ �Ǵ� Ŭ������ ���õ� ĳ���͸� �����մϴ�.
    public Button playButton; // UI�� PlayBtn ��ư ����
    private bool isDragging = false; // �巡�� ���¸� �����ϴ� ����

    void Start()
    {
        // �������� ������ �̹����� ũ�⸦ SpriteRenderer ������Ʈ�� ���� �����ɴϴ�.
        if (boundaryTransform.GetComponent<SpriteRenderer>() != null)
        {
            boundarySize = boundaryTransform.GetComponent<SpriteRenderer>().bounds.size;
        }

        // �ʱ� ������ �����մϴ�.
        ChooseDirection();

        // 2�ʸ��� ������ �ٲٵ��� �����մϴ�.
        InvokeRepeating("ChooseDirection", changeDirectionTime, changeDirectionTime);
    }

    void Update()
    {
        // PC������ ���콺 Ŭ�� �Ǵ� ����Ͽ����� ��ġ�� �����մϴ�.
        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            // ��ġ ��ġ�� ȭ�� ���� ��ǥ�� ��ȯ�մϴ�.
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            touchPosition.z = 0f; // 2D �����̹Ƿ� Z ��ġ�� �������� �ʽ��ϴ�.

            // ��ġ�� ��ġ�� ĳ���Ͱ� �ִ��� Ȯ���մϴ�.
            Collider2D collider = Physics2D.OverlapPoint(new Vector2(touchPosition.x, touchPosition.y));
            if (collider != null && collider.GetComponent<MoveChar>() == this) // Ȯ��: ���� ��ũ��Ʈ �ν��Ͻ��� �����ϴ� ��ü�� ��ġ�ߴ���
            {
                isDragging = true;
                isMoving = false; // �巡�� ���̸� �ڵ� �������� �ߴ��մϴ�.
                selectedCharacter = this; // ���� ĳ���͸� �����մϴ�.
                                          // PlayBtn �ִϸ��̼��� �����մϴ�.
                StartCoroutine(StartButtonAnimation(playButton, true));
            }
        }

        if (isDragging)
        {
            // �巡���ϴ� ���� ĳ���͸� ���콺 �Ǵ� ��ġ ��ġ�� �̵���ŵ�ϴ�.
            DragCharacter();
        }

        // ���콺 ��ư�� �������� ���� ����...
        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            isDragging = false;
            isMoving = true; // �巡�װ� �������Ƿ� �������� �ٽ� ����մϴ�.

            // ��ư ���� �巡�װ� �������� Ȯ���մϴ�. ���⼭ selectedCharacter.transform.position�� �״�� �ѱ�� ���,
            // Camera.main.WorldToScreenPoint�� ����Ͽ� ��ũ�� ��ǥ�� ��ȯ�ؾ� �մϴ�.
            Vector2 screenPoint = Camera.main.WorldToScreenPoint(selectedCharacter.transform.position);
            if (IsOverButton(screenPoint, playButton))
            {
                // �� ��ȯ ����
                SceneManager.LoadScene("PlayScene");
            }
            else
            {
                // PlayBtn �ִϸ��̼��� �����մϴ�.
                StartCoroutine(StartButtonAnimation(playButton, false));
            }

            selectedCharacter = null;  // ���� �����մϴ�.
        }


        // ĳ���Ͱ� ������ �� �ֵ��� ���� ��쿡�� �������� ����մϴ�.
        if (isMoving)
        {
            MoveCharacter();
        }
    }


    void DragCharacter()
    {
        Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        touchPosition.z = 0f; // 2D �����̹Ƿ� Z ��ġ�� �������� �ʽ��ϴ�.
        transform.position = touchPosition;
    }


    // ĳ���Ͱ� ��ư ���� �ִ��� Ȯ���ϴ� �Լ�
    bool IsOverButton(Vector3 characterPosition, Button button)
    {
        // ���� ��ǥ�� ��ũ�� ��ǥ�� ��ȯ�մϴ�.
        Vector2 screenPoint = Camera.main.WorldToScreenPoint(characterPosition);

        // UI�� RectTransform�� �����ɴϴ�.
        RectTransform rectTransform = button.GetComponent<RectTransform>();

        // ��ũ�� ��ǥ�� UI�� RectTransform�� ����Ͽ� ��ư ���� �ִ��� Ȯ���մϴ�.
        return RectTransformUtility.RectangleContainsScreenPoint(rectTransform, screenPoint, Camera.main);
    }


    IEnumerator StartButtonAnimation(Button button, bool show)
    {
        RectTransform rectTransform = button.GetComponent<RectTransform>();
        float time = 0.25f; // �ִϸ��̼ǿ� �ɸ��� �ð�
        float currentTime = 0f;
        Vector3 startScale = show ? Vector3.zero : Vector3.one;
        Vector3 endScale = show ? Vector3.one : Vector3.zero;

        if (show)
        {
            button.gameObject.SetActive(true);
        }

        while (currentTime < time)
        {
            currentTime += Time.deltaTime;
            float t = currentTime / time;
            rectTransform.localScale = Vector3.Lerp(startScale, endScale, t);
            yield return null;
        }

        rectTransform.localScale = endScale;

        if (!show)
        {
            button.gameObject.SetActive(false);
        }
    }

    void MoveCharacter()
    {
        // ������ ���� ����մϴ�.
        Vector3 moveAmount = new Vector3(direction.x, direction.y, 0) * speed * Time.deltaTime;

        // ���ο� ��ġ�� ����մϴ�.
        Vector3 newPos = transform.position + moveAmount;

        // ĳ���Ͱ� �̹����� �Ѿ�� �ʵ��� x�� y ��ġ�� �����մϴ�.
        newPos.x = Mathf.Clamp(newPos.x, -boundarySize.x / 2, boundarySize.x / 2);
        newPos.y = Mathf.Clamp(newPos.y, -boundarySize.y / 2, boundarySize.y / 2);

        // ĳ������ ��ġ�� ������Ʈ�մϴ�.
        transform.position = newPos;
    }
    
    // ������ ������ �����ϴ� �Լ��Դϴ�.
        void ChooseDirection()
    {
        float h = Random.Range(-1f, 1f);  // ���� ���� ���� ��
        float v = Random.Range(-1f, 1f);  // ���� ���� ���� ��
        direction = new Vector2(h, v).normalized;  // ������ ����ȭ�Ͽ� ���̰� 1�� �ǵ��� �մϴ�.
    }
}