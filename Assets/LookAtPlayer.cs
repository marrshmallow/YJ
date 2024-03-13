using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public Transform npc;
    public Transform player;
    private Quaternion forward; // 원래 보고 있던 방향
    public bool isLooking = false;

    private void Awake()
    {
        forward = npc.transform.rotation;
    }

    private void Update()
    {
        if (isLooking)
            //npc.transform.LookAt(player); // NPC가 반대방향이라서 안됨
            npc.rotation = Quaternion.LookRotation(transform.position - player.position);
        else npc.rotation = forward;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
            isLooking = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player") 
            isLooking = false;

    }

    public void LookForward()
    {
        isLooking = false;
        npc.rotation = forward;
    }
}
