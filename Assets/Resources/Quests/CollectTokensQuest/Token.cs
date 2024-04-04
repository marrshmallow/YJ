using UnityEngine;

/// <summary>
/// 부스의 NPC와 이야기할 때마다 획득하는 증표에 부착
/// 실질적으로는 부스 NPC 위에 뜨는 아이콘에 부착
/// 대화로 진입하게 되면 대화가 끝날 때까지 타임라인 재생이 강행되므로
/// 진입 시 토큰 하나 획득한 걸로 처리하지만
/// 표현은 대화가 끝나면 하는 방식
/// 
/// - 정진솔
/// </summary>

namespace Jinsol
{
    [RequireComponent(typeof(SphereCollider))]
    public class Token : MonoBehaviour
    {
        [Header("설정")]
        [SerializeField] private int tokensCollected = 1;

        // 속한 퀘스트의 진행 상황에 따라 아이콘 색상을 변하지 않게 하기 위한 부분
        private QuestIcon questIcon;

        private SphereCollider sphereCollider;

        private void Awake()
        {
            questIcon = (QuestIcon)GetComponent("QuestIcon");
            sphereCollider = (SphereCollider)GetComponent("SphereCollider");
            //questIcon.isSubQuest = true;
        }

        public void CollectToken()
        {
            sphereCollider.enabled = false;
            GameEventsManager.instance.tokenEvents.TokenCollected(tokensCollected);
            GameEventsManager.instance.miscEvents.TokenCollected();
            GameEventsManager.print(tokensCollected);
        }
    }
}
