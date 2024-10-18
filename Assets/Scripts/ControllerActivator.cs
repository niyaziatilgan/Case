using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerActivator : MonoBehaviour
{
    public GameObject leftController;
    public GameObject rightController;

    void Start()
    {
        // Ba�lang��ta kontrolc�leri inaktif yap
        leftController.SetActive(false);
        rightController.SetActive(false);

        // Bir s�re sonra kontrolc�leri aktif yap
        Invoke("ActivateControllers", 0.1f);
    }

    void ActivateControllers()
    {
        leftController.SetActive(true);
        rightController.SetActive(true);
    }
}
