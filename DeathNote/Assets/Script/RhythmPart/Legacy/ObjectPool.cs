using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 
 * ��Ʈ�� ����� ������ ������ ���� ���� ť�� ������ ObjectPool�Դϴ�.
 * 
 ***/

[System.Serializable] public class ObjectInfo
{
    public GameObject goPrefab; // �̵��ϴ� ��Ʈ ����(prefab)
    public int count; // ť�� �� ����
    public Transform tfPoolParent;
}

public class ObjectPool : MonoBehaviour
{    
    // ObjectPool�� �̸� �־���� ��Ʈ ����Ʈ
    [SerializeField] ObjectInfo[] leftObjectInfo = null;
    [SerializeField] ObjectInfo[] rightObjectInfo = null;
    [SerializeField] ObjectInfo[] leftMetronomeInfo = null;
    [SerializeField] ObjectInfo[] rightMetronomeInfo = null;

    // �����θ� ȣ���ϴ� ����
    public static ObjectPool instance;

    // Object�� ��Ƶ� Queue
    public Queue<GameObject> leftMetronome = new Queue<GameObject>();
    public Queue<GameObject> rightMetronome = new Queue<GameObject>();
    public Queue<GameObject> leftQueue = new Queue<GameObject>();
    public Queue<GameObject> rightQueue = new Queue<GameObject>();

    void Start()
    {
        // ��� ������ �ʱ�ȭ
        instance = this;
        leftMetronome = InsertQueue(leftMetronomeInfo, true);
        rightMetronome = InsertQueue(rightMetronomeInfo, false);
        leftQueue = InsertQueue(leftObjectInfo, true);
        rightQueue = InsertQueue(rightObjectInfo, false);

    }

    Queue<GameObject> InsertQueue(ObjectInfo[] objectInfos, bool left)
    {
        // Queue ����
        Queue<GameObject> queue = new Queue<GameObject>();
        for(int x = 0; x < 50; x++)
        {
            foreach (ObjectInfo objectInfo in objectInfos)
            {
                for (int i = 0; i < objectInfo.count; i++)
                {
                    // ObjectInfo �迭�� ��� ��Ҹ� count��ŭ �����ϰ� ��Ȱ��ȭ �� �� Queue�� �־�д�.
                    GameObject clone = Instantiate(objectInfo.goPrefab, transform.position, Quaternion.identity);
                    clone.GetComponent<Note>().isLeft = left;
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
        
        }
        return queue;
    }
}
