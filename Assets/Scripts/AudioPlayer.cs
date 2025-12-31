using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] AudioClip paddleSound;
    [Range(0f, 1f)] public float paddleVolume = 1f;

    [SerializeField] AudioClip correctSound;
    [Range(0f, 1f)] public float correctVolume = 1f;

    [SerializeField] AudioClip wrongSound;
    [Range(0f, 1f)] public float wrongVolume = 1f;

    [SerializeField] AudioClip clickSound;
    [Range(0f, 1f)] public float clickVolume = 1f;
    
    [SerializeField] AudioClip bgMusic;

    static AudioPlayer instance;

    AudioSource bgSource;
    int lastScene = -1;

    void Awake()
    {
        ManageSingleton();
        bgSource = gameObject.AddComponent<AudioSource>();
        bgSource.loop = true;
        bgSource.playOnAwake = false;
    }

    void Start()
    {
        PlayMusic(bgMusic);
    }
    private void ManageSingleton()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;

        if (currentScene != lastScene)
        {
            lastScene = currentScene;
        }
    }


    public void PlayMusic(AudioClip clip)
    {
        if (bgSource.clip == clip) return;

        bgSource.clip = clip;
        bgSource.volume = 1f;
        bgSource.Play();
    }

    public void PlayPaddleClip()
    {
        PlayClip(paddleSound, paddleVolume);
    }

    public void PlayCorrectClip()
    {
        PlayClip(correctSound, correctVolume);
    }

    public void PlayWrongClip()
    {
        PlayClip(wrongSound, correctVolume);
    }

    public void PlayClickClip()
    {
        PlayClip(clickSound, clickVolume);
    }

    public void PlayBgClip()
    {
        PlayMusic(bgMusic);
    }

    private void PlayClip(AudioClip audio, float volume)
    {
        if (audio == null) return;

        AudioSource.PlayClipAtPoint(audio, Camera.main.transform.position, volume);
    }
}
