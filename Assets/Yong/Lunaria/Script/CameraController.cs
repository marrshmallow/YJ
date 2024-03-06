using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //SerializeField ������ȯ
    public Transform follow_target;
    public Vector3 followOffset = new Vector3(0f, 6f, -10f);
    public Vector3 lookOffset = new Vector3(0f, 3f, 0f);

    public float rotation_speed = 2; //ī�޶� ȸ���ϴ� ȸ�� �ӵ�.

    [SerializeField]
    private float yAngle; //ī�޶� ���ΰ��� �ٶ󺸴� ����
    [SerializeField]
    private float xAngle;


    [SerializeField] float min_v_angle = 0; //Vertical x�� ȸ�� �ּ� ���� ������ ���� 
    [SerializeField] float max_v_angle = 90; //Vertical x �� ȸ�� ����

    private Vector2 rotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetMouseButton(1)) // ���콺 ������ ��ư ������ �ִ� ����
        {
            //CameraMove();
        }
        else
        { 
            //�������
        }

        FollowTarget(); //ī�޶� �÷��̾� ���󰡴� ���, ī�޶� �⺻���


        //�׻� �÷��̾ ����� ���ߵ���..
        //ȸ���ϴ°͸� �׶�

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

        //�� ���͵� �����ϰ� x�࿡ ���� ���콺 y �Է� ���� y �࿡ ���� ���콺 x �Է� ���� ������ 
        //���� ���⿡ ȸ���ӵ��� ���մϴ�. X�࿡�� ī�޶� ȸ���� �����մϴ�.
        //���� Ŭ������ Ŭ���� �޼��带 ����Ͽ� �ּ� �� �ִ� ���� ������ ����

        // �ӽ� ����, ȸ�� ������ ��ȯ�� ���� ���ʹϾ����� �����ϱ� ���� ��� ȸ�� �������� ī�޶���
        // ������ �����մϴ�. ��ġ�� ���󰡴� ����� ��ġ���� ����� �� ȸ���� ���ο� ���� 3�� ���ϰ� 
        // �Ÿ� ������ z�࿡ ���޵˴ϴ�.

        // ī�޶��� ȸ���� ��� ȸ�� ������ �����ϰ� �����մϴ�.


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
        
        // ī�޶� ���� ������ ����
        // ���ΰ� �������� ī�޶� followOffset�� �����Ϸ��� followOffset �տ� ���ΰ�.rotation�� ���ϸ� �ȴ�.
        transform.position = follow_target.position + Quaternion.Euler(0, yAngle, 0) * followOffset;

        //���� ǥ���ϴ� ����� 2����
        //1. ���ʹϾ� - ������ ����, 
        //2. ���Ϸ���(vector3) - �ܼ��� ���, ǥ���

        transform.LookAt(follow_target.position + lookOffset);
    }

}
