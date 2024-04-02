using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ObjectToUI : MonoBehaviour
{
    public bool isPlayerInTrigger;
    public bool isUIActive;
    public GameObject uiObject;
    // public Image objectUI;


    // Update is called once per frame
    void Update()
    {
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.Space))
        {
            isUIActive = !isUIActive;
            uiObject.SetActive(isUIActive);
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isPlayerInTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isPlayerInTrigger = false;
        }
    }
}
