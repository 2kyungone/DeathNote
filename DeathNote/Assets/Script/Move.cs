//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Move : MonoBehaviour
//{
//    private int w;
//    public static float speed = 0;
//    private int len;
//    private int num;
//    // Start is called before the first frame update
//    void Start()
//    {
//        w = Screen.width;
//        speed = 8;
//        len = w / 2;
//        num = MusicSelectBtn.limit;
//        Debug.Log(w);
//    }

//    private void Update()
//    {
//        num = MusicSelectBtn.limit;
//        ////일단 옮기고 내자리랑 limit이랑 차이를 구해서 그 차이가 8보다 작을 때 그냥 그만큼만 그속도로 그방향으로 보내준다.
//        if (num < Mathf.Round(transform.position.x) - len) 
//        {
//            if(Mathf.Abs((Mathf.Abs(transform.position.x) - Mathf.Abs(num))) < 8)
//            {
//                float temp = Mathf.Abs((Mathf.Abs(transform.position.x) - Mathf.Abs(num)));
//                transform.position = transform.position + new Vector3(1, 0, 0) * temp;
//            }
//            transform.position = transform.position + new Vector3(-1, 0, 0) * speed;
//        }
//        else if(num > Mathf.Round(transform.position.x) - len)
//        {
//            if (Mathf.Abs((Mathf.Abs(transform.position.x) - Mathf.Abs(num))) < 8)
//            {
//                float temp = Mathf.Abs((Mathf.Abs(transform.position.x) - Mathf.Abs(num)));
//                transform.position = transform.position + new Vector3(-1, 0, 0) * temp;
//            }
//            transform.position = transform.position + new Vector3(1, 0, 0) * speed;
//        }
        
//    }
//}
