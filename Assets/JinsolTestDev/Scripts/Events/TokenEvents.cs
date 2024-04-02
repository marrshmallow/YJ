using System;

/// <summary>
/// NPC���� ��ȭ���� ���� ��ū�� ������ ������Ʈ�� �ִ� ���.
/// - ������
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
