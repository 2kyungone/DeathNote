using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, List<TalkData>> scriptList;
    //�����׽��� 0�� ���� ���� 7�� 1~6���� ����
    public int storyId;

    void Awake()
    {
        //�ʱ�ȭ
        scriptList = new Dictionary<int, List<TalkData>>();
        GenerateData();
    }

    void GenerateData()
    {
        List<TalkData> openingData =  new List<TalkData> {
            new TalkData(1, "����, �̰� ������"),
            new TalkData(1, "���� �������ֳס�"),
            new TalkData(1, "���������� ���ֳ� �غ��")
        };

        scriptList.Add(0, openingData);
    }

    public int getStoryId()
    {
        return storyId;
    }

    public TalkData getTalk(int id, int idx)
    {
        if (idx == scriptList[id].Count) 
            return null; //��ȭ ������ null
        List<TalkData> data = scriptList[id];
        return data[idx];
    }
}
