using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class StageManager : MonoBehaviour
{
    AudioSource audioSource; // ����� ����
    MusicManager musicManager; // �뷡 ������ ��� �ִ� ��ü
    ScoreManager scoreManager; // ���� ������ ��� �ִ� ��ü
   
    public int beatNumber = 0; // �������� ������ ��Ʈ ��ȣ
    public double timePerBeat; // ��Ʈ �� �ɸ��� �ð�
    public Queue<NoteData> noteQueue; // ��Ʈ ������ ���� ť
    public float speed ;
    public double currentTime; // ���� ���� �ð�
    public double gameStart; // ���� ���� �ð�
    public double songStart; // �뷡 ���� �ð�

    public Color black = Color.black; // ��ο� ��
    public Color white = Color.white; // ��ο� ��
    public Color semiparent; // ���� ���� (�������)
    public Color transparent; // ���� ���� (�����)
    private bool running = false; // ���� ������ ����
    private bool hasPlayed = false; // ���� ���� ����
    private Animator bgAnimator;

    [SerializeField] ClickNote[] clicknotes;
    [SerializeField] EffectController[] effectControllers;
    [SerializeField] GameObject background;
    [SerializeField] Image thumbnail;
    [SerializeField] GameObject readyUI;
    [SerializeField] TextMeshProUGUI title; // �� ����
    [SerializeField] ResultManager resultManager;


    void Awake()
    {
        transparent = new Color(white.r, white.g, white.b, 0); // �������
        semiparent = new Color(white.r, white.g, white.b, 150f / 255f); // �ִ����

    }

    void Start()
    {
        readyUI.SetActive(true);
        noteQueue = new Queue<NoteData>(); // ��Ʈ ť ����
        bgAnimator = background.GetComponent<Animator>(); // ��� �ִϸ��̼� ����
        
        // MusicManager �̱����� �ҷ�����, �뷡 ����
        musicManager = MusicManager.instance;
        musicManager.SetSomeDay();
        //musicManager.SetSecondRun();
        title.text = musicManager.musicTitle;
        scoreManager = ScoreManager.instance;
        audioSource = musicManager.audioSource;
        Debug.Log("����:"+musicManager.beat.Length);
        // bpm�� 60���� ���� �ʴ� ��Ʈ���� ������ ��Ʈ�� ��
        timePerBeat = (60d / musicManager.bpm);
        // song�� 2����( musicManger.songBeat�� �ι� )���� ����
        songStart = timePerBeat * (musicManager.songBeat * 2);
        speed = musicManager.bpm / 120;
        // ��Ʈ�� �� ����Ʈ�� ���������ϴ�.
        for (int i = 0; i < 16; i++)
        {
            clicknotes[i].effectController = effectControllers[i];
            clicknotes[i].speed = 1.0f;

        }


        // ť�� �� ��Ʈ�� �����͸� �ִ´�.
        for (int i = 0; i < musicManager.totalNote; i++)
        {
            noteQueue.Enqueue(musicManager.getNoteData(i));
        }

        StartCoroutine(ReadyFinish());
        StartCoroutine(StartMusic(4.0f));
    }

    IEnumerator ReadyFinish()
    {
        yield return new WaitForSeconds(2.0f); // 2���� �ð��� ��ٸ�
        readyUI.SetActive(false); // �ٽ� �Ⱥ��̰���

        
    }

    IEnumerator StartMusic(float delay)
    {
        yield return new WaitForSeconds(delay);

        bgAnimator.speed = (float)musicManager.bpm / 240; // �ִϸ��̼� ����
        // �뷡�� ������̶�� ����
        running = true;
        // �뷡�� ����� �̷��� �ִٰ� ����
        hasPlayed = true;
        // ���� ���۽ð��� ����
        gameStart = AudioSettings.dspTime;
        // ����� ���
        audioSource.PlayDelayed((float)timePerBeat * 4 + musicManager.offset + musicManager.customOffset + speed);
        StartCoroutine(UpdateNote());
        bgAnimator.SetTrigger("start");
    }

    IEnumerator UpdateNote()
    {

        while (running)
        {
            // ������� ������ ��ħ
            float lerpValue = Mathf.Clamp01(((float)scoreManager.totalPercent / (musicManager.totalNote * 100))); // ����(Clamp�� 0~1�� ����)
            Debug.Log(lerpValue);
            thumbnail.color = Color.Lerp(transparent, semiparent, lerpValue);
            currentTime = AudioSettings.dspTime; // ����ð�
            int now = (int)((currentTime - gameStart) / timePerBeat);

            if(beatNumber != now)
            {
                beatNumber = now;
                // ��Ʈ�δ� ����
                Metronome(beatNumber);
            }
            

            if (noteQueue.Count == 0)
            {
                running = false;
                StartCoroutine(ExecuteAfterDelay(4.0f));
            }

            yield return null;
        }
        
    }

    IEnumerator ExecuteAfterDelay(float delay)
    {
        float startVolume = audioSource.volume;
        Color color = thumbnail.color;
        // fadeDuration ���� ���� ������ ���δ�.
        while (audioSource.volume > 0)
        {
            float lerpValue = 1f - audioSource.volume / startVolume; // ������ �پ��� ������ ���� lerpValue�� ����մϴ�.
            thumbnail.color = Color.Lerp(color, white, lerpValue); // �� ������ ���� ������ �����մϴ�.

            audioSource.volume -= startVolume * Time.deltaTime / delay;
            yield return null;
        }
        thumbnail.color = white;
        audioSource.Stop();
        audioSource.volume = startVolume; // ���� �������� �ٽ� ���� (��� �غ�)

        resultManager.ShowResult(musicManager.musicTitle, (float)scoreManager.totalPercent / (musicManager.totalNote * 100), scoreManager.score.text);

    }
    IEnumerator EnableNote(float info, ClickNote note, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (note.gameObject.activeSelf)
        {
            note.SetActive(false);
            // �ʿ��ϴٸ� ���⼭ ��� ����� ���� �ֽ��ϴ�.
            // yield return null; // ���� �����ӱ��� ���
        }
        note.SetNoteInfo(info, timePerBeat);
        note.SetActive(true);

    }

    public void SceneChange()
    {

    }

    public void SceneRestart()
    {
        // ���� Ȱ�� ���� �̸��� ����ϴ�.
        string sceneName = SceneManager.GetActiveScene().name;

        // ���� Ȱ�� ���� �ٽ� �ε��մϴ�.
        SceneManager.LoadScene(sceneName);
    }

    private void Metronome(int beatNumber)
    {
        //String str = "{";
        //for(int i = 1; i <= 300; i++)
        //{
        //    str += i * 10;
        //    str += ", ";
        //}
        //str += "}";
        //Debug.Log(str);
        // isEarly��, ���� ������ ��Ʈ�ѹ��� ��Ʈť�� ��Ʈ ���ʺ��� ū���� �ǹ��Ѵ�.
        // ���ÿ� �����ų�, �������� ������ ��Ʈ ������ �ݺ����� ������.
        bool isEarly = false;

        while (!isEarly)
        {
            // noteQueue�� �����ִ� �����Ͱ� �ִ��� Ȯ��
            if (noteQueue.TryPeek(out NoteData noteData))
            {
                // �����Ͱ� �ִٸ� ����(���� ������ �Ȳ��� ����� ������(��������) ���� ����)
                if (noteData.beat/10 <= beatNumber)
                {
                    noteQueue.Dequeue();

                    if (noteData.length == 0)
                    {
                        float nextTime = CheckTime((noteData.beat + 1) / 10, noteData.beat % 10);
                        ClickNote note = clicknotes[noteData.pos];
                        

                        StartCoroutine(EnableNote(nextTime+1.0f, note, (float)(nextTime - currentTime)));
                        
                    }
                    //else if (noteData.length >= 1)
                    //{
                    //    // �ճ�Ʈ�� ���
                    //    float startPos = checkPositionX(beatNumber + 3, noteData.posX, timeDiff);
                    //    gauges = new List<CenterNote>();

                    //    int start = noteData.beat + 3;

                    //    for (int i = 2; i <= noteData.length; i++)
                    //    {
                    //        GameObject centerNote = NotePool.instance.centerQueue.Dequeue();
                    //        CenterNote scripts = centerNote.GetComponent<CenterNote>();
                    //        scripts.SetNoteInfo(checkPositionX(start + i / 4, i % 4, timeDiff), areasY[noteData.posY], checkTime(start + i / 4, i % 4));
                    //        gauges.Add(scripts);
                    //        centerNote.SetActive(true);
                    //    }

                    //    GameObject longNote = NotePool.instance.longQueue.Dequeue();
                    //    LongNote script = longNote.GetComponent<LongNote>();
                    //    // �ճ�Ʈ�� ��ũ��Ʈ�� X�� ��ġ, Y�� ��ġ, ���� �ð�, �ð� ����, ����/������ ���θ� ����
                    //    script.SetNoteInfo(startPos, areasY[noteData.posY], checkTime(noteData.beat + 3, noteData.posX), checkTime(noteData.beat + 3 + noteData.length / 4, noteData.length % 4), timePerBeat);
                    //    script.gauges = gauges;
                    //    longNote.SetActive(true);


                    //}
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


    private float CheckTime(int beatNumber, int tempo)
    {
        return (float)(gameStart + beatNumber * timePerBeat + tempo * timePerBeat / 4);
    }


    /**
     * ���� �ý���
     */
    //AudioSource audioSource; // ����� ����
    //MusicManager musicManager; // �뷡 ������ ����ִ� ��ü

    //public float areaX = 0; // ��Ʈ�� ��Ÿ���� ������ ���α���
    //public float[] areasY = new float[11]; // ��Ʈ�� ��Ÿ���� ���ο����� �� �κ� ����

    //public int beatNumber = 0; // �������� ������ ��Ʈ ��ȣ
    //public double timePerBeat; // ��Ʈ �� �ɸ��� �ð�
    //public Queue<NoteData> noteQueue; // ��Ʈ ������ ���� ť

    //public double currentTime; // ���� ���� �ð�
    //public double gameStart; // ���� ���� �ð�
    //public double songStart; // �뷡 ���� �ð�

    //private bool running = false; // ���� ������ ����
    //private bool hasPlayed = false; // ���� ���� ����
    //private List<CenterNote> gauges; // �߾� ��Ʈ

    //[SerializeField] GameObject judgeLine; // ������
    //[SerializeField] TextMeshProUGUI title; // �� ����
    //Image[] noteDownImage;
    //ResultManager resultManager;


    //void Start()
    //{
    //    noteQueue = new Queue<NoteData>();
    //    // ���� ���� �ð�


    //    // MusicManager �̱����� �ҷ�����, �뷡 ����
    //    musicManager = MusicManager.instance;
    //    musicManager.setSunset();
    //    title.text = musicManager.musicTitle;
    //    resultManager = FindObjectOfType<ResultManager>();
    //    audioSource = musicManager.audioSource;
    //    // bpm�� 60���� ���� �ʴ� ��Ʈ���� ������ ��Ʈ�� ��
    //    timePerBeat = (60d / musicManager.bpm);
    //    // song�� 2����( musicManger.songBeat�� �ι� )���� ����
    //    songStart = timePerBeat * (musicManager.songBeat * 2);

    //    // �뷡�� ������̶�� ����
    //    running = true;
    //    // �뷡�� ����� �̷��� �ִٰ� ����
    //    hasPlayed = true;


    //    // ��Ʈ�� ������ ������ ũ������
    //    areaX = GetComponent<RectTransform>().rect.width;
    //    float areaY = GetComponent<RectTransform>().rect.height;
    //    noteDownImage = new Image[11];
    //    for (int i = 0; i < 11; i++)
    //    {
    //        areasY[i] = (areaY * i / 10) - areaY/2; // Y�� ��Ŀ�� �߾ӿ� ���������Ƿ�, areaY/2��ŭ ����� ��Ȯ�ϰ� ����

    //        // Center ǥ���� ���� ��ü
    //        Transform effect = transform.GetChild(2).GetChild(1).GetChild(i);
    //        Vector3 newPos = effect.localPosition;
    //        newPos.y = areasY[i];
    //        effect.localPosition = newPos;
    //        noteDownImage[i] = effect.GetComponent<Image>();
    //    }

    //    // ť�� �� ��Ʈ�� �����͸� �ִ´�.
    //    for (int i = 0; i < musicManager.totalNote; i++)
    //    {
    //        noteQueue.Enqueue(musicManager.getNoteData(i));
    //    }

    //    gameStart = AudioSettings.dspTime;
    //    // ����� �ҽ��� ����
    //    audioSource.PlayDelayed((float)(songStart + musicManager.offset + musicManager.customOffset));


    //}


    //void Update()
    //{
    //    currentTime = AudioSettings.dspTime - gameStart; // ����ð�
    //    Debug.Log(gameStart);

    //    double timeDiffer = currentTime - (timePerBeat * beatNumber); // ���� ���ڰ� ���� �ð������� ����
    //    beatNumber = (int)(currentTime / timePerBeat);
    //    // ���� ��Ʈ �����ں��� ������ ���, ��Ʈ ���� �ø�

    //    // ��Ʈ�δ� ����
    //    Metronome(beatNumber, currentTime - (timePerBeat * beatNumber));


    //    if (!audioSource.isPlaying && running)
    //    {
    //        running = false;
    //       // StartCoroutine(ExecuteAfterDelay(2.0f));
    //    }
    //}

    //private void Metronome(int beatNumber, double timeDiff)
    //{
    //    // ������ ��ġ ����
    //    judgeLine.transform.localPosition = new Vector3(checkPositionX(beatNumber, 0, timeDiff), judgeLine.transform.localPosition.y);

    //    // isEarly��, ���� ������ ��Ʈ�ѹ��� ��Ʈť�� ��Ʈ ���ʺ��� ū���� �ǹ��Ѵ�.
    //    // ���ÿ� �����ų�, �������� ������ ��Ʈ ������ �ݺ����� ������.
    //    bool isEarly = false;

    //    while (!isEarly)
    //    {
    //        // noteQueue�� �����ִ� �����Ͱ� �ִ��� Ȯ��
    //        if (noteQueue.TryPeek(out NoteData noteData))
    //        {
    //            // �����Ͱ� �ִٸ� ����(���� ������ �Ȳ��� ����� ������(��������) ���� ����)
    //            if (noteData.beat <= beatNumber)
    //            {
    //                noteQueue.Dequeue();

    //                if (noteData.length == 0)
    //                {
    //                    GameObject note = NotePool.instance.normalQueue.Dequeue();
    //                    Note script = note.GetComponent<Note>();
    //                    script.SetNoteInfo(checkPositionX(noteData.beat + 3, noteData.posX, timeDiff), areasY[noteData.posY], checkTime(noteData.beat+3, noteData.posX), timePerBeat);
    //                    note.transform.SetAsFirstSibling();
    //                    note.SetActive(true);
    //                }
    //                else if (noteData.length >= 1)
    //                {
    //                    // �ճ�Ʈ�� ���
    //                    float startPos = checkPositionX(beatNumber + 3, noteData.posX, timeDiff);
    //                    gauges = new List<CenterNote>();

    //                    int start = noteData.beat + 3;

    //                    for (int i = 2; i <= noteData.length; i++)
    //                    {
    //                        GameObject centerNote = NotePool.instance.centerQueue.Dequeue();
    //                        CenterNote scripts = centerNote.GetComponent<CenterNote>();
    //                        scripts.SetNoteInfo(checkPositionX(start + i / 4, i % 4, timeDiff), areasY[noteData.posY], checkTime(start + i / 4, i % 4));
    //                        gauges.Add(scripts);
    //                        centerNote.SetActive(true);
    //                    }

    //                    GameObject longNote = NotePool.instance.longQueue.Dequeue();
    //                    LongNote script = longNote.GetComponent<LongNote>();
    //                    // �ճ�Ʈ�� ��ũ��Ʈ�� X�� ��ġ, Y�� ��ġ, ���� �ð�, �ð� ����, ����/������ ���θ� ����
    //                    script.SetNoteInfo(startPos, areasY[noteData.posY], checkTime(noteData.beat + 3, noteData.posX), checkTime(noteData.beat + 3 + noteData.length / 4, noteData.length % 4), timePerBeat);
    //                    script.gauges = gauges;
    //                    longNote.SetActive(true);


    //                    }
    //                    //else
    //                    //{
    //                    //    // �����̽� ��Ʈ�� ���
    //                    //}

    //                }
    //            else
    //            {
    //                // ���� ��Ʈ�� �� ũ�ٸ� ���� �����ʿ� ������ �ݺ��� ����
    //                isEarly = true;
    //            }
    //        }
    //        else
    //        {   
    //            // ���̻� ������ ������ �ݺ��� ����
    //            isEarly = true;
    //        }
    //    }
    //}

    //IEnumerator ExecuteAfterDelay(float delay)
    //{
    //    yield return new WaitForSeconds(delay);
    //    resultManager.ShowResult();

    //}

    //// ��Ʈ�� ����� �ڷ�ƾ�̴�.

    //// ��Ʈ�δ�


    //public void noteDown(int i, bool stat)
    //{
    //    noteDownImage[i].enabled = stat;
    //}

    //private float checkPositionX(int beatNumber, int tempo, double timeDiff)
    //{
    //    // ��Ȯ�� ��Ʈ���� �������� ��ġ�� ��Ÿ����. 
    //    double beatPosStd = beatNumber % 8;

    //    // ���������� ���� ��츦 ��Ÿ���� ���� 2�γ��� ������ ���� �̿��Ѵ�.
    //    if ((beatNumber / 8) % 2 == 0)
    //        beatPosStd = 8 - beatPosStd;


    //    // �������� ����Ʈ�� ��ġ
    //    double linePosition = areaX * beatPosStd / 8;
    //    // �������� ���� ���� ����
    //    double deciPosition = areaX * tempo / 32;

    //    // �������� �������� ���� ��ġ  
    //    double error = (areaX / 8) * (timeDiff / timePerBeat);

    //    // �������� ��ġ
    //    Vector3 newPos = judgeLine.transform.localPosition;

    //    // ������ X��ǥ ��ġ ����
    //    if ((beatNumber / 8) % 2 == 1)
    //        return (float)(linePosition + deciPosition + error);
    //    else
    //        return (float)(linePosition - deciPosition - error);

    //}

    //private float checkTime(int beatNumber, int tempo)
    //{
    //    return (float)(gameStart + beatNumber * timePerBeat + tempo * timePerBeat / 4);


    //}


    //private bool checkLeft(int beatNumber)
    //{
    //    return ((beatNumber / 8) % 2 == 1);

    //}



}
