using UnityEngine;
using UnityEngine.Playables;

public class LookAtPlayer : MonoBehaviour
{
    public Transform npc;
    public Transform player;
    private Quaternion forward; // 원래 보고 있던 방향
    [SerializeField] private PlayableDirector director; // 타임라인이 재생중인지 아닌지를 읽어서 컷씬 재생중에 회전값을 초기화 시켜주려고
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

        if (director.state == PlayState.Playing)
            LookForward();
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
