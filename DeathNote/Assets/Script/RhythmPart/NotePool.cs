using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 
 * ��Ʈ�� ����� ������ ������ ���� ���� ť�� ������ ObjectPool�Դϴ�.
 * 
 ***/

[System.Serializable]
public class NoteInfo
{
    public GameObject goPrefab; // �̵��ϴ� ��Ʈ ����(prefab)
    public int count; // ť�� �� ����
    public Transform tfPoolParent; // ������ ��ġ
}

public class NotePool : MonoBehaviour
{
    // ObjectPool�� �̸� �־���� ��Ʈ ����Ʈ
    [SerializeField] NoteInfo[] noteInfo = null;

    // �����θ� ȣ���ϴ� ����
    public static NotePool instance;

    // Object�� ��Ƶ� Queue
    public Queue<GameObject> noteQueue = new Queue<GameObject>();

    void Start()
    {
        // ��� ������ �ʱ�ȭ
        instance = this;
        noteQueue = InsertQueue(noteInfo);

    }

    Queue<GameObject> InsertQueue(NoteInfo[] objectInfos)
    {
        // Queue ����
        Queue<GameObject> queue = new Queue<GameObject>();

            foreach (NoteInfo objectInfo in objectInfos)
            {
                for (int i = 0; i < objectInfo.count; i++)
                {
                    // ObjectInfo �迭�� ��� ��Ҹ� count��ŭ �����ϰ� ��Ȱ��ȭ �� �� Queue�� �־�д�.
                    GameObject clone = Instantiate(objectInfo.goPrefab, transform.position, Quaternion.identity);
                    // clone.GetComponent<Note>().isLeft = left;
                    clone.SetActive(false);
                if (objectInfo.tfPoolParent != null)
                {
                    clone.transform.SetParent(objectInfo.tfPoolParent);
                }
                else
                    clone.transform.SetParent(transform);
                queue.Enqueue(clone);
                }
            }

        
        return queue;
    }
}
