using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;
using System.Collections;
using System;

public class SocketManager : MonoBehaviour
{
    public List<SocketObject> socketObjects; // Tag e�le�meleri i�in socket ve grab objelerinin listesi
    public GrabManager grabManager;

    public GameObject specialPaper; // �zel ka��t
    public GameObject key; // Anahtar
    public GameObject keySocket; // Anahtar soketi

    public int timeLimit; // Zaman limiti (saniye)
    private float countdown; // Geri say�m de�i�keni

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
                    socketObject.correct = true;
                    if (CheckAllSocketObjects())
                    {
                        // T�m objeler do�ru yerle�tirildi�inde fonksiyonu �a��r
                        OnAllObjectsCorrectlyPlaced();
                        DeactivateSockets();
                    }
                }
            });
        }

        // Timer'� ba�lat
        countdown = timeLimit;
        StartCoroutine(StartTimer());
    }

    private IEnumerator StartTimer()
    {
        while (countdown > 0)
        {
            yield return new WaitForSeconds(1);
            countdown--;

            // E�er s�re 6 dakika (360 saniye) dolduysa alarm �al
            if (countdown == 0)
            {
                if (timeLimit == 360) // 6 dk
                {
                    StartCoroutine(PlayAlarmSound());
                }
            }
        }
    }

    private IEnumerator PlayAlarmSound()
    {
        AudioManager.Instance.PlayAlarmSound(); // Alarm sesini �al
        yield return new WaitForSeconds(5f); // 5 saniye bekle
        AudioManager.Instance.StopAlarmSound(); // Alarm sesini durdur
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

    public void CheckSocketInteraction(GameObject grabbedObject, GameObject socketObject)
    {
        foreach (SocketObject socketObj in socketObjects)
        {
            if (socketObj.socket == socketObject)
            {
                if (socketObj.grabbable.tag == grabbedObject.tag)
                {
                    // Do�ru yerle�tirme
                    grabManager.SetGrabEnabled(grabbedObject, false);
                    AudioManager.Instance.PlayCorrectSound();
                }
                else
                {
                    // Yanl�� yerle�tirme
                    AudioManager.Instance.PlayWrongSound();
                }
                break;
            }
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
    [NonSerialized] public bool correct = false; // Do�ru yerle�tirildi mi
}
