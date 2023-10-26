using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, List<TalkData>> scriptList;
    //�����׽��� 0�� ���� ���� 7�� 1~6���� ����
    public int storyId;

    void Awake()
    {
        //�ʱ�ȭ
        //int�� ���丮�� ��ȣ
        scriptList = new Dictionary<int, List<TalkData>>();
        GenerateData();
    }

    void GenerateData()
    {
        List<TalkData> openingData =  new List<TalkData> {
            new TalkData(1, "��...�̰� ������"),
            new TalkData(2, "����, ����Ʈ�Ƚ��١�"),
            new TalkData(1, "���� �������ֳס�"),
            new TalkData(1, "���������� ���ֳ� �غ���?��")
        };
        scriptList.Add(0, openingData);

        List<TalkData> opening2Data = new List<TalkData> {
            //new TalkData(0, "(������ Ƣ��´�.)"),
            new TalkData(2, "���� ��� ������ �ҷ��� ����?��"),
            new TalkData(2, "���� �Ǻ��� �Ϻ��� �����ؾ߸� ������ Ǯ�����µ���"),
            new TalkData(1, "�����..! �� ���ߡ�"),
            new TalkData(2, "���� �Ǻ��� ���ɵ��� ���εǾ��־�.��"),
            new TalkData(2, "�������� �Ǻ��� �������� ���ɵ��� �Ի��� ������� ������.��"),
            new TalkData(2, "���ʶ�� ���ɵ��� Ǯ���� �� �ְھ�.��"),
            new TalkData(2, "������ ��ư�� ������. ���� �Ǻ��� ã�� ������ ������.��")
        };
        scriptList.Add(-1, opening2Data);

        //�̷��� �� ���丮 ������ ��� �ְ� storyId�� ����ȣ �־ ����ϸ� ��.
        //����1������ ���丮 4�� ���ߵǴϱ� ��� ������ �־���� �������� ����ϰ��ұ�?
        //����°���
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
