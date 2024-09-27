using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

/// <summary>
/// 
/// 이 코드를 플레이어에게 붙여서 입력 신호를 감지합니다.
/// Controllers.IPlayerActions는 다른 컨트롤러가 연결되었을 때를 위한 인터페이스입니다.

/// by 정진솔
/// </summary>

/*해 줘야 하는 설정들
1. InputAction이 Asset > Settings > Input에 생성되어 있을 것.
2. 이동은 Value > 2D WASD. 둘러보는 건 Value > Delta.
주의사항: 이름! 정말! 중요! 코드 쓸 때 헷갈리지 않게 조심.*/

namespace Jinsol
{
    public class InputReader : MonoBehaviour, Controllers.IPlayerActions
    {
        public Vector2 delta;
        public Vector2 moveComposite;
        [SerializeField] private float sprintMultiplier = 3f;
        [SerializeField] private float camMoveSpeed;
        [SerializeField] private float camSpeed = 1f;
        public Action OnJumpPerformed;
        public Action OnRunPerformed;
        private Controllers controllers;
        private Player player;
        private CharacterController charController;
        private float jumpPower = 3f;

        #region 발걸음 소리용
        [SerializeField] private GameObject footstep;
        #endregion

        #region 카메라 시점 변환용
        public CinemachineVirtualCamera mainCam; // 현재 주도권을 가진 카메라
        public CinemachineVirtualCamera defaultCam; // 원래카메라
        public CinemachineVirtualCamera playerLookCam; // 플레이어의 모습을 관찰하기 위한 카메라
        public CinemachineVirtualCamera firstPersonCam; // 플레이어 시점 카메라
        public bool toggleCam = false; // 껐다켰다 스위치
        [SerializeField] private CinemachineBrain brain; // 1인칭 전환을 위한 시네머신 브레인 참조
        public bool togglePOV = false; // 1인칭 시점 스위치
        private float defaultBlendTime = 0f; // 1인칭 전환 속도 (기본값)
        public float scrollY; // 마우스 스크롤 값 저장
        private Quaternion nextRotation;
        private Vector3 nextPosition;
        #endregion

        #region 카메라 회전용
        [SerializeField] private GameObject followTransform;
        [SerializeField] private float rotationPower = 0.1f;
        [SerializeField] private float rotationLerp = 10f;
        [SerializeField] private float aimValue;
        #endregion

        [SerializeField] private PlayableDirector director;

        private void Awake()
        {
            charController = (CharacterController)GetComponent("CharacterController");
        }

        private void OnEnable()
        {
            if (controllers != null) // 컨트롤러가 연결돼 있다면 컨트롤러 연결해 줄 필요가 없으므로 여기서 중단.
                return;

            controllers = new Controllers(); // 새 인스턴스 생성
            controllers.Player.SetCallbacks(this); // 콜백 호출 설정
            controllers.Player.Enable(); // 입력 활성화
            controllers.Player.Movement.canceled += context => { footstep.SetActive(false); };
            controllers.Player.CameraZoom.performed += context => { scrollY = context.ReadValue<float>() * 0.02f * -1; };
            director.played += OnPlayableDirectorPlayed;
            director.stopped += OnPlayableDirectorStopped;
            player = (Player)GetComponent("Player");
        }

        private void OnDisable()
        {
            controllers.Player.Disable(); // 컴포넌트 꺼지면 입력도 비활성화
            controllers.Player.Movement.canceled -= context => { footstep.SetActive(false); };
            controllers.Player.CameraZoom.performed -= context => { scrollY = context.ReadValue<float>() * 0.02f * -1; };
            director.played -= OnPlayableDirectorPlayed;
            director.stopped -= OnPlayableDirectorStopped;
        }

        // 이것도 전에 OnMove였는데 OnMovement로 바뀌었다ㅏㅏㅏ
        public void OnMovement(InputAction.CallbackContext context)
        {
            // WASD 누를 떄마다 OnMove가 호출되어 매핑대로 Vector2 값을 읽어온다
            moveComposite = context.ReadValue<Vector2>();
            footstep.SetActive(true);
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (!context.performed) // 이중 점프 방지용
                return;
            if (!charController.isGrounded)
                return;

            float jumpVelocity = Mathf.Sqrt(jumpPower * -2 * Physics.gravity.y);
        }
        public void OnLookAround(InputAction.CallbackContext context)
        {
            delta = context.ReadValue<Vector2>();
        }

