using System;

/// <summary>
/// 플레이어의 레벨을 변경해주고, 그때마다 이벤트를 선언하는 기능
/// - 정진솔
/// </summary>

namespace Jinsol
{
    public class PlayerEvents
    {
        public event Action<int> onPlayerLevelChange;
        public void PlayerLevelChange(int level)
        {
            if (onPlayerLevelChange != null)
            {
                onPlayerLevelChange(level);
            }
        }
    }
}
