using System;

/// <summary>
/// 토큰 획득 상황을 업데이트 하는 기능
/// - 정진솔
/// </summary>

namespace Jinsol
{
    public class MiscEvents
    {
        public event Action onTokenCollected; // 토큰을 획득했다는 이벤트 발생

        public void TokenCollected()
        {
            if (onTokenCollected != null) // 획득 안 한 게 아니라면
                onTokenCollected();
        }

        // 이 밑으로 저런 식으로 기타 등등 내용 추가 가능
    }
}
