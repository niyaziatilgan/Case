using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioClip correctSound;
    public AudioClip wrongSound;
    public AudioClip congratSound;
    public AudioClip alarmSound; // Alarm sesi için AudioClip
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

    public void PlayAlarmSound() // Alarm sesini çalma fonksiyonu
    {
        audioSource.PlayOneShot(alarmSound);
    }

    public void StopAlarmSound() // Alarm sesini durdurma fonksiyonu
    {
        audioSource.Stop();
    }
}
