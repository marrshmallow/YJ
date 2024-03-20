using UnityEngine;
using UnityEngine.Playables;

// 여기에서 퀘스트 수락(시작)과 완료를 담당
// 즉, 이곳이 '퀘스트 수락/완료 장소'

// NPC의 컬라이더 범위 안에 들어오게 되면
[RequireComponent(typeof(BoxCollider))]
public class QuestPoint : MonoBehaviour
{
    [Header("퀘스트")]
    [SerializeField] private QuestInfoSO questInfoForPoint;
    private string questId;
    private QuestState currentQuestState;
    private QuestIcon questIcon;

    [Header("퀘스트 설정")]
    [SerializeField] private bool startPoint = true;
    [SerializeField] private bool finishPoint = true;

    [SerializeField] private Transform npc;
    private Transform player; // 포톤 하게 되면 이부분 Awake에서 저렇게 부르면 안될 것 같다
    private Quaternion forward; // 원래 보고 있던 방향
    [SerializeField] private PlayableDirector director; // 타임라인이 재생중인지 아닌지를 읽어서 컷씬 재생중에 회전값을 초기화 시켜주려고
    [SerializeField] private bool playerNearby; // 플레이어가 근처에 있는지
    private void Awake()
    {
        questId = questInfoForPoint.id;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        director = (PlayableDirector)FindObjectOfType(typeof(PlayableDirector   ));
        forward = npc.transform.rotation;
        questIcon = GetComponentInChildren<QuestIcon>();
    }

    private void OnEnable()
    {
        GameEventsManager.instance.questEvents.onQuestStateChange += QuestStateChange;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.questEvents.onQuestStateChange -= QuestStateChange;
    }

    private void QuestStateChange(Quest quest)
    {
        // 이 포인트에 해당하는 퀘스트가 있을 때에만 퀘스트 상태 업데이트
        if (quest.info.id.Equals(questId))
        {
            currentQuestState = quest.state;
            questIcon.SetState(currentQuestState, startPoint, finishPoint);
        } 
    }

    public void SubmitPressed()
    {
        if (!playerNearby)
        {
            return;
        }

        #region 퀘스트를 시작하거나 완료
        // 지금 상태가 퀘스트를 시작할 수 있는 상태이고 이 지점이 퀘스트 수주 장소라면
        if (currentQuestState.Equals(QuestState.CAN_START) && startPoint)
        {
            GameEventsManager.instance.questEvents.StartQuest(questId); // 퀘스트 시작
            Debug.Log("따-다-다-다-");
        }
        else if (currentQuestState.Equals(QuestState.CAN_COMPLETE) && finishPoint)
        {
            // 지금 상태가 퀘스트를 끝낼 수 있는 상태이고 이 지점이 퀘스트 완료 장소라면
            GameEventsManager.instance.questEvents.CompleteQuest(questId); // 퀘스트 완료
            Debug.Log("따다다다- 따-다 따! 따다~");
        }
        #endregion
    }

    private void Update()
    {
        if (playerNearby)
            //npc.transform.LookAt(player); // NPC가 반대방향이라서 안됨
            npc.rotation = Quaternion.LookRotation(transform.position - player.position);
        else npc.rotation = forward;

        if (director.state == PlayState.Playing)
            LookForward();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        playerNearby = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
            playerNearby = false;

    }

    public void LookForward()
    {
        playerNearby = false;
        npc.rotation = forward;
    }

    public void AcceptQuest()
    {
        if (!playerNearby)
        {
            Debug.Log("PlayerNearby: " + playerNearby);
            Debug.Log("NPC에게 좀 더 다가가세요!"); // 왜인지 모르겠는데 시작 지점과 종료 지점을 다르게 설정했을 때 이 오류 생김
            // https://www.youtube.com/watch?v=UyTJLDGcT64&t=1958s 다시 차근차근 봐야
            return;
        }
        
        if (currentQuestState.Equals(QuestState.CAN_START) && startPoint)
        {
            GameEventsManager.instance.questEvents.StartQuest(questId);
            Debug.Log("클릭: startPoint");
        }
        else if (currentQuestState.Equals(QuestState.CAN_COMPLETE) && finishPoint)
        {
            GameEventsManager.instance.questEvents.CompleteQuest(questId);
            Debug.Log("클릭: finishPoint");
        }
    }
}
