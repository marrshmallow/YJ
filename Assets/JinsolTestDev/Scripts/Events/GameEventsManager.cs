using UnityEngine;

/// <summary>
/// 
/// 게임이벤트 매니저
/// 
/// - 정진솔
/// </summary>

namespace Jinsol
{
    public class GameEventsManager : MonoBehaviour
    {
        public static GameEventsManager instance { get; private set; }

        public PlayerEvents playerEvents; // 플레이어 상태 감지용
        public QuestEvents questEvents; // 퀘스트 진행 상황 감지용
        public TokenEvents tokenEvents; // 토큰 수집 상황 감지용
        public MiscEvents miscEvents; // 기타 퀘스트 진행에 필요한 이벤트 감지용

        private void Awake()
        {
            if (instance != null) // 시작할 때 체크
            {
                Debug.LogError("씬에 GameEventsManager가 중복 실행 중입니다.");
            }
            instance = this;

            #region 모든 이벤트 초기화
            questEvents = new QuestEvents();
            playerEvents = new PlayerEvents();
            miscEvents = new MiscEvents();
            tokenEvents = new TokenEvents();
            #endregion
        }
    }
}
