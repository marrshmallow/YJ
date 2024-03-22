using UnityEngine;

public class PlayerRunState : PlayerBaseState
{
    private readonly int RunHash = Animator.StringToHash("Run");
    private const float CrossFadeDuration = 0.1f; // 애니메이션 크로스페이드 값 고정
    public PlayerRunState(PlayerStateMachine stateMachine) : base(stateMachine){}

    public override void Enter()
    {
        stateMachine.animator.CrossFadeInFixedTime(RunHash, CrossFadeDuration);
    }

    public override void Tick()
    {
    }

    public override void Exit()
    {
        base.Exit();
    }
}
