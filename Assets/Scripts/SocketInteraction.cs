using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketInteraction : MonoBehaviour
{
    public GameObject arrow; //ok
    public GameObject specialPaper; // �zel k���t objesi
    public GameObject key;          // Anahtar objesi
    public GameObject keySocket;    // Anahtar�n soketi
    public GameObject[] sockets;     // Soketler (mum1, mum2, vazo, kitap1, kitap2, vb.)
    public AudioClip correctSound;   // Do�ru ses
    public AudioClip wrongSound;     // Yanl�� ses
    public AudioSource audioSource;  // Ses kayna��

    private int correctCount = 0;    // Do�ru yerle�tirilen nesne say�s�

    private void Start()
    {
        // Ba�lang��ta anahtar ve �zel k���d� inaktif yap
        specialPaper.SetActive(false);
        key.SetActive(false);
        keySocket.SetActive(false);

        // Ba�lang��ta t�m soketleri inaktif yap
        foreach (GameObject socket in sockets)
        {
            socket.SetActive(false);
        }
    }

    public void OnPaperGrabbed()
    {
        // K���t al�nd���nda soketleri aktif hale getir
        foreach (GameObject socket in sockets)
        {
            arrow.SetActive(false);
            socket.SetActive(true);
        }
    }

    public void OnObjectPlaced(GameObject placedObject, GameObject targetSocket)
    {
        // Nesne do�ru sokete yerle�tirildi mi?
        if (placedObject.CompareTag(targetSocket.tag))
        {
            // Do�ru sokete yerle�tirildi
            DisableGrab(placedObject);
            audioSource.PlayOneShot(correctSound);
            correctCount++;

            // E�er t�m objeler do�ru soketlere yerle�tirildiyse anahtar ve k���d� aktif et
            if (correctCount >= sockets.Length)
            {
                specialPaper.SetActive(true);
                key.SetActive(true);
                keySocket.SetActive(true);
            }
        }
        else
        {
            // Yanl�� sokete yerle�tirildi
            audioSource.PlayOneShot(wrongSound);
        }
    }

    private void DisableGrab(GameObject obj)
    {
        XRGrabInteractable grabInteractable = obj.GetComponent<XRGrabInteractable>();
        if (grabInteractable != null)
        {
            grabInteractable.interactionLayerMask = LayerMask.GetMask("UI"); // Grab etkile�imini pasif hale getir
        }
    }

    public void OnKeyPlaced()
    {
        // Anahtar soketine yerle�tirildi�inde kap�y� a�
        // Buraya kap�y� a�ma mekanizmas� eklenebilir
        Debug.Log("Kap� a��ld�!");
    }
}
