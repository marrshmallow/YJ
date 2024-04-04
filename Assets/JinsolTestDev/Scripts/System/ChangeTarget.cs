using UnityEngine;
using Cinemachine;

    /// <summary>
    /// 플레이어 시점 npc를 바라보는 카메라의 타겟 설정
    /// 
    /// - 정진솔
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
