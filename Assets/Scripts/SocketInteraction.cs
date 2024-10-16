using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketInteraction : MonoBehaviour
{
    public GameObject arrow; //ok
    public GameObject specialPaper; // Özel kâðýt objesi
    public GameObject key;          // Anahtar objesi
    public GameObject keySocket;    // Anahtarýn soketi
    public GameObject[] sockets;     // Soketler (mum1, mum2, vazo, kitap1, kitap2, vb.)
    public AudioClip correctSound;   // Doðru ses
    public AudioClip wrongSound;     // Yanlýþ ses
    public AudioSource audioSource;  // Ses kaynaðý

    private int correctCount = 0;    // Doðru yerleþtirilen nesne sayýsý

    private void Start()
    {
        // Baþlangýçta anahtar ve özel kâðýdý inaktif yap
        specialPaper.SetActive(false);
        key.SetActive(false);
        keySocket.SetActive(false);

        // Baþlangýçta tüm soketleri inaktif yap
        foreach (GameObject socket in sockets)
        {
            socket.SetActive(false);
        }
    }

    public void OnPaperGrabbed()
    {
        // Kâðýt alýndýðýnda soketleri aktif hale getir
        foreach (GameObject socket in sockets)
        {
            arrow.SetActive(false);
            socket.SetActive(true);
        }
    }

    public void OnObjectPlaced(GameObject placedObject, GameObject targetSocket)
    {
        // Nesne doðru sokete yerleþtirildi mi?
        if (placedObject.CompareTag(targetSocket.tag))
        {
            // Doðru sokete yerleþtirildi
            DisableGrab(placedObject);
            audioSource.PlayOneShot(correctSound);
            correctCount++;

            // Eðer tüm objeler doðru soketlere yerleþtirildiyse anahtar ve kâðýdý aktif et
            if (correctCount >= sockets.Length)
            {
                specialPaper.SetActive(true);
                key.SetActive(true);
                keySocket.SetActive(true);
            }
        }
        else
        {
            // Yanlýþ sokete yerleþtirildi
            audioSource.PlayOneShot(wrongSound);
        }
    }

    private void DisableGrab(GameObject obj)
    {
        XRGrabInteractable grabInteractable = obj.GetComponent<XRGrabInteractable>();
        if (grabInteractable != null)
        {
            grabInteractable.interactionLayerMask = LayerMask.GetMask("UI"); // Grab etkileþimini pasif hale getir
        }
    }

    public void OnKeyPlaced()
    {
        // Anahtar soketine yerleþtirildiðinde kapýyý aç
        // Buraya kapýyý açma mekanizmasý eklenebilir
        Debug.Log("Kapý açýldý!");
    }
}
