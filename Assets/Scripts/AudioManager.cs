using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioClip correctSound;
    public AudioClip wrongSound;
    public AudioClip congratSound;
    public AudioClip alarmSound; // Alarm sesi i�in AudioClip
    public AudioClip backgroundMusic; // Arka plan m�zi�i i�in AudioClip

    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void Start()
    {
        PlayBackgroundMusic();
    }

    public void PlayCorrectSound()
    {
        audioSource.PlayOneShot(correctSound);
    }

    public void PlayWrongSound()
    {
        audioSource.PlayOneShot(wrongSound);
    }

    public void PlayCongratSound()
    {
        audioSource.PlayOneShot(congratSound);
    }

    public void PlayAlarmSound() // Alarm sesini �alma fonksiyonu
    {
        audioSource.PlayOneShot(alarmSound);
    }

    public void StopAlarmSound() // Alarm sesini durdurma fonksiyonu
    {
        audioSource.Stop();
    }

    public void PlayBackgroundMusic() // Arka plan m�zi�ini �alma fonksiyonu
    {
        audioSource.clip = backgroundMusic;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void StopBackgroundMusic() // Arka plan m�zi�ini durdurma fonksiyonu
    {
        if (audioSource.isPlaying && audioSource.clip == backgroundMusic)
        {
            audioSource.Stop();
        }
    }
}
