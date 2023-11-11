using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 
 * ��Ʈ�� ����� ������ ������ ���� ���� ť�� ������ ObjectPool�Դϴ�.
 * ClickNote ������, �� ��ġ�� ObjectPool�� �����մϴ�.
 ***/

public class NotePool : MonoBehaviour
 {
    [SerializeField] GameObject note = null; // ��Ʈ
    [SerializeField] GameObject effect = null; // ����Ʈ
    public Queue<GameObject> clickQueue = null;
    public Queue<GameObject> effectQueue = null;
    // public Queue<GameObject> longQueue = new Queue<GameObject>();

    void Start()
    {
        // �ڱ� �ڽ��� �ʱ�ȭ�� ��, ť���� ä������
        clickQueue = InsertQueue(note);
        effectQueue = EffectQueue(effect);
    }

    Queue<GameObject> InsertQueue(GameObject target)
    {
        // Queue ����
        Queue<GameObject> queue = new Queue<GameObject>();
        for (int i = 0; i < 4; i++)
        {
            // ObjectInfo �迭�� ��� ��Ҹ� count��ŭ �����ϰ� ��Ȱ��ȭ �� �� Queue�� �־�д�.
            GameObject clone = Instantiate(target, transform.position, Quaternion.identity);
            // ��Ȱ��ȭ
            clone.SetActive(false);
            // ������ ����� �θ��� ����
            clone.transform.SetParent(transform);
            queue.Enqueue(clone);
        }


        return queue;
    }

    Queue<GameObject> EffectQueue(GameObject target)
    {
        // Queue ����
        Queue<GameObject> queue = new Queue<GameObject>();
        for (int i = 0; i < 4; i++)
        {
            // ObjectInfo �迭�� ��� ��Ҹ� count��ŭ �����ϰ� ��Ȱ��ȭ �� �� Queue�� �־�д�.
            GameObject clone = Instantiate(target, transform.position, Quaternion.identity);
            // ������ ����� �θ��� ����
            clone.transform.SetParent(transform);
            queue.Enqueue(clone);
        }


        return queue;
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
