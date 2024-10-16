using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[System.Serializable]
public class SocketGrabPair
{
    public XRGrabInteractable grabbableObject; // Tutulabilir nesne
    public XRSocketInteractor socketObject;    // Soket
}

public class SocketInteraction : MonoBehaviour
{
    public GameObject specialPaper; // �zel k���t objesi
    public GameObject key;          // Anahtar objesi
    public GameObject keySocket;    // Anahtar�n soketi
    public SocketGrabPair[] socketGrabPairs; // Nesne ve soket e�le�meleri
    public AudioClip correctSound;  // Do�ru ses
    public AudioClip wrongSound;    // Yanl�� ses
    public AudioClip alarmSound;    // Alarm sesi
    public AudioSource audioSource; // Ses kayna��

    public float timeLimit = 60f;    // Kullan�c�dan al�nan s�re s�n�r�
    private int correctCount = 0;    // Do�ru yerle�tirilen nesne say�s�
    private float timer;              // Zamanlay�c�
    private bool isAlarmActive = false; // Alarm durumu

    private void Start()
    {
        timer = timeLimit; // Ba�lang��ta zamanlay�c�y� s�re s�n�r�na ayarla

        // Ba�lang��ta anahtar ve �zel k���d� inaktif yap
        specialPaper.SetActive(false);
        key.SetActive(false);
        keySocket.SetActive(false);

        // Ba�lang��ta t�m soketleri inaktif yap
        foreach (SocketGrabPair pair in socketGrabPairs)
        {
            pair.socketObject.socketActive = false; // Soketleri inaktif yap
            pair.grabbableObject.enabled = true; // Grabbable nesneleri aktif yap
        }
    }

    private void Update()
    {
        // Zamanlay�c�y� azalt
        timer -= Time.deltaTime;

        // Zamanlay�c� s�f�ra ula�t� m�?
        if (timer <= 0 && !isAlarmActive)
        {
            StartAlarm();
        }
    }

    private void StartAlarm()
    {
        isAlarmActive = true;
        audioSource.PlayOneShot(alarmSound);

        // Alarm�n 5 saniye boyunca �almas�n� sa�la
        Invoke(nameof(StopAlarm), 5f);
    }

    private void StopAlarm()
    {
        isAlarmActive = false;
        audioSource.Stop(); // Alarm sesini durdur
    }

    public void OnPaperGrabbed()
    {
        // K���t al�nd���nda soketleri aktif hale getir
        foreach (SocketGrabPair pair in socketGrabPairs)
        {
            pair.socketObject.socketActive = true; // Soketleri aktif yap
        }
    }

    public void OnObjectPlaced(XRBaseInteractable placedObject, XRSocketInteractor targetSocket)
    {
        // Do�ru e�le�meyi bul
        foreach (SocketGrabPair pair in socketGrabPairs)
        {
            if (pair.grabbableObject == placedObject && pair.socketObject == targetSocket)
            {
                // Nesne do�ru sokete yerle�tirildi
                DisableGrab(placedObject);
                audioSource.PlayOneShot(correctSound);
                correctCount++;

                // E�er t�m objeler do�ru soketlere yerle�tirildiyse anahtar ve k���d� aktif et
                if (correctCount >= socketGrabPairs.Length)
                {
                    specialPaper.SetActive(true);
                    key.SetActive(true);
                    keySocket.SetActive(true);
                }

                return; // E�le�me bulundu, i�lemi bitir
            }
        }

        // Yanl�� sokete yerle�tirildi
        audioSource.PlayOneShot(wrongSound);
    }

    private void DisableGrab(XRBaseInteractable obj)
    {
        XRGrabInteractable grabInteractable = obj.GetComponent<XRGrabInteractable>();
        if (grabInteractable != null)
        {
            grabInteractable.enabled = false; // Grab etkile�imini pasif hale getir
        }
    }

    public void OnKeyPlaced()
    {
        // Anahtar soketine yerle�tirildi�inde kap�y� a�
        // Buraya kap�y� a�ma mekanizmas� eklenebilir
        Debug.Log("Kap� a��ld�!");
    }
}
