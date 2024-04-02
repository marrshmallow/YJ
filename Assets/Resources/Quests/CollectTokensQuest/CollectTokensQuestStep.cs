// 토큰마다 그게 존재하고 토큰마다 Token.cs 붙여주기.

namespace Jinsol
{
    public class CollectTokensQuestStep : QuestStep
    {
        private int tokensCollected = 0;
        private int tokensRequired = 12; // 부스마다 토큰 하나씩 총 11개 부스. 남은 하나는 맨 처음에 NPC에게 퀘스트 진행을 하겠다고 말하면 얻게 됨: 이유는 인생에 있어서 큰 용기를 내서 큰 선택을 했기 때문.

        // OnEnable 단계에서 GameEventsManager의 신호를 구독
        private void OnEnable()
        {
            GameEventsManager.instance.miscEvents.onTokenCollected += TokenCollected;
        }

        // OnDisable일 때 구독 취소
        private void OnDisable()
        {
            GameEventsManager.instance.miscEvents.onTokenCollected -= TokenCollected;
        }

        private void TokenCollected()
        {
            if (tokensCollected < tokensRequired) // 현재 가지고 있는 토큰의 갯수가 달성조건보다 모자라면
            {
                tokensCollected++; // 토큰 획득 시 현재 가지고 있는 토큰의 갯수를 하나만큼 늘려준다
            }

            if (tokensCollected >= tokensRequired) // 달성조건만큼의 토큰이 모이면
            {
                CompleteQuestStep(); // 이 퀘스트 단계를 완료하고 다음으로 넘어간다
            }
        }
    }
}
