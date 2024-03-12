using UnityEngine;

public class Player : MonoBehaviour
{
    #region 지우지말기 - 마우스 lookat 관련
    private Vector3 pressPoint;
    private float sceneWidth;
    private Quaternion startRotation;
    //public float lookRotationDamper = 10f; 조금만 더 부드럽게 돌려주는 방법 찾으려다 포기한 부분
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
}