        public void OnLookAround_Gamepad(InputAction.CallbackContext context)
        {
            Gamepad.current.leftStick.ReadValue();
        }

        public void OnRun(InputAction.CallbackContext context)
        {
            //moveComposite = context.ReadValue<Vector2>() * sprintMultiplier;
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            //player.PlayerInteract(); // 이건 좀 뒤에
            Debug.Log("Interacting..." + context);

        }

        public void OnPlayerLookCam(InputAction.CallbackContext context)
        {
            toggleCam = !toggleCam;
            if (toggleCam)
            {
                playerLookCam.gameObject.SetActive(true);
                mainCam = playerLookCam;
                mainCam.MoveToTopOfPrioritySubqueue();
            }
            else
            {
                playerLookCam.gameObject.SetActive(false);
                mainCam = defaultCam;
                mainCam.MoveToTopOfPrioritySubqueue();
            }
        }

        public void OnFirstPersonToggle(InputAction.CallbackContext context)
        {
            togglePOV = !togglePOV;
            if (togglePOV)
            {
                firstPersonCam.gameObject.SetActive(true);
                brain.m_DefaultBlend.m_Time = 1f;
                mainCam = firstPersonCam;
                mainCam.MoveToTopOfPrioritySubqueue();
            }
            else
            {
                firstPersonCam.gameObject.SetActive(false);
                brain.m_DefaultBlend.m_Time = defaultBlendTime;
                mainCam = defaultCam;
                mainCam.MoveToTopOfPrioritySubqueue();
            }
        }

        public void OnPlayableDirectorPlayed(PlayableDirector director)
        {
            if (director.state == PlayState.Playing) // 타임라인 재생 중에는 인풋 시스템을 비활성화
                controllers.Player.Disable();
        }

        public void OnPlayableDirectorStopped(PlayableDirector director)
        {
            controllers.Player.Enable();
        }

        public void ForceEnableInput()
        {
            controllers.Player.Enable();
        }

        public void OnCameraZoom(InputAction.CallbackContext context)
        {
            if (scrollY < 0 && mainCam.m_Lens.FieldOfView <= 15)
                mainCam.m_Lens.FieldOfView = 15;
            else if (scrollY > 0 && mainCam.m_Lens.FieldOfView >= 60)
                mainCam.m_Lens.FieldOfView = 60;
            else
                mainCam.m_Lens.FieldOfView += scrollY;
        }

        private void Update()
        {
            #region Follow Transform Rotation
            followTransform.transform.rotation *= Quaternion.AngleAxis(delta.x * rotationPower, Vector3.up);
            #endregion

            #region Vertical Rotation
            followTransform.transform.rotation *= Quaternion.AngleAxis(delta.y * rotationPower, Vector3.right);

            var angles = followTransform.transform.localEulerAngles;
            angles.z = 0;

            var angle = followTransform.transform.localEulerAngles.x;

            // Clamp the Up/Down Rotation
            if (angle > 180 && angle < 340)
                angles.x = 340;
            else if (angle < 180 && angle > 40)
                angles.x = 40;

            followTransform.transform.localEulerAngles = angles;
            #endregion

            if (Quaternion.Angle(followTransform.transform.rotation, nextRotation) > 0.1f)
            {
                nextRotation = Quaternion.Lerp(followTransform.transform.rotation, nextRotation, Time.deltaTime * rotationLerp);
            }
            
            if (moveComposite.x == 0 && moveComposite.y == 0)
            {
                nextPosition = transform.position;

                if (aimValue == 1)
                {
                    // Set the player rotation based on the look transform
                    transform.rotation = Quaternion.Euler(0, followTransform.transform.rotation.eulerAngles.y, 0);
                    // Reset the y rotaton of the look transform
                    followTransform.transform.localEulerAngles = new Vector3(angles.x, 0, 0);
                }

                return;
            }
            float camMoveSpeed = camSpeed * 0.01f;
            Vector3 position = (transform.forward * moveComposite.y * camMoveSpeed) + (transform.right * moveComposite.x * camMoveSpeed);
            nextPosition = transform.position + position;

            // Set the player rotation based on the look transform
            transform.rotation = Quaternion.Euler(0, followTransform.transform.rotation.eulerAngles.y, 0);
            // Reset the y rotaton of the look transform
            followTransform.transform.localEulerAngles = new Vector3(angles.x, 0, 0);
        }
    }
}