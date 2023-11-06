using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;


public class StageManager : MonoBehaviour
{
    AudioSource audioSource; // ����� ����
    MusicManager musicManager; // �뷡 ������ ����ִ� ��ü

    public float areaX = 0; // ��Ʈ�� ��Ÿ���� ������ ���α���
    public float[] areasY = new float[11]; // ��Ʈ�� ��Ÿ���� ���ο����� �� �κ� ����

    public int beatNumber = 0; // �������� ������ ��Ʈ ��ȣ
    public double timePerBeat; // ��Ʈ �� �ɸ��� �ð�
    public Queue<NoteData> noteQueue; // ��Ʈ ������ ���� ť

    public double currentTime; // ���� ���� �ð�
    public double gameStart; // ���� ���� �ð�
    public double songStart; // �뷡 ���� �ð�

    private bool running = false; // ���� ������ ����
    private bool hasPlayed = false; // ���� ���� ����
    
    [SerializeField] GameObject judgeLine; // ������
    [SerializeField] TextMeshProUGUI title; // �� ����
    Image[] noteDownImage;
    ResultManager resultManager;


    void Start()
    {
        noteQueue = new Queue<NoteData>();
        // ���� ���� �ð�
        

        // MusicManager �̱����� �ҷ�����, �뷡 ����
        musicManager = MusicManager.instance;
        musicManager.setSunset();
        title.text = musicManager.musicTitle;
        resultManager = FindObjectOfType<ResultManager>();
        audioSource = musicManager.audioSource;
        // bpm�� 60���� ���� �ʴ� ��Ʈ���� ������ ��Ʈ�� ��
        timePerBeat = (60d / musicManager.bpm);
        // song�� 2����( musicManger.songBeat�� �ι� )���� ����
        songStart = timePerBeat * (musicManager.songBeat * 2);

        // �뷡�� ������̶�� ����
        running = true;
        // �뷡�� ����� �̷��� �ִٰ� ����
        hasPlayed = true;


        // ��Ʈ�� ������ ������ ũ������
        areaX = GetComponent<RectTransform>().rect.width;
        float areaY = GetComponent<RectTransform>().rect.height;
        noteDownImage = new Image[11];
        for (int i = 0; i < 11; i++)
        {
            areasY[i] = (areaY * i / 10) - areaY/2; // Y�� ��Ŀ�� �߾ӿ� ���������Ƿ�, areaY/2��ŭ ����� ��Ȯ�ϰ� ����
            
            // Center ǥ���� ���� ��ü
            Transform effect = transform.GetChild(2).GetChild(1).GetChild(i);
            Vector3 newPos = effect.localPosition;
            newPos.y = areasY[i];
            effect.localPosition = newPos;
            noteDownImage[i] = effect.GetComponent<Image>();
        }
        
        // ť�� �� ��Ʈ�� �����͸� �ִ´�.
        for (int i = 0; i < musicManager.totalNote; i++)
        {
            noteQueue.Enqueue(musicManager.getNoteData(i));
        }

        gameStart = AudioSettings.dspTime;
        // ����� �ҽ��� ����
        audioSource.PlayDelayed((float)(songStart + musicManager.offset + musicManager.customOffset));


    }


    void Update()
    {
        currentTime = AudioSettings.dspTime - gameStart; // ����ð�
        Debug.Log(gameStart);

        double timeDiffer = currentTime - (timePerBeat * beatNumber); // ���� ���ڰ� ���� �ð������� ����
        beatNumber = (int)(currentTime / timePerBeat);
        // ���� ��Ʈ �����ں��� ������ ���, ��Ʈ ���� �ø�
        
        // ��Ʈ�δ� ����
        Metronome(beatNumber, currentTime - (timePerBeat * beatNumber));


        if (!audioSource.isPlaying && running)
        {
            running = false;
           // StartCoroutine(ExecuteAfterDelay(2.0f));
        }
    }

