using UnityEngine;

// 부모 상태
public abstract class PlayerBaseState : State
{
    protected readonly PlayerStateMachine stateMachine;
    protected PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    protected void CalculateMoveDirection()
    {
        Vector3 cameraForward = new (stateMachine.mainCamera.forward.x, 0, stateMachine.mainCamera.forward.z);
        Vector3 cameraRight = new(stateMachine.mainCamera.right.x, 0, stateMachine.mainCamera.right.z);
        Vector3 moveDirection = cameraForward.normalized * stateMachine.inputReader.moveComposite.y + cameraRight.normalized * stateMachine.inputReader.moveComposite.x;
        stateMachine.velocity.x = moveDirection.x * stateMachine.moveSpeed;
        stateMachine.velocity.z = moveDirection.z * stateMachine.moveSpeed;
    }

    // 바라보는 방향
    // protected void FaceMoveDirection()
    // {
    //     Vector3 faceDirection = new(stateMachine.velocity.x, 0f, stateMachine.velocity.z);
    //     if (faceDirection == Vector3.zero)
    //         return;
    //     stateMachine.transform.rotation = Quaternion.Slerp(stateMachine.transform.rotation, Quaternion.LookRotation(faceDirection), stateMachine.lookRotationDampFactor * Time.deltaTime);
    // }

    // 중력 구현
    protected void ApplyGravity()
    {
        if(stateMachine.velocity.y > Physics.gravity.y)
        {
            stateMachine.velocity.y += Physics.gravity.y * Time.deltaTime;
        }
    }

    protected void Move()
    {
        stateMachine.charController.Move(stateMachine.velocity * Time.deltaTime);
    }

    public override void Enter()
    {

    }

    public override void Tick()
    {

    }

    public override void Exit()
    {

    }

    private float getMoveSpeed()
    {
        float moveSpeed = stateMachine.moveSpeed * stateMachine.moveSpeedModifier;
        return moveSpeed;
    }
}
