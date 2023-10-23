using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    AudioSource audioSource; // ����� ����
    public int bpm = 0; // beats per minute : 1�д� ��Ʈ �� 
    public int songBeat = 2; // ���� �뷡�� ��Ʈ ���� 
    public int stdBeat = 4; // ������ �Ǵ� ��Ʈ�� ���� ����
    public float offset = 0; // ������ : ������ ���۵� ����
    public float customOffset = 0; // ����� �ٲ� ������

    public float areaX = 0;
    public float[] areasY = new float[8];


    public int beatNumber = 0;

    public int songSpeed = 1; // �뷡 ��� �ӵ���, 1�� 2�ʿ� ��Ʈ�� ����
    public double timePerBeat; // 1��Ʈ�� �ð�

    public double currentTime; // ���� �ð�
    public double gameStart; // ���� ���� �ð�
    public double songStart; // �뷡 ���� �ð�

    private bool running = false; // ���� ������ ����
    private bool hasPlayed = false; // ���� ���� ����
    //                                     /              /                                                              ��, ��, ��           /           /           /          /            /           /          /            /           /                                                                                    
    int[] notes = new int[] { 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 21, 23, 25, 26, 27, 29, 31, 33, 34, 35, 37, 39, 41, 43, 45, 49, 50, 51, 53, 54, 55, 57, 58, 59, 61, 62, 63, 65, 66, 67, 69, 70, 71, 73, 74, 75, 77, 78, 79, 81, 82, 83, 85, 86, 87, 89, 90, 91, 93, 94, 95, 97, 98, 99, 101, 102, 103, 104, 105, 106, 107, 109, 109, 114, 115, 116, 117, 122, 123, 124, 125, 130, 131, 132, 133, 138, 139, 140, 141, 141, 142, 143, 143, 144, 146, 147, 148, 149, 154, 155, 156, 157, 159, 161, 163, 165, 167, 169, 169, 173, 173, 178, 179, 180, 183, 184, 186, 187, 188, 191, 192, 194, 195, 196, 198, 200, 201, 203, 205, 207, 209, 209, 214, 215, 216, 217, 220, 221, 222, 223, 224, 226, 227, 228, 229, 229, 231, 231, 233, 233, 238, 239, 240, 242, 243, 244, 245, 245, 247, 247, 249, 249, 254, 255, 256, 258, 259, 260, 261, 261, 263, 263, 265, 266, 267, 269, 271, 273, 274, 275, 277, 279, 281, 281, 282, 282, 283, 283, 285, 287, 289, 291, 293, 293, 297, 297 };
    int[] notesPosY = new int[] { 3, 4, 5, 6, 2, 3, 4, 5, 5, 5, 5, 5, 2, 2, 2, 2, 2, 5, 5, 5, 5, 5, 2, 1, 4, 7, 4, 4, 4, 3, 3, 3, 4, 4, 4, 3, 3, 3, 4, 4, 4, 3, 3, 3, 4, 4, 4, 3, 3, 3, 2, 3, 4, 4, 5, 6, 6, 5, 4, 4, 3, 2, 2, 1, 0, 0, 1, 2, 3, 3, 3, 4, 0, 7, 2, 6, 3, 7, 1, 5, 2, 6, 0, 4, 1, 5, 4, 3, 4, 2, 5, 3, 2, 5, 2, 2, 5, 3, 6, 1, 4, 2, 5, 4, 1, 3, 2, 4, 5, 2, 4, 4, 3, 4, 4, 3, 4, 6, 2, 2, 1, 2, 2, 1, 2, 4, 3, 3, 4, 2, 2, 3, 4, 2, 1, 4, 5, 6, 2, 4, 7, 1, 3, 0, 4, 1, 2, 3, 4, 5, 2, 1, 3, 4, 1, 2, 4, 1, 3, 6, 2, 4, 1, 5, 2, 4, 6, 2, 1, 5, 4, 7, 3, 2, 1, 0, 5, 4, 3, 2, 1, 5, 2, 6, 3, 7, 0, 1, 3, 5, 2, 4, 6, 7, 4, 1, 4 };
    [SerializeField] GameObject judgeLine = null;


    public Queue<int[]> noteQueue = new Queue<int[]>();

    ResultManager resultManager;


    void Start()
    {

        string s = "{";
        for (int i = 0; i < notes.Length; i++)
        {
            s += 4 + ",";
        }
        s += "}";

        Debug.Log(notesPosY.Length);
        Debug.Log(notes.Length);
        // ����� �ʺ�� ���̸� ���Ѵ�.
        areaX = GetComponent<RectTransform>().rect.width;
        float areaY = GetComponent<RectTransform>().rect.height;

        for(int i = 0; i < 8; i++)
        {
            areasY[i] = (areaY * i / 7) - areaY/2;
        
        }
        // 8��Ʈ �ڿ� �뷡�� ����

        for(int i = 0; i < notes.Length; i++)
        {
            noteQueue.Enqueue(new int[] { notes[i], notesPosY[i] });
        }

        songStart = (timePerBeat * 7);
        resultManager = FindObjectOfType<ResultManager>();

        audioSource = GetComponent<AudioSource>();
        gameStart = AudioSettings.dspTime;
        // bpm�� 60���� ���� �ʴ� ��Ʈ���� ������ ��Ʈ�� ���̸�, �̸� ��Ʈ������ ���Ѵ�.
        timePerBeat = (60d / bpm) * ((double)songBeat / stdBeat);
        songStart = timePerBeat * 7;
        //string s = "";
        //for(int i = 4; i <= 1000; i++)
        //{
        //    s = s + i + ",";
        //}

        //Debug.Log(s);

    }

    void Update()
    {

        currentTime = AudioSettings.dspTime - gameStart; // ����ð�
        double timeDiffer = currentTime - (timePerBeat * beatNumber); // ���� ���ڰ� ���� �ð������� ����

        // ���� ��Ʈ�� �����ں��� ������ ���, ��Ʈ ���� �ø�
        if (timeDiffer >= 0)
        {
            // ���� ���ڱ��� ��Ʈ ������ �ø�
            while (beatNumber * timePerBeat <= currentTime)
            {
                beatNumber++;
            }
        }

        // ��Ʈ�δ� ����
        Metronome(beatNumber-1, currentTime - (timePerBeat * (beatNumber-1)));


        if (!audioSource.isPlaying && running)
        {
            running = false;
            StartCoroutine(ExecuteAfterDelay(2.0f));
        }
    }
        
    IEnumerator ExecuteAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        resultManager.ShowResult();

    }

    // ��Ʈ�� ����� �ڷ�ƾ�̴�.

    // ��Ʈ�δ�

    private void Metronome(int beatNumber, double timeDiff)
    {
        // �뷡�� ������ 8��Ʈ�� �����ϹǷ� 7��Ʈ�� �����Ѵ�. 
        if (!running && !hasPlayed && (currentTime >= (songStart - offset)))
        {
            // �뷡�� ������̶�� ����
            running = true;
            // �뷡�� ����� �̷��� �ִٰ� ����
            hasPlayed = true;

            // ���� ����(8��Ʈ)�� �뷡�� �����Ű��, ������ ������ŭ�� ���ش�.
            audioSource.PlayDelayed((float)(timePerBeat - (currentTime - (songStart-offset))));
        }


        // ��Ʈ�� 1���� ������ �����Ѵ�.
        // ��Ȯ�� ��Ʈ���� �������� ��ġ�� ��Ÿ����. 
        double beatPosStd = beatNumber % 8;
        // 4��Ʈ ���� ������������ ��ġ�� ��Ÿ����.
        double notePosStd = (beatNumber + 3) % 8;

        // ���������� ���� ��츦 ��Ÿ���� ���� 2�γ��� ������ ���� �̿��Ѵ�.
        if ((beatNumber / 8) % 2 == 1)
            beatPosStd = 8 - beatPosStd;
        if (((beatNumber + 3) / 8) % 2 == 1)
            notePosStd = 8 - notePosStd;


        // �������� ����Ʈ�� ��ġ
        double linePosition = areaX * beatPosStd/8;
        // ��Ʈ�� ����Ʈ�� ��ġ
        double notePosition = areaX * notePosStd / 8;

        // �������� �������� ���� ��ġ  
        double error = (areaX / 8) * (timeDiff / timePerBeat);

        // �������� ��ġ
        Vector3 newPos = judgeLine.transform.localPosition;

        // ������ X��ǥ ��ġ ����
        if ((beatNumber / 8) % 2 == 0)
            newPos.x = (float)(linePosition + error);
        else
            newPos.x = (float)(linePosition - error);

        // ������ ��ġ ����
        judgeLine.transform.localPosition = newPos;

        // beatNumber�� Ȯ���ϰ�, ť���� ������.

        // isEarly��, ���� ������ ��Ʈ�ѹ��� ��Ʈť�� ��Ʈ ���ʺ��� ū���� �ǹ��Ѵ�.
        // ���ÿ� �����ų�, �������� ������ ��Ʈ ������ �ݺ����� ������.
        bool isEarly = false;

        while (!isEarly)
        {
            if (noteQueue.TryPeek(out int[] noteInfo))
            {

                if (noteInfo[0] <= beatNumber)
                {
                    noteQueue.Dequeue();
                    GameObject note = NotePool.instance.noteQueue.Dequeue();
                    WideNote script = note.GetComponent<WideNote>();
                    script.SetNoteInfo((float)notePosition, areasY[noteInfo[1]], gameStart + (beatNumber + 3) * timePerBeat, timePerBeat);
                    note.SetActive(true);
                }
                else
                {
                    isEarly = true;
                }
            }
            else
            {
                isEarly = true;
            }
        }
    }

}
