using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderMove : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left*speed*Time.deltaTime;
        //transform: �̰� �ٷ� ���� �� �ִ�.
        //Vector3.left : (-1, 0, 0)
        //Time.deltaTime : ���� �������� �Ϸ�Ǵµ����� �ɸ� �ð�(FPS ���������� ���)
    }
}
