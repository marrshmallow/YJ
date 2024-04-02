using System;

/// <summary>
/// NPC와의 대화에서 얻은 토큰의 개수를 업데이트해 주는 기능.
/// - 정진솔
/// </summary>

namespace Jinsol
{
    public class TokenEvents
    {
        public event Action<int> onTokenCollected;
        public void TokenCollected(int token)
        {
            if (onTokenCollected != null)
            {
                onTokenCollected(token);
            }
        }
    }
}
