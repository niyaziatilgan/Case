using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerActivator : MonoBehaviour
{
    public GameObject leftController;
    public GameObject rightController;

    void Start()
    {
        // Baþlangýçta kontrolcüleri inaktif yap
        leftController.SetActive(false);
        rightController.SetActive(false);

        // Bir süre sonra kontrolcüleri aktif yap
        Invoke("ActivateControllers", 0.1f);
    }

    void ActivateControllers()
    {
        leftController.SetActive(true);
        rightController.SetActive(true);
    }
}
