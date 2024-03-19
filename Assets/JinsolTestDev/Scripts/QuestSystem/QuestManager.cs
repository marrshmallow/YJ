using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private Dictionary<string, Quest> questMap; // 전체 퀘스트 상황을 지도처럼 정리

    private void Awake()
    {
        questMap = CreateQuestMap();
    }

    private void OnEnable()
    {
        GameEventsManager.instance.questEvents.onStartQuest += StartQuest;
        GameEventsManager.instance.questEvents.onProceedQuest += ProceedQuest;
        GameEventsManager.instance.questEvents.onCompleteQuest += CompleteQuest;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.questEvents.onStartQuest -= StartQuest;
        GameEventsManager.instance.questEvents.onProceedQuest -= ProceedQuest;
        GameEventsManager.instance.questEvents.onCompleteQuest -= CompleteQuest;
    }

    private void Start()
    {
        // 게임 시작할 때 모든 퀘스트의 초기 상태를 선언
        foreach (Quest quest in questMap.Values)
            GameEventsManager.instance.questEvents.QuestStateChange(quest);
    }

    private void StartQuest(string id)
    {
        // 퀘스트 시작시키는 기능 추가
        Debug.Log("퀘스트 수락: " + id);
    }

    private void ProceedQuest(string id)
    {
        // 기능 추가
        Debug.Log("다음 퀘스트로 진행: " + id);
    }

    private void CompleteQuest(string id)
    {
        // 기능 추가
        Debug.Log("퀘스트 완료!: " + id);
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
