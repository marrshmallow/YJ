using UnityEngine;

/// <summary>
/// 
/// 스크립터블 오브젝트(QuestInfoSO)에서 퀘스트에 대한 기본 정보를 불러와 인스턴스를 새로 생성해 주는 스크립트
/// 
/// - 정진솔
/// </summary>

namespace Jinsol
{

    public class Quest
    {
        public QuestInfoSO info; // 퀘스트에 관련된 static 데이터 참조
        public QuestState state; // 퀘스트 상황 참조
        private int currentQuestStepIndex; // 퀘스트 내부 단계 진행 상황 참조
        public Quest(QuestInfoSO questInfo) // public constructor that passes info data to the quest
        {
            this.info = questInfo; // 이 info 정보를 현재 퀘스트에 전달해주고
            this.state = QuestState.REQUIREMENTS_NOT_MET; // 현재 퀘스트 수주 상태를 '불가능'으로 바꾸고 (초기화)
            this.currentQuestStepIndex = 0; // 현재 퀘스트 단계 진행도를 0으로 초기화
        }

        public void MoveToNextStep() // 다음 단계로 넘겨주는 기능
        {
            currentQuestStepIndex++;
        }

        public bool CurrentStepExists() // 현재 진행도가 유효한지 체크 (MoveToNextStep 남발하다가 OutOfRange 문제 발생 가능)
        {
            return (currentQuestStepIndex < info.questStepPrefabs.Length); // 전체 단계보다 낮은 수치면 bool = true
        }

        public void InstantiateCurrentQuestStep(Transform parentTransform)
        {
            GameObject questStepPrefab = GetCurrentQuestStepPrefab();
            if (questStepPrefab != null)
            {
                QuestStep questStep = Object.Instantiate<GameObject>(questStepPrefab, parentTransform).GetComponent<QuestStep>(); // 현재 프로젝트에서는 문제없지만 분량 늘어나면 Object pooling으로 처리해야
                questStep.InitializeQuestStep(info.id);
            }
        }

        private GameObject GetCurrentQuestStepPrefab()
        {
            GameObject questStepPrefab = null;

            if (CurrentStepExists())
                questStepPrefab = info.questStepPrefabs[currentQuestStepIndex];
            else
                Debug.LogWarning("Tried to get quest step prefab, but stepIndex was out of range. 존재하지 않는 진행 단계: " + info.id + ", stepIndex = " + currentQuestStepIndex);

            return questStepPrefab;
        }
    }
}
