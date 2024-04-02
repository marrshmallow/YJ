using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// 게임 이벤트용 스크립터블 오브젝트
/// 
/// - 정진솔
/// </summary>

namespace Jinsol
{
    [CreateAssetMenu(fileName = "GameEvent", menuName = "ScriptableObjects/GameEvent")]
    public class GameEvent : ScriptableObject
    {
        public List<GameEventListener> listeners = new List<GameEventListener>();

        // 이벤트 송출
        public void Raise()
        {
            for (int i = 0; i < listeners.Count; i++)
            {
                listeners[i].OnEventRaised();
            }
        }

        // 구독자 관리
        public void RegisterListener(GameEventListener listener)
        {
            if (!listeners.Contains(listener)) // 중복이 아니라면 구독자로 등록
                listeners.Add(listener);
        }

        public void UnregisterListener(GameEventListener listener)
        {
            if (listeners.Contains(listener))
                listeners.Remove(listener);
        }
    }
}
