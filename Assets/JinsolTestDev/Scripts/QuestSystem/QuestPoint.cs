using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Playables;

/// <summary>
/// 
/// 여기에서 퀘스트 수락(시작)과 완료를 담당
/// '퀘스트 수락/완료 장소'에 이 스크립트 삽입
/// 
/// - 정진솔
/// </summary>

// 20240320 현재 문제점 (해결 완료) 클릭해도 퀘스트 수락이 될 때가 있고 안 될 때가 있음
// >> 20240320 : NPC 이벤트 박스 컬라이더가 방해가 되어서 안 눌러진 것. 박스 컬라이더를 내리거나 레이어 등으로 처리
// >> 20240404 : 카메라에 Physics Raycaster 컴포넌트를 추가
// UI (현재 퀘스트 아이콘의 레이어) 인식하게 설정한 뒤 Max Ray Intersections 수치를 1로 지정
// 이것만 했을 때 멀리 있어도 클릭할 수 있게 되므로 스크립트로 제한해 줘야 합니다. (완료)

// NPC의 컬라이더 범위 안에 들어오게 되면 퀘스트를 받을 수 있는 조건 중 하나를 충족

namespace Jinsol
{
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
        private Transform player;
        //private Quaternion forward; // NPC가 원래 보고 있던 방향
        //[SerializeField] private PlayableDirector director; // 타임라인이 재생중인지 아닌지를 읽어서 컷씬 재생중에 회전값을 초기화 시켜주려고
        [SerializeField] protected bool playerNearby; // 플레이어가 근처에 있는지
        [SerializeField] private EventTrigger myEventTrigger; // 플레이어가 근처에 없으면 클릭할 수 없게

        [Header("퀘스트 목록 갱신용")]
        [SerializeField] private Transform questList; // Instantiate 할 곳
        [SerializeField] private GameObject questTitlePrefab; // 퀘스트 제목 프리팹 (button)
        [SerializeField] private GameObject questStepPrefab; // 퀘스트 단계 프리팹 (button)

        // 프리팹을 생성하고 지우는 작업이 자주 이루어지기 때문에 Array 대신 List 사용
        [SerializeField] private List<GameObject> questTitleList = new(); // 퀘스트 제목 프리팹을 구분해줄 목록
        [SerializeField] private List<GameObject> questStepList = new(); // 퀘스트 단계 프리팹을 구분해줄 목록

        // 연출용
        [SerializeField] private PlayableDirector director;
        [SerializeField] private PlayableAsset startQuestCutscene;
        [SerializeField] private PlayableAsset finishQuestCutscene;

        private void Awake()
        {
            questId = questInfoForPoint.id;
            player = GameObject.FindGameObjectWithTag("Player").transform;
            questIcon = GetComponentInChildren<QuestIcon>();
            myEventTrigger = GetComponentInChildren<EventTrigger>();
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

        private void Update()
        {
            /*if (playerNearby) // 플레이어가 주변에 있을 때 NPC가 바라보게 하는 효과 (prototype)
                //npc.transform.LookAt(player); // NPC가 반대방향이라서 안됨
                npc.rotation = Quaternion.LookRotation(transform.position - player.position);
            else npc.rotation = forward;

            if (director.state == PlayState.Playing)
                LookForward();*/
            
            /*if (playerNearby)
            {
                myEventTrigger.enabled = true;
            }
            else
                myEventTrigger.enabled = false;*/
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

        /*    public void LookForward() // 플레이어가 근처에 없을 때 NPC가 다시 정면을 보게 만들어 주려던 함수 (prototype)
            {
                playerNearby = false;
                npc.rotation = forward;
            }*/

        public void AcceptQuest() // 퀘스트 수락 기능
        {
            #region 퀘스트를 시작하거나 완료
            // 지금 상태가 퀘스트를 시작할 수 있는 상태이고 이 지점이 퀘스트 수주 장소라면
            if (currentQuestState.Equals(QuestState.CAN_START) && startPoint)
            {
                director.Play(startQuestCutscene);
                GameEventsManager.instance.questEvents.StartQuest(questId); // 퀘스트 시작
                GameObject newQuestTitle = Instantiate(questTitlePrefab, questList); // 리스트를 만들어서 관리해야 할 것 같다
                questTitleList.Add(newQuestTitle);
                GameObject newQuestStep = Instantiate(questStepPrefab, questList); // 생성된 프리팹의 인덱스를 특정해서 지워야
                questStepList.Add(newQuestStep);
            }
            else if (currentQuestState.Equals(QuestState.CAN_COMPLETE) && finishPoint)
            {
                // 지금 상태가 퀘스트를 끝낼 수 있는 상태이고 이 지점이 퀘스트 완료 장소라면
                director.RebuildGraph();
                director.Play(finishQuestCutscene);
                GameEventsManager.instance.questEvents.CompleteQuest(questId); // 퀘스트 완료
            }
            #endregion
        }
    }
}
