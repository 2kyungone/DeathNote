using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowResult : MonoBehaviour
{
    [SerializeField] Text text;
    // Start is called before the first frame update
    public void duplicateName()
    {
        text.text = "�ߺ��� �г����Դϴ�";
    }

    public void successName()
    {
        text.text = "�г����� �����Ǿ����ϴ�";
    }
}
