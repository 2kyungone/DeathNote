using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NoteData
{
    public int beat;
    public int posX;
    public int posY;
    public int length;
    public int demi;

    public NoteData(int b, int x, int y, int l, int d)
    {
        this.beat = b;
        this.posX = x;
        this.posY = y;
        this.length = l;
        this.demi = d;
    }
}

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    public AudioSource audioSource; // ����� ����
    public string musicTitle; // ������ ����
    public float musicLength; // ������ ����
    public float musicEnd; // ������ ������ �ð�
    public int totalNote; // �� ��Ʈ�� ����
    public int bpm = 0; // beats per minute : 1�д� ��Ʈ �� 
    public int songBeat = 4; // �� ������ ��Ʈ �� 
    public int stdBeat = 4; // �� ��Ʈ�� ����
    public float offset = 0; // �뷡�� ���۵Ǵ� ���۵� ����
    public float customOffset = 0; // �÷��̾ �ٲ� ������

    int[] beat;
    int[] posX;
    int[] posY;
    int[] length;
    int[] demi;

    // �̱��� �ν��Ͻ��� �����ϴ� �Ӽ�

    void Awake()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    public NoteData getNoteData(int idx)
    {
        return new NoteData(beat[idx], posX[idx], posY[idx], length[idx], demi[idx]);
    }

    public void setChristmas()
    {
        AudioClip clip = Resources.Load<AudioClip>("Sound/ChristmasMemory");
        musicLength = clip.length;
        musicEnd = 0;
        audioSource.clip = clip;
        bpm = 170;
        songBeat = 4;
        stdBeat = 4;
        offset = (float)-0.13;
        customOffset = (float)0.0;
        totalNote = 197;
        beat = new int[] { 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 21, 23, 25, 26, 27, 29, 31, 33, 34, 35, 37, 39, 41, 43, 45, 49, 50, 51, 53, 54, 55, 57, 58, 59, 61, 62, 63, 65, 66, 67, 69, 70, 71, 73, 74, 75, 77, 78, 79, 81, 82, 83, 85, 86, 87, 89, 90, 91, 93, 94, 95, 97, 98, 99, 101, 102, 103, 104, 105, 106, 107, 109, 109, 114, 115, 116, 117, 122, 123, 124, 125, 130, 131, 132, 133, 138, 139, 140, 141, 141, 142, 143, 143, 144, 146, 147, 148, 149, 154, 155, 156, 157, 159, 161, 163, 165, 167, 169, 169, 173, 173, 178, 179, 180, 183, 184, 186, 187, 188, 191, 192, 194, 195, 196, 198, 200, 201, 203, 205, 207, 209, 209, 214, 215, 216, 217, 220, 221, 222, 223, 224, 226, 227, 228, 229, 229, 231, 231, 233, 233, 238, 239, 240, 242, 243, 244, 245, 245, 247, 247, 249, 249, 254, 255, 256, 258, 259, 260, 261, 261, 263, 263, 265, 266, 267, 269, 271, 273, 274, 275, 277, 279, 281, 281, 282, 282, 283, 283, 285, 287, 289, 291, 293, 293, 297, 297 };
        posX = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        posY = new int[] { 3, 4, 5, 6, 2, 3, 4, 5, 5, 5, 5, 5, 2, 2, 2, 2, 2, 5, 5, 5, 5, 5, 2, 1, 4, 3, 4, 4, 4, 3, 3, 3, 4, 4, 4, 3, 3, 3, 4, 4, 4, 3, 3, 3, 4, 4, 4, 3, 3, 3, 2, 3, 4, 4, 5, 6, 6, 5, 4, 4, 3, 2, 2, 1, 0, 0, 1, 2, 3, 3, 3, 4, 0, 5, 2, 6, 3, 5, 1, 5, 2, 6, 0, 4, 1, 5, 4, 3, 4, 2, 5, 3, 2, 5, 2, 2, 5, 3, 6, 1, 4, 2, 5, 4, 1, 3, 2, 4, 5, 2, 4, 4, 3, 4, 4, 3, 4, 6, 2, 2, 1, 2, 2, 1, 2, 4, 3, 3, 4, 2, 2, 3, 4, 2, 1, 4, 5, 6, 2, 4, 5, 1, 3, 0, 4, 1, 2, 3, 4, 5, 2, 1, 3, 4, 1, 2, 4, 1, 3, 6, 2, 4, 1, 5, 2, 4, 6, 2, 1, 5, 4, 4, 3, 2, 1, 0, 5, 4, 3, 2, 1, 5, 2, 6, 3, 3, 0, 1, 3, 5, 2, 4, 6, 3, 4, 1, 4 };
        length = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        demi = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        //length = new int[] { 4, 3, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3 };

    }

    public void setRainy()
    {
        AudioClip clip = Resources.Load<AudioClip>("Sound/RainyDay");
        musicLength = clip.length;
        audioSource.clip = clip;
        musicTitle = "Rainy Day";
        bpm = 160;
        songBeat = 4;
        stdBeat = 4;
        offset = (float)-0.3;
        customOffset = (float)0.0;
        totalNote = 23;
        beat = new int[]   { 005, 009, 011, 013, 016, 017, 017, 019, 020, 021, 024, 024, 025, 027, 027, 028, 029, 029, 030, 030, 032, 033, 035};
        posX = new int[]   { 000, 000, 000, 000, 002, 000, 002, 000, 000, 000, 000, 002, 000, 000, 002, 000, 000, 000, 002, 002, 000, 000, 000};
        posY = new int[]   { 003, 005, 003, 001, 003, 005, 006, 006, 004, 002, 004, 005, 004, 004, 005, 002, 004, 006, 003, 002, 004, 004, 006};
        length = new int[] { 003, 001, 001, 003, 000, 000, 001, 000, 000, 003, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 001, 001};
        demi = new int[]   { 000, 000, 000, 000, 000, 000, 002, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000 };
        //length = new int[] { 4, 3, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3 };

    }

    public void setSunset()
    {
        AudioClip clip = Resources.Load<AudioClip>("Sound/SunsetRoad");
        musicLength = clip.length;
        audioSource.clip = clip;
        musicTitle = "Sunset Road";
        bpm = 200;
        songBeat = 4;
        stdBeat = 4;
        offset = (float)-0.8;
        customOffset = (float)0.0;
        totalNote = 23;
        beat = new int[] { 005, 009, 011, 013, 016, 017, 017, 019, 020, 021, 024, 024, 025, 027, 027, 028, 029, 029, 030, 030, 032, 033, 035 };
        posX = new int[] { 000, 000, 000, 000, 002, 000, 002, 000, 000, 000, 000, 002, 000, 000, 002, 000, 000, 000, 002, 002, 000, 000, 000 };
        posY = new int[] { 001, 002, 003, 004, 005, 006, 007, 008, 009, 008, 004, 005, 004, 004, 005, 002, 004, 006, 003, 002, 004, 004, 006 };
        length = new int[] { 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000 };
        demi = new int[] { 000, 000, 000, 000, 000, 000, 002, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000, 000 };

    }
}