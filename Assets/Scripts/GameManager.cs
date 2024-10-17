using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GrabManager grabManager;
    public GameObject specialPaper;
    public GameObject key;
    public GameObject keySocket;

    private void Start()
    {
        // Baþlangýçta anahtar ve özel kaðýt ile anahtar soketi inaktif
        specialPaper.SetActive(false);
        key.SetActive(false);
        keySocket.SetActive(false);
    }

    public void OnAllObjectsCorrectlyPlaced()
    {
        // Tüm objeler doðru yerleþtirildiðinde anahtar, özel kaðýt ve anahtar soketini aktif yap
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
