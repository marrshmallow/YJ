using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestIcon : MonoBehaviour
{
    [Header("Icons")]
    [SerializeField] private GameObject canStartIcon;
    [SerializeField] private GameObject cannotCompleteIcon;
    [SerializeField] private GameObject canCompleteIcon;

    public void SetState(QuestState newState, bool startPoint, bool finishPoint)
    {
        #region 일단 모든 아이콘을 비표시
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
        #endregion
    }
}
