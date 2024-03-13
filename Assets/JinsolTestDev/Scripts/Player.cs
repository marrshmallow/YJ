using System.Net.Sockets;
using UnityEngine;
using UnityEngine.Playables;

public enum PlayerState
{
    Passerby, // 지나가던 사람
    Visitor, // 일시적으로 머무를 사람
    Attendee // 참관객: 관심 있게 내부를 둘러볼 사람
    }
public class Player : MonoBehaviour
{
    #region 지우지말기 - 마우스 lookat 관련
    private Vector3 pressPoint;
    private float sceneWidth;
    private Quaternion startRotation;
    //public float lookRotationDamper = 10f; 조금만 더 부드럽게 돌려주는 방법 찾으려다 포기한 부분
    #endregion

    #region 이벤트 컷씬 연출용 참조 영역
    private PlayerState playerState; // 플레이어의 게임플레이 상태 확인
    [SerializeField] private PlayableDirector director; // 상호작용으로 컷씬 불러오기 위해 인스펙터창에서 디렉터 참조
    #endregion

    #region UI 참조용
    public GameObject exit;
    public GameObject interact;
    #endregion

    #region 발걸음 소리용
    public GameObject footstep;
    #endregion

    private void Awake()
    {
        sceneWidth = Screen.width;
        playerState = PlayerState.Passerby;
    }    
    void Update()
    {
        #region 확실하게 작동하는, LookAt 기능: 마우스 우클릭으로 둘러보기
        if (Input.GetMouseButtonDown(1))
        {
            pressPoint = Input.mousePosition;
            startRotation = transform.rotation;
        }
        else if (Input.GetMouseButton(1))
        {
            float CurrentDistanceBetweenMousePositions = (Input.mousePosition - pressPoint).x;
            transform.rotation = startRotation * Quaternion.Euler((CurrentDistanceBetweenMousePositions / sceneWidth) * 180 * Vector3.up);
        }
        #endregion

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            footstep.SetActive(true);
        else footstep.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        #region 이벤트 컷씬 발생
        if (other.gameObject.tag == "Event")
            director.Play();
            
        if (other.gameObject.tag == "Event_Dialogue")
        {
            interact.SetActive(true);
            //director.Play();
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
            interact.SetActive(false);
        }
    }
}
