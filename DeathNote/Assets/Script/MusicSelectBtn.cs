using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSelectBtn : MonoBehaviour
{
    //x ����Ʈ�� �д�. 0, -1920, -3840, -5760
    public static int[] stage = { 0, -1, -2, -3 };
    public static int idx = 0;
    public static int limit = 0;
    private int w;


    public void Start()
    {
        limit = 0;
        idx = 0;
        w = Screen.width;
    }
    public void MoveNext()
    {
        if (idx < 3) 
        {
            idx++;
            limit = stage[idx] * w;
            if(Mathf.Abs(limit % 8) == 1)
            {
                limit += 1;
            }else if (Mathf.Abs(limit % 8) == 2)
            {
                limit += 2;
            }
            else if (Mathf.Abs(limit % 8) == 3)
            {
                limit += 3;
            }
            else if (Mathf.Abs(limit % 8) == 4)
            {
                limit += 4;
            }
            else if (Mathf.Abs(limit % 8) == 5)
            {
                limit -= 3;
            }
            else if (Mathf.Abs(limit % 8) == 6)
            {
                limit -= 2;
            }
            else if (Mathf.Abs(limit % 8) == 7)
            {
                limit -= 1;
            }
            else
            {
            }
        }
    }

    public void MoveBack()
    {
        if( idx > 0 )
        {
            idx--;
            limit = stage[idx] * w;
            if (Mathf.Abs(limit % 8) == 1)
            {
                limit -= 1;
            }
            else if (Mathf.Abs(limit % 8) == 2)
            {
                limit -= 2;
            }
            else if (Mathf.Abs(limit % 8) == 3)
            {
                limit -= 3;
            }
            else if (Mathf.Abs(limit % 8) == 4)
            {
                limit -= 4;
            }
            else if (Mathf.Abs(limit % 8) == 5)
            {
                limit += 3;
            }
            else if (Mathf.Abs(limit % 8) == 6)
            {
                limit += 2;
            }
            else if (Mathf.Abs(limit % 8) == 7)
            {
                limit += 1;
            }
        }
    }
}
