using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestInfoSO", menuName = "ScriptableObjects/QuestInfoSO")]
public class QuestInfoSO : ScriptableObject
{
    // 퀘스트 아이디 만들 때에는 해당하는 Scriptable Object와 동일한 이름으로 만들기
    // field: 를 덧붙이면 확실하게 보이게 된다 ㅇㅁㅇ!!!!!!!! 헐 (안붙이면안보임)
    [field: SerializeField] public string id { get; private set;} // 퀘스트 고유 아이디 설정, 인스펙터에서 확인 가능.

    [Header("기본정보")]
    public string displayName;
    [Header("달성단계")]
    public GameObject[] questStepPrefabs;
    [Header("퀘스트 발생 조건")]
    public int levelRequired; // 플레이어 레벨이 맞지 않은 퀘스트는 수주 불가
    public QuestInfoSO[] questPrerequisites;

    [Header("보상")]
    public int tokenReward; // 임시: 토큰을 받을 때마다 숫자가 올라간다. 이거 근데 수정ㅁㄴ이ㅏㅓㅁㄹ

    // Scriptable Object와 동일한 이름의 id가 생성되기를 보장하는 기능
    private void OnValidate()
    {
        #if UNITY_EDITOR
        id = this.name;
        UnityEditor.EditorUtility.SetDirty(this); // 플레이 중에 스크립터블 오브젝트가 변하더라도 바로바로 저장 가능한 Dirty 플래그 부여. 주의: Dirty 상태는 취소 불가능
        #endif
    }
}
