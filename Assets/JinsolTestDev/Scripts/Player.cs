using UnityEngine;
using UnityEngine.Playables;

public class Player : MonoBehaviour
{
    #region 지우지말기 - 마우스 lookat 관련
    private Vector3 pressPoint;
    private float sceneWidth;
    private Quaternion startRotation;
    //public float lookRotationDamper = 10f; 조금만 더 부드럽게 돌려주는 방법 찾으려다 포기한 부분
    #endregion

    #region 이벤트 컷씬 연출용 참조 영역
    [SerializeField] private PlayableDirector director; // 상호작용으로 컷씬 불러오기 위해 인스펙터창에서 디렉터 참조
    #endregion

    private void Awake()
    {
        sceneWidth = Screen.width;
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
            //transform.rotation = startRotation * Quaternion.Slerp(CurrentDistanceBetweenMousePositions / sceneWidth * 180 * Vector3.up, lookRotationDamper);
        }
        #endregion
    }

    private void OnTriggerEnter(Collider other)
    {
        #region 이벤트 컷씬 발생
        if (other.gameObject.tag == "Event")
            director.Play();
        #endregion

        #region 회장 밖으로 나갔을 때 발생
        if (other.gameObject.tag == "GetOut")
        {
            // UI 창을 띄워서 나갈 것인지 질문
        }
        #endregion
    }
}
