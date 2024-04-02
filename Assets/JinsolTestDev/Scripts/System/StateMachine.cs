using UnityEngine;

/// <summary>
/// 
/// 상태 머신 틀
/// 
/// - 정진솔 
/// </summary>

namespace Jinsol
{
    public class StateMachine : MonoBehaviour
    {
        private State currentState;

        public void SwitchState(State state)
        {
            currentState?.Exit(); // 현재 상태에서 나온 다음에
            currentState = state; // 다른 상태로 바꿔준다
            currentState.Enter();
        }

        private void Update()
        {
            currentState?.Tick(); // 프레임마다 상태 업데이트
        }
    }
}
