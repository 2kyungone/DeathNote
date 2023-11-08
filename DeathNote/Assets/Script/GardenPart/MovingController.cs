using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MovingController : MonoBehaviour
{
    public float speed = 10.0f;  // ĳ������ ������ �ӵ�
    private Vector2 direction;  // ĳ���Ͱ� ������ ����
    private float changeDirectionTime = 2.0f;  // ������ �ٲٴ� �ð� ����
    public Transform boundaryTransform; // �������� ������ �̹����� Transform
    private Vector2 boundarySize;  // ������ �̹����� ũ��
    private bool isMoving; // �����̰� �ִ���
    private bool timer;
    Animator animator;
    
    void Start()
    {
        timer = false;
        animator = GetComponent<Animator>();
        // �������� ������ �̹����� ũ�⸦ SpriteRenderer ������Ʈ�� ���� �����ɴϴ�.
        if (boundaryTransform.GetComponent<SpriteRenderer>() != null)
        {
            boundarySize = boundaryTransform.GetComponent<SpriteRenderer>().bounds.size;
        }

        // �ʱ� ������ �����մϴ�.
        ChooseDirection();

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

    }
    IEnumerator TimeUp()
    {
        yield return new WaitForSeconds(2.0f);

        timer = false;
    }
    IEnumerator Stop()
    {
        yield return new WaitForSeconds(2.0f);

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
        
        if(h >= 0f)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
        else
        {
            Vector3 scale = transform.localScale;
            scale.x = -Mathf.Abs(scale.x);
            transform.localScale = scale;
        }

        direction = new Vector2(h, v).normalized;  // ������ ����ȭ�Ͽ� ���̰� 1�� �ǵ��� �մϴ�.
    }
}

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;


//public class MovingController : MonoBehaviour
//{
//    [SerializeField] RectTransform background; // ���
//    Animator animator; // 
//    float unitX; // �����̴� ����
//    bool isMoving; // ���� �����̰� �ִ���
//    bool isAnimating; // �ִϸ��̼��� ���������
//    // Start is called before the first frame update
//    void Start()
//    {
//        animator = GetComponent<Animator>();
//        unitX = background.rect.width / 10;
//        animator.SetTrigger("Idle");
//    }

//    // Update is called once per frame

//}
