using System.Collections;
using System.Collections.Generic;

public class TalkData
{
    //id 0�̸� ����, 1�̸� ���� ���, 2�̸� ��� ���
    public int id;
    public string content;

    public TalkData(int id, string content)
    {
        this.id = id; 
        this.content = content;
    }
}