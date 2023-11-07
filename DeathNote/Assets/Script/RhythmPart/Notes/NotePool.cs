using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 
 * ��Ʈ�� ����� ������ ������ ���� ���� ť�� ������ ObjectPool�Դϴ�.
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
    public static NotePool instance;
    [SerializeField] ClickNote[] clickNotePool = new ClickNote[16];
    [SerializeField] EffectController[] effectPool = new EffectController[16];

    void Start()
    {
        // �ڱ� �ڽ��� �ʱ�ȭ�� ��, ť���� ä������
        instance = this;
    }

    /**
     * ����
     */
    //// ��Ʈ Ǯ�� �̸� �־���� ��Ʈ ����Ʈ
    //[SerializeField] NoteInfo[] normalNoteInfo = null;
    //[SerializeField] NoteInfo[] longNoteInfo = null;
    //[SerializeField] NoteInfo[] centerNoteInfo = null;
    //[SerializeField] NoteInfo[] endNoteInfo = null;

    //// �����θ� ȣ���ϴ� ����
    //public static NotePool instance;

    //// Object�� ��Ƶ� Queue
    //public Queue<GameObject> normalQueue = new Queue<GameObject>();
    //public Queue<GameObject> longQueue = new Queue<GameObject>();
    //public Queue<GameObject> centerQueue = new Queue<GameObject>();
    //public Queue<GameObject> endQueue = new Queue<GameObject>();

    //void Start()
    //{
    //    // �ڱ� �ڽ��� �ʱ�ȭ�� ��, ť���� ä������
    //    instance = this;
    //    normalQueue = InsertQueue(normalNoteInfo);
    //    longQueue = InsertQueue(longNoteInfo);
    //    centerQueue = InsertQueue(centerNoteInfo);
    //    endQueue = InsertQueue(endNoteInfo);
    //}

    //Queue<GameObject> InsertQueue(NoteInfo[] objectInfos)
    //{
    //    // Queue ����
    //    Queue<GameObject> queue = new Queue<GameObject>();

    //        foreach (NoteInfo objectInfo in objectInfos)
    //        {
    //            for (int i = 0; i < objectInfo.count; i++)
    //            {
    //                // ObjectInfo �迭�� ��� ��Ҹ� count��ŭ �����ϰ� ��Ȱ��ȭ �� �� Queue�� �־�д�.
    //                GameObject clone = Instantiate(objectInfo.goPrefab, transform.position, Quaternion.identity);
    //            // ��Ȱ��ȭ
    //            clone.SetActive(false);
    //            // ������ ����� �θ��� ����
    //            if (objectInfo.tfPoolParent != null)
    //            {
    //                clone.transform.SetParent(objectInfo.tfPoolParent);
    //            }
    //            else
    //                clone.transform.SetParent(transform);
    //            queue.Enqueue(clone);
    //            }
    //        }


    //    return queue;
    //}
}
