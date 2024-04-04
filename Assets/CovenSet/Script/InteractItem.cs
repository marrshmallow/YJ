using UnityEngine;
using UnityEngine.UI;

public class InteractItem : MonoBehaviour
{
    bool onPlayer = false; //플레이어가 범위에 있는지

    bool equal = false;

    public GameObject Item;

    public GameObject equalItem;

    public GameObject handVR_R;

    public GameObject equalhandVR_R;

    public GameObject handVR_L;

    public GameObject equalhandVR_L;

    public GameObject info_Text;  //안내 패널

    public bool isUIActive;
    void Update()
    {
        if (onPlayer && Input.GetKeyDown(KeyCode.F))
        {
           
                Debug.Log("상호작용");
                //Item.SetActive(!Item.activeSelf);
               //테이블위 오브젝트 비활성화
              
            //플레이어 눈에위치한 오브젝트 활성화
            if (!equal)
            {
                Item.SetActive(false);
                equalItem.SetActive(true);
                handVR_R.SetActive(false);
                equalhandVR_R.SetActive(true);
                handVR_L.SetActive(false);
                equalhandVR_L.SetActive(true);

                equal = true;
            }
            else
            {
                Item.SetActive(true);
                equalItem.SetActive(false);
                handVR_R.SetActive(true);
                equalhandVR_R.SetActive(false);
                handVR_L.SetActive(true);
                equalhandVR_L.SetActive(false);

                equal = false;
            }

            if (isUIActive)
            {
                info_Text.SetActive(false);
            }
        }
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onPlayer = true;
            info_Text.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onPlayer = false;
            info_Text.SetActive(false);
        }
    }
}
