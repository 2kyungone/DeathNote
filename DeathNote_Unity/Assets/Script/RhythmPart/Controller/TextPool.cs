using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextPool : MonoBehaviour
{
    [SerializeField] GameObject text = null; // �ۼ�Ʈ, ��ų

    public Queue<GameObject> textQueue = null;
    // public Queue<GameObject> longQueue = new Queue<GameObject>();

    void Start()
    {
        // �ڱ� �ڽ��� �ʱ�ȭ�� ��, ť���� ä������
        textQueue = InsertQueue(text, 50);
    }

    Queue<GameObject> InsertQueue(GameObject target, int idx)
    {
        // Queue ����
        Queue<GameObject> queue = new Queue<GameObject>();
        for (int i = 0; i < idx; i++)
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
}