    private void Metronome(int beatNumber, double timeDiff)
    {
        // ������ ��ġ ����
        judgeLine.transform.localPosition = new Vector3(checkPositionX(beatNumber, 0, timeDiff), judgeLine.transform.localPosition.y);

        // isEarly��, ���� ������ ��Ʈ�ѹ��� ��Ʈť�� ��Ʈ ���ʺ��� ū���� �ǹ��Ѵ�.
        // ���ÿ� �����ų�, �������� ������ ��Ʈ ������ �ݺ����� ������.
        bool isEarly = false;

        while (!isEarly)
        {
            // noteQueue�� �����ִ� �����Ͱ� �ִ��� Ȯ��
            if (noteQueue.TryPeek(out NoteData noteData))
            {
                // �����Ͱ� �ִٸ� ����(���� ������ �Ȳ��� ����� ������(��������) ���� ����)
                if (noteData.beat <= beatNumber)
                {
                    noteQueue.Dequeue();

                    if (noteData.length == 0)
                    {
                        GameObject note = NotePool.instance.normalQueue.Dequeue();
                        Note script = note.GetComponent<Note>();
                        script.SetNoteInfo(checkPositionX(noteData.beat + 3, noteData.posX, timeDiff), areasY[noteData.posY], checkTime(noteData.beat+3, noteData.posX), timePerBeat);
                        note.transform.SetAsFirstSibling();
                        note.SetActive(true);
                    }
                    else if (noteData.length >= 1)
                    {
                        // �ճ�Ʈ�� ���
                        float startPos = checkPositionX(beatNumber + 3, noteData.posX, timeDiff);
                        float endPos = checkPositionX(beatNumber + 3 + noteData.length, noteData.demi, timeDiff);
                        GameObject endNote = NotePool.instance.endQueue.Dequeue();
                        EndNote script2 = endNote.GetComponent<EndNote>();

                        script2.SetNoteInfo(endPos, areasY[noteData.posY], checkTime(noteData.beat + 3 + noteData.length, noteData.demi), timePerBeat);

                        GameObject longNote = NotePool.instance.longQueue.Dequeue();
                        LongNote script = longNote.GetComponent<LongNote>();
                        // �ճ�Ʈ�� ��ũ��Ʈ�� X�� ��ġ, Y�� ��ġ, ���� �ð�, �ð� ����, ����/������ ���θ� ����
                        script.SetNoteInfo(noteData.posY, startPos, areasY[noteData.posY], checkTime(noteData.beat + 3, noteData.posX), checkTime(noteData.beat + 3 + noteData.length, noteData.demi), timePerBeat, checkLeft(beatNumber + 3), script2);

                        Debug.Log("��ο�");
                        endNote.SetActive(true);
                        longNote.SetActive(true);


                        }
                        //else
                        //{
                        //    // �����̽� ��Ʈ�� ���
                        //}

                    }
                else
                {
                    // ���� ��Ʈ�� �� ũ�ٸ� ���� �����ʿ� ������ �ݺ��� ����
                    isEarly = true;
                }
            }
            else
            {   
                // ���̻� ������ ������ �ݺ��� ����
                isEarly = true;
            }
        }
    }

    IEnumerator ExecuteAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        resultManager.ShowResult();

    }

    // ��Ʈ�� ����� �ڷ�ƾ�̴�.

    // ��Ʈ�δ�


    public void noteDown(int i, bool stat)
    {
        noteDownImage[i].enabled = stat;
    }

    private float checkPositionX(int beatNumber, int tempo, double timeDiff)
    {
        // ��Ȯ�� ��Ʈ���� �������� ��ġ�� ��Ÿ����. 
        double beatPosStd = beatNumber % 8;

        // ���������� ���� ��츦 ��Ÿ���� ���� 2�γ��� ������ ���� �̿��Ѵ�.
        if ((beatNumber / 8) % 2 == 0)
            beatPosStd = 8 - beatPosStd;


        // �������� ����Ʈ�� ��ġ
        double linePosition = areaX * beatPosStd / 8;
        // �������� ���� ���� ����
        double deciPosition = areaX * tempo / 32;

        // �������� �������� ���� ��ġ  
        double error = (areaX / 8) * (timeDiff / timePerBeat);

        // �������� ��ġ
        Vector3 newPos = judgeLine.transform.localPosition;

        // ������ X��ǥ ��ġ ����
        if ((beatNumber / 8) % 2 == 1)
            return (float)(linePosition + deciPosition + error);
        else
            return (float)(linePosition - deciPosition - error);

    }

    private float checkTime(int beatNumber, int tempo)
    {
        return (float)(gameStart + beatNumber * timePerBeat + tempo * timePerBeat / 4);


    }


    private bool checkLeft(int beatNumber)
    {
        return ((beatNumber / 8) % 2 == 1);

    }

    

}
