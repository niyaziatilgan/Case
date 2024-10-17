using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GameManager : MonoBehaviour
{
    public GrabManager grabManager;
    public GameObject specialPaper;
    public GameObject key;
    public GameObject keySocket;
    public Animator animator;

    public XRSocketInteractor socketInteractor;
    public XRGrabInteractable interactable;

    private void Start()
    {
        // Ba�lang��ta anahtar ve �zel ka��t ile anahtar soketi inaktif
        specialPaper.SetActive(false);
        key.SetActive(false);
        keySocket.SetActive(false);

        socketInteractor.selectEntered.AddListener((value) =>
        {
            if (value.interactableObject == interactable as IXRSelectInteractable)
            {
                OnKeyInserted();
            }
        });
    }

    public void OnAllObjectsCorrectlyPlaced()
    {
        // T�m objeler do�ru yerle�tirildi�inde anahtar, �zel ka��t ve anahtar soketini aktif yap
        specialPaper.SetActive(true);
        key.SetActive(true);
        keySocket.SetActive(true);
    }

    public void OnKeyInserted()
    {
        AudioManager.Instance.PlayCongratSound();
        animator.SetBool("openDoor", true);
        key.SetActive(false);

        Invoke("QuitGame", 6f);
    }

    private void QuitGame()
    {
        Debug.Log("oyundan c�kt�");
        Application.Quit();
    }
}
