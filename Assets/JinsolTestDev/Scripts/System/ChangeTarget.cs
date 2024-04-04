using UnityEngine;
using Cinemachine;

    /// <summary>
    /// �÷��̾� ���� npc�� �ٶ󺸴� ī�޶��� Ÿ�� ����
    /// 
    /// - ������
    /// </summary>

namespace Jinsol
{
    public class ChangeTarget : MonoBehaviour
    {
        private CinemachineVirtualCamera myCamera;
        [SerializeField] private Transform target;

        private void Awake()
        {
            myCamera = (CinemachineVirtualCamera)GetComponent("CinemachineVirtualCamera");
        }

        public void ChangeCameraTarget(Transform target)
        {
            myCamera.Follow = target;
            myCamera.LookAt = target;
        }
    }
}
