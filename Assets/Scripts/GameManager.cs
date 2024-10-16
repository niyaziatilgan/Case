using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GameManager : MonoBehaviour
{
    public SocketInteraction socketInteraction;
    public GameObject paper; // Kâðýt objesi

    private void Start()
    {
        // Kâðýt objesi için XRGrabInteractable bileþeni ekle
        XRGrabInteractable grabInteractable = paper.AddComponent<XRGrabInteractable>();

        // Kâðýt tutulduðunda OnPaperGrabbed fonksiyonunu çaðýr
        grabInteractable.onSelectEntered.AddListener((interactor) => socketInteraction.OnPaperGrabbed());
    }
}
