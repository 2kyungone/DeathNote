using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UI; : ���ھ �ٲ� ������ UI�� ����� ��ȭ�� �ֱ� ���� using�� ����ߵȴ�.

public class Score : MonoBehaviour
{
    public static int score = 0;
    public static int bestscore = 0;
    Text text = null;
    //static�� �ٿ������ν� �ٸ� Ŭ���������� score�� ���������ϴ�.

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = score.ToString();
        //GetComponent: Component�� ������.
        //ToString : int���� string���� ��ȯ
    }
}
