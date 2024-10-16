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
    public GameObject specialPaper; // Özel kâðýt objesi
    public GameObject key;          // Anahtar objesi
    public GameObject keySocket;    // Anahtarýn soketi
    public SocketGrabPair[] socketGrabPairs; // Nesne ve soket eþleþmeleri
    public AudioClip correctSound;  // Doðru ses
    public AudioClip wrongSound;    // Yanlýþ ses
    public AudioClip alarmSound;    // Alarm sesi
    public AudioSource audioSource; // Ses kaynaðý

    public float timeLimit = 60f;    // Kullanýcýdan alýnan süre sýnýrý
    private int correctCount = 0;    // Doðru yerleþtirilen nesne sayýsý
    private float timer;              // Zamanlayýcý
    private bool isAlarmActive = false; // Alarm durumu

    private void Start()
    {
        timer = timeLimit; // Baþlangýçta zamanlayýcýyý süre sýnýrýna ayarla

        // Baþlangýçta anahtar ve özel kâðýdý inaktif yap
        specialPaper.SetActive(false);
        key.SetActive(false);
        keySocket.SetActive(false);

        // Baþlangýçta tüm soketleri inaktif yap
        foreach (SocketGrabPair pair in socketGrabPairs)
        {
            pair.socketObject.socketActive = false; // Soketleri inaktif yap
            pair.grabbableObject.enabled = true; // Grabbable nesneleri aktif yap
        }
    }

    private void Update()
    {
        // Zamanlayýcýyý azalt
        timer -= Time.deltaTime;

        // Zamanlayýcý sýfýra ulaþtý mý?
        if (timer <= 0 && !isAlarmActive)
        {
            StartAlarm();
        }
    }

    private void StartAlarm()
    {
        isAlarmActive = true;
        audioSource.PlayOneShot(alarmSound);

        // Alarmýn 5 saniye boyunca çalmasýný saðla
        Invoke(nameof(StopAlarm), 5f);
    }

    private void StopAlarm()
    {
        isAlarmActive = false;
        audioSource.Stop(); // Alarm sesini durdur
    }

    public void OnPaperGrabbed()
    {
        // Kâðýt alýndýðýnda soketleri aktif hale getir
        foreach (SocketGrabPair pair in socketGrabPairs)
        {
            pair.socketObject.socketActive = true; // Soketleri aktif yap
        }
    }

    public void OnObjectPlaced(XRBaseInteractable placedObject, XRSocketInteractor targetSocket)
    {
        // Doðru eþleþmeyi bul
        foreach (SocketGrabPair pair in socketGrabPairs)
        {
            if (pair.grabbableObject == placedObject && pair.socketObject == targetSocket)
            {
                // Nesne doðru sokete yerleþtirildi
                DisableGrab(placedObject);
                audioSource.PlayOneShot(correctSound);
                correctCount++;

                // Eðer tüm objeler doðru soketlere yerleþtirildiyse anahtar ve kâðýdý aktif et
                if (correctCount >= socketGrabPairs.Length)
                {
                    specialPaper.SetActive(true);
                    key.SetActive(true);
                    keySocket.SetActive(true);
                }

                return; // Eþleþme bulundu, iþlemi bitir
            }
        }

        // Yanlýþ sokete yerleþtirildi
        audioSource.PlayOneShot(wrongSound);
    }

    private void DisableGrab(XRBaseInteractable obj)
    {
        XRGrabInteractable grabInteractable = obj.GetComponent<XRGrabInteractable>();
        if (grabInteractable != null)
        {
            grabInteractable.enabled = false; // Grab etkileþimini pasif hale getir
        }
    }

    public void OnKeyPlaced()
    {
        // Anahtar soketine yerleþtirildiðinde kapýyý aç
        // Buraya kapýyý açma mekanizmasý eklenebilir
        Debug.Log("Kapý açýldý!");
    }
}
