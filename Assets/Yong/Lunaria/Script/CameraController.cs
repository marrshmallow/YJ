using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //SerializeField 공개변환
    public Transform follow_target;
    public Vector3 followOffset = new Vector3(0f, 6f, -10f);
    public Vector3 lookOffset = new Vector3(0f, 3f, 0f);

    public float rotation_speed = 2; //카메라가 회전하는 회전 속도.

    [SerializeField]
    private float yAngle; //카메라가 주인공을 바라보는 각도
    [SerializeField]
    private float xAngle;


    [SerializeField] float min_v_angle = 0; //Vertical x축 회전 최소 수직 각도를 제어 
    [SerializeField] float max_v_angle = 90; //Vertical x 축 회전 제어

    private Vector2 rotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetMouseButton(1)) // 마우스 오른쪽 버튼 누르고 있는 동안
        {
            //CameraMove();
        }
        else
        { 
            //원래대로
        }

        FollowTarget(); //카메라 플레이어 따라가는 기능, 카메라 기본기능


        //항상 플레이어를 따라는 가야되죠..
        //회전하는것만 그때

    }


   void CameraMove()
   {
      float rotationSpeed = rotation_speed;

    if (Input.GetKey(KeyCode.LeftShift))
    {
        rotationSpeed *= 2f;
    }


        rotation += new Vector2(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X")) * rotation_speed;
        rotation.x = Mathf.Clamp(rotation.x, min_v_angle, max_v_angle);

        //새 벡터도 생성하고 x축에 대한 마우스 y 입력 값과 y 축에 대한 마우스 x 입력 값을 전달한 
        //다음 여기에 회전속도를 곱합니다. X축에서 카메라 회전을 제한합니다.
        //수학 클래스의 클램프 메서드를 사용하여 최소 및 최대 수직 각도를 전달

        // 임시 변수, 회전 변수의 변환된 값을 쿼터니언으로 저장하기 위한 대상 회전 다음으로 카메라의
        // 각도를 설정합니다. 위치를 따라가는 대상의 위치에서 대상을 뺀 회전에 새로운 벡터 3을 곱하고 
        // 거리 변수가 z축에 전달됩니다.

        // 카메라의 회전을 대상 회전 변수와 동일하게 설정합니다.


        var target_rotation = Quaternion.Euler(rotation);


        transform.position = follow_target.position - target_rotation * new Vector3(0f, -8f, 0);
        transform.rotation = target_rotation;

   }

    void FollowTarget()
    {
        if (Input.GetMouseButton(1))
        {
            yAngle += Input.GetAxis("Mouse X") * rotation_speed;
        }
        else
        {
            //yAngle = follow_target.eulerAngles.y;
           
        }
        
        // 카메라 높이 오프셋 설정
        // 주인공 기준으로 카메라 followOffset을 적용하려면 followOffset 앞에 주인공.rotation을 곱하면 된다.
        transform.position = follow_target.position + Quaternion.Euler(0, yAngle, 0) * followOffset;

        //각을 표현하는 방법은 2가지
        //1. 쿼터니언 - 복잡한 계산용, 
        //2. 오일러각(vector3) - 단순한 계산, 표기용

        transform.LookAt(follow_target.position + lookOffset);
    }

}
