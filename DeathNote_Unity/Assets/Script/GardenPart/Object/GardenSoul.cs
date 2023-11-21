using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GardenSoul : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float speed = 10.0f;  // ĳ������ ������ �ӵ�
    private Vector2 direction;  // ĳ���Ͱ� ������ ����
    public TextMeshProUGUI soulName;
    public Transform boundaryTransform = null; // �������� ������ �̹����� Transform
    private Vector2 boundarySize;  // ������ �̹����� ũ��
    private bool isMoving; // �����̰� �ִ���
    private bool timer;
    public Canvas userCanvas;
    private bool isPointerDown = false;
    private float pointerDownTimer = 0;

    public bool zoomed;

    private Coroutine myCoroutine;
    public float requiredHoldTime = 3.0f;

    public Transform[] transforms;

    [SerializeField] SpriteRenderer[] sprites;
    public SoulDetail soulDetail;
    GardenCamera gardenCamera;

    // ��������Ʈ�� �ٲ�� �ϴ� ��ҵ�
    Animator animator;
    public Soul soul { get; set; }

    void Awake()
    {
        timer = false;

        gardenCamera = FindObjectOfType<GardenCamera>();
        // ��������Ʈ�� �ִϸ����� �ʱ�ȭ
        animator = GetComponent<Animator>();
        // �������� ������ �̹����� ũ�⸦ SpriteRenderer ������Ʈ�� ���� �����ɴϴ�.

        // �ʱ� ������ �����մϴ�.
        ChooseDirection();

    }

    void OnEnable()
   {
        if (boundaryTransform != null && boundaryTransform.GetComponent<SpriteRenderer>() != null)
        {
            boundarySize = boundaryTransform.GetComponent<SpriteRenderer>().bounds.size;
        }
        if(soul != null)
        {
            animator.SetInteger("body", soul.customizes[0]);
            animator.SetInteger("eyes", soul.customizes[1]);
            animator.SetInteger("bcolor", soul.customizes[2]);
            animator.SetInteger("ecolor", soul.customizes[3]);
            soulName.text = soul.name;
        }

    }

    public void ChangeName(string name)
    {
        soulName.text = name;
        soul.name = name;
        UserManager.instance.SaveData();
    }

    public void ReRender()
    {
        animator.SetInteger("body", soul.customizes[0]);
        animator.SetInteger("eyes", soul.customizes[1]);
        animator.SetInteger("bcolor", soul.customizes[2]);
        animator.SetInteger("ecolor", soul.customizes[3]);
        animator.SetTrigger("spawn");
    }


    void Update()
    {
        if (!timer)
        {
            timer = true;

            int random = UnityEngine.Random.Range(1, 100);

            if (random >= 50)
            {
                isMoving = true;
                ChooseDirection();
                StartCoroutine(Stop());
                StartCoroutine(Moving());
            }

            StartCoroutine(TimeUp());
        }

        // Y ���� ��������, �� ȭ�� �Ʒ��� �������� ���� ���� ���� �����ϴ�.
        // �� ���������� Y ��ġ�� -1000�� ���Ͽ� ���� ������ �����մϴ�.
        // -1000�� ���� ���� ����� ���� �� �ִ� ������ �ֱ� �����Դϴ�.
        // �� ���� ������Ʈ�� �ʿ信 ���� �����ؾ� �� �� �ֽ��ϴ�.
        for(int i = 0; i < sprites.Length; i++)
        {
            sprites[i].sortingOrder = i + Mathf.RoundToInt(transform.position.y * -1000);
        }
       

    }
    IEnumerator TimeUp()
    {
        yield return new WaitForSeconds(2.0f);

        timer = false;
    }
    IEnumerator Stop()
    {
        yield return new WaitForSeconds(1.8f);

        isMoving = false;
    }

    // isMoving���� �����̴� �ڷ�ƾ 
    IEnumerator Moving()
    {
        animator.SetTrigger("move");

        while (isMoving)
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

            yield return null;
        }

        animator.SetTrigger("idle");
    }


    // ������ ������ �����ϴ� �Լ��Դϴ�.
    void ChooseDirection()
    {

        float h = Random.Range(-1f, 1f);  // ���� ���� ���� ��
        float v = Random.Range(-1f, 1f);  // ���� ���� ���� ��

        if (h >= 0f)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x);
            transform.localScale = scale;
            Vector3 canvasScale = userCanvas.transform.localScale;
            canvasScale.x = Mathf.Abs(canvasScale.x);
            userCanvas.transform.localScale = canvasScale;

        }
        else
        {
            Vector3 scale = transform.localScale;
            scale.x = -Mathf.Abs(scale.x);
            transform.localScale = scale;
            Vector3 canvasScale = userCanvas.transform.localScale;
            canvasScale.x = -Mathf.Abs(canvasScale.x);
            userCanvas.transform.localScale = canvasScale;
        }

       

        direction = new Vector2(h, v).normalized;  // ������ ����ȭ�Ͽ� ���̰� 1�� �ǵ��� �մϴ�.
    }

    public void CameraZoom()
    {
        gardenCamera.SetTarget(transform);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPointerDown = true;
        myCoroutine = StartCoroutine(Countdown());
        gardenCamera.SetTarget(transform);
        soulDetail.OpenBook(this);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Reset();
    }

    private void Reset()
    {
        StopCoroutine(myCoroutine);
        isPointerDown = false;
        pointerDownTimer = 0;
    }

    private void OnLongPress()
    {
        SoulManager.instance.jumpSoul = soul;
        SceneManager.LoadScene("PlayScene");
    }

    private IEnumerator Countdown()
    {
        
        while (isPointerDown && pointerDownTimer < requiredHoldTime)
        {
            pointerDownTimer += Time.deltaTime;
            yield return null;
        }

        if (pointerDownTimer >= requiredHoldTime)
        {
            OnLongPress();
        }
    }
}

