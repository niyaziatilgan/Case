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


    void Start()
    {
        // Ba�lang��ta t�m soketleri inaktif yap
        DeactivateSockets();

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
                    //if (CheckAllSocketObjects())
                    //{
                    //    // T�m objeler do�ru yerle�tirildi�inde fonksiyonu �a��r
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
                    // T�m objeler do�ru yerle�tirildi�inde fonksiyonu �a��r
                    OnAllObjectsCorrectlyPlaced();                    
                    DeactivateSockets();
                }
            });
        }
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

    // T�m objeler do�ru yerle�tirildi�inde anahtar, �zel ka��t ve anahtar soketini aktif yap
    public void OnAllObjectsCorrectlyPlaced()
    {
        
        specialPaper.SetActive(true);

        key.SetActive(true);
        keySocket.SetActive(true);
    }
}

[System.Serializable]
public class SocketObject
{
    public XRSocketInteractor socket;
    public XRGrabInteractable grabbable;
    public Transform objectPosition;
    [NonSerialized] public bool correct = false; // Do�ru yerle�tirildi mi
}
