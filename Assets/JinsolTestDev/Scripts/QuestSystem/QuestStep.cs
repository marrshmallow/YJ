using Unity.Properties;
using UnityEngine;

/// <summary>
/// 
/// 퀘스트 안의 단계에 관한 기능
/// 여기에서 ProceedQuest 담당
/// 추후에 다른 퀘스트가 생겨날 때를 대비한 기본 틀!
/// 
/// - 정진솔
/// </summary>

namespace Jinsol
{
    public abstract class QuestStep : MonoBehaviour
    {
        private bool isCompleted = false;
        private string questId;

        public void InitializeQuestStep(string questId)
        {
            this.questId = questId; // 받은 퀘스트의 아이디로 현재 퀘스트 아이디를 갱신
        }

        protected void CompleteQuestStep()
        {
            if (!isCompleted)
            {
                isCompleted = true;
                GameEventsManager.instance.questEvents.ProceedQuest(questId);
                //Destroy(this.gameObject); // 씬에 남아있으면 안되니까 없애주기
            }
        }
    }
}
