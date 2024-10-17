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
        arrow.SetActive(true);
        paper.SetActive(true);

        //socketManager.DeactivateSockets();


        paperGrabInteractable = paper.GetComponent<XRGrabInteractable>();
        paperGrabInteractable.selectEntered.AddListener(OnPaperGrabbed);
        //paperGrabInteractable.selectExited.AddListener(OnPaperReleased); 

       
    }

    private void OnPaperGrabbed(SelectEnterEventArgs args)
    {
        Debug.Log("grabbed");
        arrow.SetActive(false);
        socketManager.ActivateSockets();
    }

    /*
    private void OnPaperReleased(SelectExitEventArgs args)
    {
        // Kaðýt býrakýldýðýnda oku aktif yap ve soketleri inaktif hale getir
        arrow.SetActive(true);
        socketManager.DeactivateSockets();
    }
    */
    public void SetGrabEnabled(GameObject obj, bool isEnabled)
    {
        XRGrabInteractable grabInteractable = obj.GetComponent<XRGrabInteractable>();
        if (grabInteractable != null)
        {
            grabInteractable.enabled = isEnabled;
        }
    }
}
