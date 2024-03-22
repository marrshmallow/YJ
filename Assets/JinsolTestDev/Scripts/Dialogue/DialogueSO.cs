using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueSO", menuName = "ScriptableObjects/NPC_Dialogue")]
public class DialogueSO : ScriptableObject
{
    [Header("현재 대화")]
    public List<DialogueSegment> dialogueSegments = new List<DialogueSegment>(); // 대사 목록. 외부에서 참조 가능하게

    [Header("추가 대화")]
    public DialogueSO endDialogue; // 대화 오브젝트 끝나면 다음 오브젝트로 넘겨줄 수 있게
}

[System.Serializable]
public struct DialogueSegment
{
    public string dialogueText;
    public float displayTime; // 대화 표시 시간 << 버튼으로 바꿀 것
    public List<DialogueChoice> dialogueChoices;
}

[System.Serializable]
public struct DialogueChoice
{
    public string dialogueChoice;
    public DialogueSO followupDialogue;
}
