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

    public GameObject info_Text;  //안내 패널
   


    // Update is called once per frame
    void Update()
    {
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.Space))
        {
            
            isUIActive = !isUIActive;
            uiObject.SetActive(isUIActive);

            if (isUIActive)
            {
                info_Text.SetActive(false);
            }
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isPlayerInTrigger = true;
            info_Text.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isPlayerInTrigger = false;
            info_Text.SetActive(false);
        }
    }

}
