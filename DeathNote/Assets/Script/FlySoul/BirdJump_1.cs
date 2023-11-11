using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using : import
using UnityEngine.SceneManagement;

public class BirdJump_1 : MonoBehaviour
    //: MonoBehaviour: ��� �� ��� �޴´�.
{
    Rigidbody2D rb;

    public float jumpPower;
    //public���� �����ϸ� ����Ƽ������ ���� ������ �� ����

    //Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        //Rigidbody2D��� ������Ʈ�� ���ͼ� rb�� ��ڴ�.
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            //Input.GetMouseButtonDown : ���콺�� ������ ��
            //Input.GetMouseButton(0) : ���� ���콺�� ������ ��
        {
            GetComponent<AudioSource>().Play();
            // ������ �� �Ҹ��� �������� ���ִ� �Լ�
            rb.velocity = Vector2.up * jumpPower; // (0,1)
            //velocity: �ӵ�
                //Vector2:2d
                //Vector2.up : (0,1)
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
        //OnCollisionEnter2D : �ε����� �̺�Ʈ�� ���Ǵ� �޼ҵ�
    {
        SceneManager.LoadScene("GameOverScene");
    }
}
