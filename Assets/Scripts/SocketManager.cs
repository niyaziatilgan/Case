using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;
using System.Collections;
using System;

public class SocketManager : MonoBehaviour
{
    public List<SocketObject> socketObjects; 
    public GrabManager grabManager;

    public GameObject specialPaper; 
    public GameObject key;
    public GameObject keySocket;
    public GameObject arrow2;

    private XRGrabInteractable SpeacialPaperGrabInteractable;


    void Start()
    {
        // Baþlangýçta tüm soketleri inaktif yap
        DeactivateSockets();
        arrow2.SetActive(false);

        foreach (SocketObject socketObject in socketObjects)
        {
            socketObject.socket.selectEntered.AddListener((value) =>
            {
                if (value.interactableObject == socketObject.grabbable as IXRSelectInteractable)
                {
                    AudioManager.Instance.PlayCorrectSound();
                    socketObject.correct = true;

                    //Debug.LogWarning(socketObject.grabbable.GetComponent<Rigidbody>().isKinematic);

                    socketObject.grabbable.GetComponent<XRGrabInteractable>().enabled = false;
                    socketObject.grabbable.GetComponent<Rigidbody>().isKinematic = true;
                    socketObject.socket.gameObject.SetActive(false);
                    //Debug.LogWarning(socketObject.grabbable.GetComponent<Rigidbody>().isKinematic);
                    socketObject.grabbable.transform.position = socketObject.objectPosition.position;
                    socketObject.grabbable.transform.rotation = socketObject.objectPosition.rotation;
                    //if (CheckAllSocketObjects())
                    //{
                    //    OnAllObjectsCorrectlyPlaced();
                    //    DeactivateSockets();
                    //}
                }
                else
                {
                    AudioManager.Instance.PlayWrongSound();
                }

                if (CheckAllSocketObjects())
                {
                    // Tüm objeler doðru yerleþtirildiðinde fonksiyonu çaðýr
                    OnAllObjectsCorrectlyPlaced();
                    DeactivateSockets();
                }
            });
        }
        SpeacialPaperGrabInteractable = specialPaper.GetComponent<XRGrabInteractable>();
        SpeacialPaperGrabInteractable.selectEntered.AddListener(OnSpecialPaperGrabbed);
    }

    private void OnSpecialPaperGrabbed(SelectEnterEventArgs args)
    {
        Debug.Log("second arrow is gone");
        arrow2.SetActive(false);
    }

    private IEnumerator PlayAlarmSound()
    {
        AudioManager.Instance.PlayAlarmSound(); 
        yield return new WaitForSeconds(5f); 
        AudioManager.Instance.StopAlarmSound(); 
    }

    public bool CheckAllSocketObjects()
    {
        foreach (SocketObject socketObject in socketObjects)
        {
            if (socketObject.correct == false) return false;
        }
        return true;
    }

    public void ActivateSockets()
    {
        foreach (SocketObject socketObj in socketObjects)
        {
            Debug.Log("activated");
            socketObj.socket.gameObject.SetActive(true);
        }
    }

    public void DeactivateSockets()
    {
        foreach (SocketObject socketObj in socketObjects)
        {
            socketObj.socket.gameObject.SetActive(false);
        }
    }

    // Tüm objeler doðru yerleþtirildiðinde anahtar, özel kaðýt ve anahtar soketini aktif yap
    public void OnAllObjectsCorrectlyPlaced()
    {
        
        specialPaper.SetActive(true);
        key.SetActive(true);
        keySocket.SetActive(true);
        arrow2.SetActive(true);

    }
}

[System.Serializable]
public class SocketObject
{
    public XRSocketInteractor socket;
    public XRGrabInteractable grabbable;
    public Transform objectPosition;
    [NonSerialized] public bool correct = false; // Doðru yerleþtirildi mi
}
