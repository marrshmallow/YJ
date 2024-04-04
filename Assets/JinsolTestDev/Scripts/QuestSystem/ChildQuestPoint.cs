using UnityEngine;
using UnityEngine.EventSystems;

    /// <summary>
    /// 
    /// ���� ����Ʈ ����Ʈ�� �ƴϱ⿡ �ٸ� ����� ����,
    /// �÷��̾ ��ó�� ���� ���� ����Ʈ �������� Ŭ���� �� �ְԲ� ���ִ� ��ũ��Ʈ
    /// 
    /// - ������
    /// </summary>

namespace Jinsol
{
    public class ChildQuestPoint : MonoBehaviour
    {
        private Transform player;
        private bool playerNearby = false;
        [SerializeField] private EventTrigger myEventTrigger; // �÷��̾ ��ó�� ������ Ŭ���� �� ����

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
