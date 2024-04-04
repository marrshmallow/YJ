using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToUI_Movie : MonoBehaviour
{
    public bool isPlayerInTrigger;
    public bool isUIActive_Movie;
    public GameObject move_Object;
    // public Image objectUI;

    public GameObject info_Text;  //안내 패널



    // Update is called once per frame
    void Update()
    {
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.R))
        {

            isUIActive_Movie = !isUIActive_Movie;
            move_Object.SetActive(isUIActive_Movie);

            if (isUIActive_Movie)
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
