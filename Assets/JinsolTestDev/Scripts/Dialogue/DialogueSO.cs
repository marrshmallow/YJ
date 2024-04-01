using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

/// <summary>
/// 대화 내용을 저장할 스크립터블 오브젝트
/// - 정진솔
/// </summary>

namespace Jinsol
{
    [CreateAssetMenu(fileName = "DialogueSO", menuName = "ScriptableObjects/NPC_Dialogue")]
    public class DialogueSO : ScriptableObject
    {
        [Header("현재 대화")]
        public List<DialogueSegment> dialogueSegments = new List<DialogueSegment>(); // 대사 목록. 외부에서 참조 가능.

        [Header("추가 대화")]
        public DialogueSO endDialogue; // 현재 대화가 끝난 뒤 이어지는 대화 (선택사항)
    }

    [System.Serializable]
    public struct DialogueSegment
    {
        public string speakerName; // 화자의 이름
        public string dialogueText; // 말하는 내용
        public float displayTime; // 대화 표시 시간 << 사실 버튼 누르기 전까지 계속 대기하는 걸로 바꿔야
        public PlayableAsset nextCutscene; // 현재 대화와 연동할 컷씬 (선택사항)
        public List<DialogueChoice> dialogueChoices; // 선택지 프리팹을 저장할 리스트 (선택사항)
    }

    [System.Serializable]
    public struct DialogueChoice
    {
        public string dialogueChoice; // 선택지 내용
        public DialogueSO followupDialogue; // 각 선택과 연결된 후속 대화
    }
}
