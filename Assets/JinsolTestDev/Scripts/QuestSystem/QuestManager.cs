using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private Dictionary<string, Quest> questMap; // 전체 퀘스트 상황을 지도처럼 정리
    //private EPlayerState currentPlayerStatus; // 플레이어의 상태 체크
    //private int currentPlayerLevel; // Enum 상태 안돼서 레벨로 가정.
    [SerializeField] private Player player;
    // 레벨0 = 지나가던 행인
    // 레벨1 = 방문객
    // 레벨2 = 관람객
    // 레벨3 = 모험가

    private void Awake()
    {
        questMap = CreateQuestMap();
    }

    private void OnEnable()
    {
        GameEventsManager.instance.questEvents.onStartQuest += StartQuest;
        GameEventsManager.instance.questEvents.onProceedQuest += ProceedQuest;
        GameEventsManager.instance.questEvents.onCompleteQuest += CompleteQuest;

        GameEventsManager.instance.playerEvents.onPlayerLevelChange += PlayerLevelChange;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.questEvents.onStartQuest -= StartQuest;
        GameEventsManager.instance.questEvents.onProceedQuest -= ProceedQuest;
        GameEventsManager.instance.questEvents.onCompleteQuest -= CompleteQuest;

        GameEventsManager.instance.playerEvents.onPlayerLevelChange -= PlayerLevelChange;
    }

    private void Start()
    {
        // 게임 시작할 때 모든 퀘스트의 초기 상태를 선언
        foreach (Quest quest in questMap.Values)
            GameEventsManager.instance.questEvents.QuestStateChange(quest);
    }

    private void StartQuest(string id)
    {
        Quest quest = GetQuestById(id);
        quest.InstantiateCurrentQuestStep(this.transform); // 씬 안의 QuestManager 자식으로 수락한 퀘스트가 쌓이게 처리
        ChangeQuestState(quest.info.id, QuestState.IN_PROGRESS);
        Debug.Log("퀘스트 수락: " + id);
    }

    private void ProceedQuest(string id)
    {
        Quest quest = GetQuestById(id);
        quest.MoveToNextStep(); // 다음 단계로 이동
        if (quest.CurrentStepExists()) // 할 게 남았다면 그 단계를 생성
        {
            quest.InstantiateCurrentQuestStep(this.transform);
        }
        else // 더 단계가 없다는 건 퀘스트 완료를 의미하므로
        {
            ChangeQuestState(quest.info.id, QuestState.CAN_COMPLETE);
        }
        Debug.Log("다음 퀘스트로 진행: " + id);
    }

    private void CompleteQuest(string id)
    {
        Quest quest = GetQuestById(id);
        ClaimRewards(quest);
        ChangeQuestState(quest.info.id, QuestState.COMPLETED);
        Debug.Log("퀘스트 완료!: " + id);
    }

    private void ClaimRewards(Quest quest)
    {
        GameEventsManager.instance.tokenEvents.TokenCollected(quest.info.tokenReward);
    }

    public void PlayerLevelChange(int level)
    {
        player.currentLevel = level;
    }

    private bool CheckRequirements(Quest quest)
    {
        bool meetsRequirements = true;

        if (player.currentLevel < quest.info.levelRequired)
        {
            meetsRequirements = false;
        }

        // 다른 전제 조건이 있는지 스캔
        foreach (QuestInfoSO prerequisiteQuestInfo in quest.info.questPrerequisites)
        {
            if (GetQuestById(prerequisiteQuestInfo.id).state !=QuestState.COMPLETED)
            {
                meetsRequirements = false;
                Debug.Log("퀘스트를 클리어했기 때문에 받을 수 없습니다.");
            }
        }

        return meetsRequirements;
    }

    private void Update()
    {
        // 퀘스트 하나 하나 확인
        foreach (Quest quest in questMap.Values)
        {
            if (quest.state == QuestState.REQUIREMENTS_NOT_MET && CheckRequirements(quest))
            {
                ChangeQuestState(quest.info.id, QuestState.CAN_START);
            }
        }
    }

    // 퀘스트의 상태를 가져오고 업데이트
    private void ChangeQuestState(string id, QuestState state)
    {
        Quest quest = GetQuestById(id);
        quest.state = state;
        GameEventsManager.instance.questEvents.QuestStateChange(quest);
    }

    private Dictionary<string, Quest> CreateQuestMap()
    {
        QuestInfoSO[] allQuests = Resources.LoadAll<QuestInfoSO>("Quests"); // Asset/Resources 안의 Quests 폴더 안의 모든 QuestInfoSO 타입을 불러온다 (폴더명, 경로 코드상으로든 인스펙터상으로든 바꾸면 안됨)
        Dictionary<string, Quest> idToQuestMap = new Dictionary<string, Quest>();
        foreach (QuestInfoSO questInfo in allQuests)
        {
            if (idToQuestMap.ContainsKey(questInfo.id))
                Debug.LogWarning("중복되는 퀘스트 아이디가 있습니다: " + questInfo.id);
            
            idToQuestMap.Add(questInfo.id, new Quest(questInfo));
        }
        return idToQuestMap;
    }

    // 에러 방지용
    private Quest GetQuestById(string id) // id의 문자열을 받으면 Dictionary에 저장된 값을 불러준다
    {
        Quest quest = questMap[id];
        if (quest == null)
            Debug.LogError("퀘스트 ID를 찾을 수 없습니다." + id);
        
        return quest;
    }
}
