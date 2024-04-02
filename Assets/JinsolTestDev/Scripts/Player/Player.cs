using Cinemachine;
using UnityEngine;
using UnityEngine.Playables;

/* public enum EPlayerState // 혹시라도 변수와 enum의 이름이 겹치면 안되므로 E 추가
{
    Passerby, // 지나가던 사람 (레벨0)
    Visitor, // 일시적으로 머무를 사람 (레벨1)
    Attendee // 참관객: 관심 있게 내부를 둘러볼 사람 (레벨2)
    } */

namespace Jinsol
{
    public class Player : MonoBehaviour
    {
        /*    #region 지우지말기 - 마우스 lookat 관련
            private Vector3 pressPoint;
            private float sceneWidth;
            private float sceneHeight;
            private Quaternion startRotation;
            //public float lookRotationDamper = 10f; 조금만 더 부드럽게 돌려주는 방법 찾으려다 포기한 부분
            #endregion*/

        #region 퀘스트 관련
        [SerializeField] private int startingLevel = 0; // 기본값: 레벨0 (지나가던 사람)
        public int currentLevel; // 현재레벨. 메모: 이걸 지금 수정한다고 다른 데 반영이 안되는데...?
        #endregion

        #region 이벤트 컷씬 연출용 참조 영역
        //public EPlayerState currentPlayerState; // 플레이어의 게임플레이 상태 확인
        [SerializeField] private PlayableDirector director; // 상호작용으로 컷씬 불러오기 위해 인스펙터창에서 디렉터 참조
        #endregion

        #region UI 참조용
        public GameObject exit;
        public GameObject interact;
        #endregion

        #region 카메라 시점 변환용
        public CinemachineVirtualCamera mainCam; // 현재 주도권을 가진 카메라
        #endregion

        // 비디오 플레이어 볼륨 조절용
        [HideInInspector] public bool inLobby = true;

        private void Awake()
        {
            //sceneWidth = Screen.width;
            //currentPlayerState = EPlayerState.Passerby;
            //Cursor.lockState = CursorLockMode.Locked; // 잠금 걸어놓고 돌리면 카메라 움직임이 이상해짐
            currentLevel = startingLevel;
        }

        private void Start()
        {
            mainCam.MoveToTopOfPrioritySubqueue();
            GameEventsManager.instance.playerEvents.PlayerLevelChange(currentLevel);
        }

        void Update()
        {
            /*        #region 마우스 드래그해서 둘러보기
                    if (Input.GetMouseButtonDown(0))
                    {
                        pressPoint = Input.mousePosition;
                        startRotation = mainCam.transform.rotation;
                    }
                    else if (Input.GetMouseButton(0))
                    {
                        float CurrentDistanceBetweenMousePositions = (Input.mousePosition - pressPoint).x;
                        //float CurrentYDistanceBetweenMousePositions = (Input.mousePosition - pressPoint).y;
                        transform.rotation = startRotation * Quaternion.Euler((CurrentDistanceBetweenMousePositions / sceneWidth) * 180 * Vector3.up);
                        //mainCam.transform.rotation = Quaternion.Euler(new Vector3(CurrentXDistanceBetweenMousePositions, 0f, 0f));
                    }
                    #endregion*/
        }

        private void OnTriggerEnter(Collider other)
        {
            #region 이벤트 컷씬 발생
            if (other.gameObject.tag == "Event")
                director.Play();

            if (other.gameObject.tag == "Event_Dialogue")
            {
                // 3D 오브젝트 버튼 만든 뒤로 일단 필요없어진 부분
            }
            #endregion

            #region 회장 밖으로 나갔을 때 발생
            if (other.gameObject.tag == "Exit")
            {
                exit.SetActive(true);
            }
            #endregion
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Event_Dialogue")
            {
                // 3D 오브젝트 버튼 만든 뒤로 일단 필요없어진 부분
            }

            if (other.gameObject.tag == "Exit")
            {
                exit.SetActive(false);
            }
        }

        /*     #region 상호작용 이벤트용
            public void PlayerInteract()
            {
                var layermask0 = 1 << 0;
                var layermask3 = 1 << 3;
                var finalmask = layermask0 | layermask3;

                RaycastHit hit;
                Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

                if (Physics.Raycast(ray, out hit, 15, finalmask))
                {
                    Interact interactScript = hit.transform.GetComponent<Interact>();
                    if (interactScript) interactScript.CallInteract(this);
                }            
            }
            #endregion */

    }
}