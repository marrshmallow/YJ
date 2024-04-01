using UnityEngine;

namespace Jinsol
{
    [RequireComponent(typeof(InputReader))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(CharacterController))]
    public class PlayerStateMachine : StateMachine
    {
        public Vector3 velocity;
        public float moveSpeed = 15f;
        public float jumpForce = 5f;
        public float sprintMultiplier = 3f;
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
}
