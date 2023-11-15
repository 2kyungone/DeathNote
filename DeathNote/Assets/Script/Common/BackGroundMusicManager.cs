// Unity ������ UI, �� ���� ���� ����� ����ϱ� ���� ���ӽ����̽�.
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BackGroundMusicManager : MonoBehaviour
{
    // �� Ŭ������ ������ �ν��Ͻ��� �����ϴ� ���� ����.
    public static BackGroundMusicManager instance;

    // ������ ����� ������� Ŭ���� �����ϴ� �迭.
    public AudioClip[] musicClips;
    // ������� ����ϴ� ������Ʈ�� ���� ����.
    private AudioSource audioSource;
    // ����ڰ� ������ �� �ִ� ���� �����̴� UI�� ���� ����.
    public Slider volumeSlider;
    // ���� ���Ұ� ���������� �����ϴ� ����.
    private bool isMuted;
    // ���Ұ� �Ǳ� ������ ���� ���� �����ϴ� ����.
    private float previousVolume;
    // ���� ��ư�� �̹��� ������Ʈ�� ���� ����.
    public Image volumeButtonImage;
    // ������ ���� ���� �� ǥ�õ� �̹���.
    public Sprite volumeSprite;
    // ���Ұ� ������ �� ǥ�õ� �̹���.
    public Sprite muteSprite;

    void Awake()
    {
        // �ν��Ͻ��� ���� �Ҵ���� �ʾҴٸ� �� ������Ʈ�� �̱������� �����մϴ�.
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // �� ��ȯ�ÿ��� ������Ʈ�� �ı����� �ʵ��� �մϴ�.
            audioSource = GetComponent<AudioSource>(); // ����� �ҽ� ������Ʈ�� �����ɴϴ�.
        }
        else if (instance != this) // �̹� �ν��Ͻ��� �����Ѵٸ� �ߺ��� �����ϱ� ���� �ڱ� �ڽ��� �ı��մϴ�.
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // ���� ���� �� ù ���� ������ ����մϴ�.
        PlayMusic(SceneManager.GetActiveScene().buildIndex);

        // �� ��ȯ �̺�Ʈ�� ���� ������ �����մϴ�.
        SceneManager.sceneLoaded += OnSceneLoaded;

        // ����� ���� ���� �ִٸ� �� ���� ����� ����� ������ �����մϴ�.
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            float savedVolume = PlayerPrefs.GetFloat("MusicVolume");
            audioSource.volume = savedVolume;
            volumeSlider.value = savedVolume; // �����̴��� UI�� ������Ʈ�մϴ�.
        }
    }

    // �� ���� �ε�� �� ȣ��Ǵ� �޼ҵ��Դϴ�.
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateVolumeSlider(); // ���� �����̴��� ������Ʈ�մϴ�.
        PlayMusic(scene.buildIndex); // �� ���� �´� ������ ����մϴ�.

        // ���Ұ� ��ư�� ã�Ƽ� �̹����� �����ʸ� ������Ʈ�մϴ�.
        GameObject muteButtonObj = GameObject.FindGameObjectWithTag("MuteButton");
        if (muteButtonObj != null)
        {
            volumeButtonImage = muteButtonObj.GetComponent<Image>();
            UpdateMuteButtonImage(); // ���Ұ� ��ư �̹����� ������Ʈ�մϴ�.

            // Button ������Ʈ�� �����ͼ� ���Ұ� ��� �޼ҵ带 �����ʷ� �����մϴ�.
            Button muteButton = muteButtonObj.GetComponent<Button>();
            if (muteButton != null)
            {
                muteButton.onClick.RemoveAllListeners(); // ���� �����ʸ� ��� �����մϴ�.
                muteButton.onClick.AddListener(ToggleMute); // ���Ұ� ��� �޼ҵ带 �����ʷ� �߰��մϴ�.
            }
        }
        else
        {
            Debug.LogError("Mute button with 'MuteButton' tag not found in the scene.");
        }
    }

    void OnDestroy()
    {
        // �� �̻� �ʿ����� �����Ƿ� �̺�Ʈ ������ ����մϴ�.
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void PlayMusic(int sceneIndex)
    {
        Debug.Log("�뷡 �������ִ� �Լ�");
        if (musicClips[sceneIndex] != null && audioSource.clip != musicClips[sceneIndex])
        {
            audioSource.clip = musicClips[sceneIndex];
            audioSource.Play();
        }
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }

    public void SetMusicVolume(float volume)
    {
        Debug.Log("���� �������ִ� �Լ�");

        if (audioSource != null)
        {
            audioSource.volume = volume;
            PlayerPrefs.SetFloat("MusicVolume", volume);
            PlayerPrefs.Save();

            // ������ 0�̸� ���Ұŷ� �����ϰ� �̹����� ���Ұ� �̹����� �����մϴ�.
            if (volume == 0)
            {
                if (!isMuted)
                {
                    // ������ 0�� �Ǿ� ���Ұ� ���°� �Ǿ����Ƿ� ���� ������ �����մϴ�.
                    previousVolume = volume;
                    isMuted = true;
                }
                volumeButtonImage.sprite = muteSprite; // ���Ұ� �̹����� ����
            }
            else
            {
                // ������ 0���� ũ�� ���Ұ� ���¸� �����ϰ� �̹����� ���� �̹����� �����մϴ�.
                if (isMuted)
                {
                    // ������ ���Ұ� ���¿��ٸ� ���ҰŸ� �����մϴ�.
                    isMuted = false;
                }
                volumeButtonImage.sprite = volumeSprite; // ���� �̹����� ����
            }
        }
        else
        {
            Debug.LogError("audioSource is null");
        }
    }

    private void UpdateVolumeSlider()
    {
        volumeSlider = GameObject.FindGameObjectWithTag("BackgroundMusicSlider")?.GetComponent<Slider>();
        if (volumeSlider != null)
        {
            if (PlayerPrefs.HasKey("MusicVolume"))
            {
                float savedVolume = PlayerPrefs.GetFloat("MusicVolume");
                volumeSlider.value = savedVolume;
            }
            volumeSlider.onValueChanged.RemoveAllListeners(); // ���� ������ ����
            volumeSlider.onValueChanged.AddListener(SetMusicVolume); // �� ������ �߰�
        }
        else
        {
            Debug.LogError("Volume slider not found");
        }
    }

    // ���Ұ� ��� �޼ҵ�
    public void ToggleMute()
    {
        if (isMuted)
        {
            // ���Ұ� ����
            audioSource.volume = previousVolume;
            isMuted = false;
            volumeButtonImage.sprite = volumeSprite; // ���� �̹����� ����
        }
        else
        {
            // ���Ұ� ����
            previousVolume = audioSource.volume;
            audioSource.volume = 0;
            isMuted = true;
            volumeButtonImage.sprite = muteSprite; // ���Ұ� �̹����� ����
        }
    }

    private void UpdateMuteButtonImage()
    {
        if (volumeButtonImage != null)
        {
            volumeButtonImage.sprite = isMuted ? muteSprite : volumeSprite;
        }
    }
}