using UnityEngine;

/// <summary>
/// 
/// 퀘스트 진행 상태에 따른 아이콘의 외관 및 활성화 여부를 결정하는 스크립트
/// 
/// - 정진솔
/// </summary>

namespace Jinsol
{
    public class QuestIcon : MonoBehaviour
    {
        [Header("Icons")]
        [SerializeField] private GameObject questIcon;
        [SerializeField] private Material material;

        // 현재 퀘스트포인트 하위의 퀘스트포인트 아이콘의 색깔이 진행 상황에 맞춰 덩달아 변하지 않게 하는 부분
        public bool isSubQuest = false; // 기본값은 false. 값 설정은 하위 퀘스트 포인트에 첨부되는 (e.g. Token.cs) 스크립트에서 실행

        /*    [SerializeField] private GameObject canStartIcon;
            [SerializeField] private GameObject cannotCompleteIcon;
            [SerializeField] private GameObject canCompleteIcon;*/

        private void Awake()
        {
            material = questIcon.GetComponent<SkinnedMeshRenderer>().material;
        }

        public void SetState(QuestState newState, bool startPoint, bool finishPoint)
        {
            /*        #region 일단 모든 아이콘을 비표시
                    canStartIcon.SetActive(false);
                    cannotCompleteIcon.SetActive(false);
                    canCompleteIcon.SetActive(false);
                    #endregion

                    #region 퀘스트 상태에 맞춰서 아이콘 표시
                    switch (newState)
                    {
                        case QuestState.REQUIREMENTS_NOT_MET:
                            break;
                        case QuestState.CAN_START:
                            if (startPoint)
                            {
                                canStartIcon.SetActive(true);
                            }
                            break;
                        case QuestState.IN_PROGRESS:
                        if (startPoint)
                            {
                                cannotCompleteIcon.SetActive(true);
                            }
                            break;
                        case QuestState.CAN_COMPLETE:
                        if (startPoint)
                            {
                                canCompleteIcon.SetActive(true);
                            }
                            break;
                        case QuestState.COMPLETED:
                            break;
                        default:
                            Debug.LogWarning("퀘스트 아이콘을 찾을 수 없습니다: " + newState);
                            break;
                    }
                    #endregion*/

            #region 일단 아이콘을 비표시
            questIcon.SetActive(false);
            #endregion

            #region 퀘스트 상태에 맞춰서 아이콘 상태/색상 변화
            if (isSubQuest)
                return;
            else
            {
                switch (newState)
                {
                    case QuestState.REQUIREMENTS_NOT_MET:
                        break;
                    case QuestState.CAN_START:
                        if (startPoint)
                        {
                            questIcon.SetActive(true);
                        }
                        break;
                    case QuestState.IN_PROGRESS:
                        if (startPoint)
                        {
                            material.color = new Color(204f / 255f, 0f, 28f / 255f, 1f); // 퀘스트 진행중일 때 붉게 표시
                            questIcon.SetActive(true);
                        }
                        break;
                    case QuestState.CAN_COMPLETE:
                        if (startPoint)
                        {
                            material.color = new Color(0f, 168f / 255f, 102f / 255f, 1f); // 퀘스트 완료 가능할 때 녹색으로 표시
                            questIcon.SetActive(true);
                        }
                        break;
                    case QuestState.COMPLETED:
                        break;
                    default:
                        Debug.LogWarning("퀘스트 아이콘을 찾을 수 없습니다: " + newState);
                        break;
                }
            }
            #endregion
        }

        /*    public void OnMouseUpAsButton()
            {
                Debug.Log("Clicked!");
            }*/
    }
}
