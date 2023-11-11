using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private int w;
    public static float speed = 0;
   private int len;
    private int num;
    void Start()
    {
        w = Screen.width;
        speed = 8;
        len = w / 2;
        num = MusicSelectBtn.limit;
    }

    private void Update()
    {
        num = MusicSelectBtn.limit;
        ////�ϴ� �ű�� ���ڸ��� limit�̶� ���̸� ���ؼ� �� ���̰� 8���� ���� �� �׳� �׸�ŭ�� �׼ӵ��� �׹������� �����ش�.
        if (num < Mathf.Round(transform.position.x) - len) 
        {
            if(Mathf.Abs((Mathf.Abs(transform.position.x) - Mathf.Abs(num))) < 8)
            {
                float temp = Mathf.Abs((Mathf.Abs(transform.position.x) - Mathf.Abs(num)));
                transform.position = transform.position + new Vector3(1, 0, 0) * temp;
            }
            transform.position = transform.position + new Vector3(-1, 0, 0) * speed;
        }
        else if(num > Mathf.Round(transform.position.x) - len)
        {
            if (Mathf.Abs((Mathf.Abs(transform.position.x) - Mathf.Abs(num))) < 8)
            {
                float temp = Mathf.Abs((Mathf.Abs(transform.position.x) - Mathf.Abs(num)));
                transform.position = transform.position + new Vector3(-1, 0, 0) * temp;
            }
            transform.position = transform.position + new Vector3(1, 0, 0) * speed;
        }
       
    }
}
