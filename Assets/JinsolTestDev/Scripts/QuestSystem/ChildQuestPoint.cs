using UnityEngine;
using UnityEngine.EventSystems;

    /// <summary>
    /// 
    /// 메인 퀘스트 포인트는 아니기에 다른 기능은 빼고,
    /// 플레이어가 근처에 있을 때만 퀘스트 아이콘을 클릭할 수 있게끔 해주는 스크립트
    /// 
    /// - 정진솔
    /// </summary>

namespace Jinsol
{
    public class ChildQuestPoint : MonoBehaviour
    {
        private Transform player;
        private bool playerNearby = false;
        [SerializeField] private EventTrigger myEventTrigger; // 플레이어가 근처에 없으면 클릭할 수 없게

        private void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            myEventTrigger = GetComponentInChildren<EventTrigger>();
        }

        private void Update()
        {
            if (playerNearby)
            {
                myEventTrigger.enabled = true;
            }
            else
                myEventTrigger.enabled= false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
                playerNearby = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Player")
                playerNearby = false;
        }
    }
}
