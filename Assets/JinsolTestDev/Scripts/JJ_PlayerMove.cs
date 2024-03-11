using UnityEngine;
using UnityEngine.TextCore.Text;

#region 코딩 참고사항
// " 방향(Vector) * 이동속도(Scalar) * 델타타임(Scalar) " 를
// " 이동속도(Scalar) * 델타타임(Scalar) * 방향(Vector) " 로 바꿔주면 에러가 안남  
// Scalar끼리의 연산이 Vector의 연산보다 더 빠르기 때문에
// Vector* Scalar * Scalar 와 같은 공식은 벡터와 두 번 곱해줘야 하지만
// Scalar * Scalar * Vector와 같은 공식은 스칼라와 한 번 곱하고 벡터를 한 번 곱하기 때문에
// 연산이 더 빨라짐 (출처: https://coding-goblin.tistory.com/39)
#endregion

public class JJ_PlayerMove : MonoBehaviour
{
    private CharacterController charController;
    private Vector3 playerVelocity;
    public float moveSpeed = 5f; //캐릭터 이동 속도
    public float RotateSpeed = 30f; //캐릭터 회전 속도
    public float jumpHeight = 2f;
    public float feetHeight = 1f;
    public float checkHeight = 0.2f;
    [SerializeField]
    private bool isGrounded;
    private float gravity;
    private readonly float upSpeed = 1f; //캐릭터 이동 속도 증가량
    private float SceneWidth;
    private Vector3 PressPoint;
    private Quaternion StartRotation;
    private Vector3 movement;
    private Vector3 moveDirection;

    void Awake()
    {
        charController = (CharacterController)GetComponent("CharacterController");
        gravity = -Physics.gravity.y;
        SceneWidth = Screen.width;
        charController.minMoveDistance = 0.7f; // 캐릭터가 70cm 이상 움직일 때만 움직이게끔 제한 (인스펙터창에서 Scale을 1 이외의 값으로 바꾸지 말것)
        isGrounded = charController.isGrounded;
    }

    void Start()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        movement = moveDirection * moveSpeed;
    }

    void Update()
    {
        if (isGrounded && playerVelocity.y < 0) // 플레이어가 지면에 닿아있는 상태이고 플레이어가 밑으로 압력을 받고 있으면
        {
            playerVelocity.y = 0f; // 플레이어의 속도 벡터 y값이 0 이하로 내려가는 일 없게
        }
        // https://docs.unity3d.com/ScriptReference/CharacterController.Move.html
        // https://velog.io/@ssu_hyun/Unity-Unity-%ED%94%84%EB%A1%9C%EC%A0%9D%ED%8A%B8-git-add-%EC%8B%A4%ED%8C%A8
        // ###### THIS IS WHERE I LEFT OFF #####
        // if (Input.GetKey(KeyCode.A))
        // {
        //     transform.Translate(moveSpeed * Time.deltaTime * Vector3.left);
        // }

        // if (Input.GetKey(KeyCode.D))
        // {
        //     transform.Translate(moveSpeed * Time.deltaTime * Vector3.right);
        // }

        // if (Input.GetKey(KeyCode.W))
        // {
        //     // gameObject.transform.position = gameObject.transform.position + Vector3.forward;
        //     // W를 누르고 있는 동안은 물체를 현재 위치에서 (0, 0, 1)만큼 더해준 곳으로 옮긴다. 특징: 성능이 좋은 컴퓨터일 수록 빠르게 이동함
        //     // Time.deltaTime = 지난 업데이트와 지금 업데이트 사이의 시간을 곱해주는 것
        //     // 시간에 걸쳐서 이동. 모든 성능의 컴퓨터에서 동일한 속도로 이동
        //     // 10f 등, 속도에서 f를 붙여주는 건 소수점도 사용하겠다는 이야기. 정밀한 조절 가능
        //     transform.Translate(moveSpeed * Time.deltaTime * Vector3.forward); // gameObject.transform.position = gameObject.transform.position + Vector3.left * moveSpeed * Time.deltaTime;의 짧은 버전

        //     //gameObject.transform.position = gameObject.transform.position + gameObject.transform.forward * moveSpeed * Time.deltaTime;
        //     // forward: 월드 기준 앞 (Global)
        //     // gameObject.transform.forward: 물체의 앞 (Local)
        // }

        // if (Input.GetKey(KeyCode.S))
        // {
        //     transform.Translate(moveSpeed * Time.deltaTime * Vector3.back);
        // }

        if (Input.GetMouseButtonDown(0))
        {
            PressPoint = Input.mousePosition;
            StartRotation = transform.rotation;
        }
        // ***** 확인 바람: else if는 간혹 안 되는 경우가 있음 (한꺼번에 동시에 입력했을 때) >>>> 되도록이면 쓰지 않는 방향으로
        // https://www.youtube.com/watch?v=V4AJWjvOFBw 코드 설명 다시 들어보기
        else if (Input.GetMouseButton(0))
        {
            float CurrentDistanceBetweenMousePositions = (Input.mousePosition - PressPoint).x;
            transform.rotation = StartRotation * Quaternion.Euler((CurrentDistanceBetweenMousePositions / SceneWidth) * 180 * Vector3.up);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            // P를 누를 때 마다 속도를 조금씩 올려주는 기능
            Debug.Log("스피드업");
            // moveSpeed = moveSpeed + upSpeed; 코드가 아래와 같이 수정됨
            moveSpeed += upSpeed;
        }
            
        // 캐릭터의 점프 처리
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            float jumpSpeed = Mathf.Sqrt(2 * Mathf.Abs(gravity) * jumpHeight);

            //rb.velocity = movement + Vector3.up * jumpSpeed;

            isGrounded = false;
        }

        //rb.velocity = movement + Vector3.up * rb.velocity.y;

        Vector3 feetPos = transform.position + Vector3.down * feetHeight;
        Color color = Color.red;
        if (charController.isGrounded) color = Color.green;
        Debug.DrawLine(feetPos, feetPos + Vector3.down * checkHeight, color);

    }
}