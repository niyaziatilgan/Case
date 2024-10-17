using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabManager : MonoBehaviour
{
    public GameObject paper;
    public GameObject arrow;
    public SocketManager socketManager;

    private XRGrabInteractable paperGrabInteractable;

    void Start()
    {
        paperGrabInteractable = paper.GetComponent<XRGrabInteractable>();
        paperGrabInteractable.selectEntered.AddListener(OnPaperGrabbed);
        paperGrabInteractable.selectExited.AddListener(OnPaperReleased); // Paper b�rak�ld���nda �a�r�lacak fonksiyon

        // Ba�lang��ta ok aktif, ka��t aktif ve tutulabilir
        arrow.SetActive(true);
        paper.SetActive(true);

        // Ba�lang��ta di�er t�m soketleri inaktif yap
        socketManager.DeactivateSockets();
    }

    private void OnPaperGrabbed(SelectEnterEventArgs args)
    {
        // Ka��t tutuldu�unda oku inaktif yap ve soketleri aktif hale getir
        arrow.SetActive(false);
        socketManager.ActivateSockets();
    }

    private void OnPaperReleased(SelectExitEventArgs args)
    {
        // Ka��t b�rak�ld���nda oku aktif yap ve soketleri inaktif hale getir
        arrow.SetActive(true);
        socketManager.DeactivateSockets();
    }

    public void SetGrabEnabled(GameObject obj, bool isEnabled)
    {
        XRGrabInteractable grabInteractable = obj.GetComponent<XRGrabInteractable>();
        if (grabInteractable != null)
        {
            grabInteractable.enabled = isEnabled;
        }
    }
}
