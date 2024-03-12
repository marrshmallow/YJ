using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerBaseState
{
        // moveSpeedHash와 moveBlendTreeHash는 애니메이터의 MoveSpeed 파라미터와 MoveBlendTree(블렌드트리)를 나타내는 정수 식별자
        // 유니티의 애니메이터 클래스에 있는 정적 함수 StringToHash를 사용해서 특별한 숫자로 변환해 준다.
        // 변환하지 않으면 해시는 이론적으로 서로 충돌 가능하기 때문. "hash collision"
        // 굳이 정수를 쓴 이유는, 문자열을 비교하는 것보다 훨씬 빠르기 때문.
        private readonly int MoveSpeedHash = Animator.StringToHash("MoveSpeed");
        private readonly int MoveBlendTreeHash = Animator.StringToHash("MoveBlendTree");
        private const float animationDampTime = 0.1f; // 기기의 프레임과 상관없이 부드러운 표현
        private const float crossFadeDuration = 0.1f;
        public PlayerMoveState(PlayerStateMachine stateMachine): base(stateMachine){}
    
    public override void Enter()
    {
        stateMachine.velocity.y = Physics.gravity.y;
        stateMachine.animator.CrossFadeInFixedTime(MoveBlendTreeHash, crossFadeDuration);
        stateMachine.inputReader.OnJumpPerformed += SwitchToJumpState;
    }
    
    public override void Tick()
    {
        if(!stateMachine.charController.isGrounded)
        {
            stateMachine.SwitchState(new PlayerFallState(stateMachine));
        }

        CalculateMoveDirection();
        //FaceMoveDirection();
        Move();

        //실제 값보다도 0인가 1인가에만 관심있으므로 sqr.magnitude 사용
        stateMachine.animator.SetFloat(MoveSpeedHash, stateMachine.inputReader.moveComposite.sqrMagnitude > 0f ? 1f : 0f, animationDampTime, Time.deltaTime);
    }

    public override void Exit()
    {
        stateMachine.inputReader.OnJumpPerformed -= SwitchToJumpState;
    }

    private void SwitchToJumpState()
    {
        stateMachine.SwitchState(new PlayerJumpState(stateMachine));
    }
}
