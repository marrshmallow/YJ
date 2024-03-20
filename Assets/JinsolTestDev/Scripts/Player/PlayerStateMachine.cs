using UnityEngine;

// StateMachine을 그냥 쓰지 않는 이유: 혹시 다른 데 쓸 수도 있으니까!

[RequireComponent(typeof(InputReader))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]
public class PlayerStateMachine : StateMachine
{
    public Vector3 velocity;
    public float moveSpeed = 15f;
    public float jumpForce = 5f;
    public float moveSpeedModifier = 3f;
    public Transform mainCamera;
    
    public InputReader inputReader;
    public Animator animator;
    public CharacterController charController;

    private void Awake()
    {
        mainCamera = this.gameObject.transform.GetChild(0); // 이를 위해서는 플레이어의 카메라가 반드시 가장 위의 자식이어야 한다
        inputReader = (InputReader)GetComponent("InputReader");
        animator = (Animator)GetComponent("Animator");
        charController = (CharacterController)GetComponent("CharacterController");
        SwitchState(new PlayerMoveState(this));
    }
}
