using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 여기에서 ProceedQuest 담당
// 추후에 다른 퀘스트가 생겨날 때를 대비한 기본 틀!
public abstract class QuestStep : MonoBehaviour
{
    private bool isCompleted = false;

    protected void CompleteQuestStep()
    {
        if (!isCompleted)
        {
            isCompleted = true;
            // 여기에 다음 퀘스트로 진행시켜주는 부분 넣기
            Destroy(this.gameObject); // 씬에 남아있으면 안되니까 없애주기
        }
    }
}
