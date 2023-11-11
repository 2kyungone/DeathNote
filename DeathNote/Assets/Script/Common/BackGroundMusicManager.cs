using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // SceneManager�� ����ϱ� ���� �߰�

public class BackGroundMusicManager : MonoBehaviour
{
    public static BackGroundMusicManager instance; // �̱��� �ν��Ͻ�

    public AudioClip[] musicClips; // ���� ���� ���� Ŭ���� ���⿡ �Ҵ�
    private AudioSource audioSource;
    public Slider volumeSlider; // ���� ���� �����̴� ����


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }


    void Start()
    {
        // ���� Ȱ��ȭ�� ���� �ε����� ���� ������ ����մϴ�.
        PlayMusic(SceneManager.GetActiveScene().buildIndex);

        // �� ��ȯ �̺�Ʈ�� �޼ҵ带 ������ŵ�ϴ�.
        SceneManager.sceneLoaded += OnSceneLoaded;

        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            // PlayerPrefs���� ���� ���� �ҷ�����
            float savedVolume = PlayerPrefs.GetFloat("MusicVolume");
            audioSource.volume = savedVolume;
            volumeSlider.value = savedVolume;
        }
        else
        {
            // �⺻ ���� �� ����
            audioSource.volume = 1f; // �Ǵ� ���ϴ� �⺻ ���� ��
            volumeSlider.value = 1f;
        }

        volumeSlider.onValueChanged.AddListener(SetVolume); // �����̴� �� ���� �̺�Ʈ�� �޼ҵ� ����
    }

    // ���� �ε�� �� ȣ��� �޼ҵ��Դϴ�.
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayMusic(scene.buildIndex); // �� �ε����� ���� ������ ����մϴ�.

        // ������� �����̴� ã��
        GameObject backgroundMusicSliderObj = GameObject.FindGameObjectWithTag("BackgroundMusicSlider");
        if (backgroundMusicSliderObj != null)
        {
            Slider backgroundMusicSlider = backgroundMusicSliderObj.GetComponent<Slider>();
            // ���Ŀ� ������� �����̴��� ���� ���� ����
        }

        // ȿ���� �����̴� ã��
        GameObject soundEffectsSliderObj = GameObject.FindGameObjectWithTag("SoundEffectsSlider");
        if (soundEffectsSliderObj != null)
        {
            Slider soundEffectsSlider = soundEffectsSliderObj.GetComponent<Slider>();
            // ���Ŀ� ȿ���� �����̴��� ���� ���� ����
        }
    }


    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
        PlayerPrefs.SetFloat("MusicVolume", volume);
        PlayerPrefs.Save();
    }


    public void PlayMusic(int sceneIndex)
    {
        if (audioSource != null && musicClips[sceneIndex] != null && audioSource.clip != musicClips[sceneIndex])
        {
            audioSource.clip = musicClips[sceneIndex];
            audioSource.Play();
        }
    }


    public void StopMusic()
    {
        audioSource.Stop();
    }
}