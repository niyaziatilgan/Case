using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GameManager : MonoBehaviour
{
    public SocketInteraction socketInteraction;
    public GameObject paper; // K���t objesi

    private void Start()
    {
        // K���t objesi i�in XRGrabInteractable bile�eni ekle
        XRGrabInteractable grabInteractable = paper.AddComponent<XRGrabInteractable>();

        // K���t tutuldu�unda OnPaperGrabbed fonksiyonunu �a��r
        grabInteractable.onSelectEntered.AddListener((interactor) => socketInteraction.OnPaperGrabbed());
    }
}
