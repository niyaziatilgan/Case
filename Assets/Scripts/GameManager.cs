using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GrabManager grabManager;
    public GameObject specialPaper;
    public GameObject key;
    public GameObject keySocket;

    private void Start()
    {
        // Ba�lang��ta anahtar ve �zel ka��t ile anahtar soketi inaktif
        specialPaper.SetActive(false);
        key.SetActive(false);
        keySocket.SetActive(false);
    }

    public void OnAllObjectsCorrectlyPlaced()
    {
        // T�m objeler do�ru yerle�tirildi�inde anahtar, �zel ka��t ve anahtar soketini aktif yap
        specialPaper.SetActive(true);
        key.SetActive(true);
        keySocket.SetActive(true);
    }

    public void OnKeyInserted(GameObject grabbedObject, GameObject socketObject)
    {
        if (grabbedObject.tag == "Key" && socketObject.tag == "keySocket")
        {
            AudioManager.Instance.PlayCongratSound();
        }
    }
}
