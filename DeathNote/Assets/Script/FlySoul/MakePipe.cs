using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakePipe : MonoBehaviour
{
    public GameObject pipe;
    //GameObject�� �ϳ� ���ؼ� �ű⿡ pipe�� �־��ֱ� ���� GameObject�� pipe��� �̸����� ������ش�.
    float timer = 0;
    public float timeDiff;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer+=Time.deltaTime;
        if (timer>timeDiff)
        {
        GameObject newpipe = Instantiate(pipe);
            newpipe.transform.position=new Vector3 (0.53f,Random.Range(-5f,-2.65f),0);
            timer = 0;
            Destroy(newpipe, 10.0f);
        }
    }
}
