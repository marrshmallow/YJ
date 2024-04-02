using UnityEngine;
using TMPro;

/// <summary>
/// 대화중에 선택지가 있는 경우, 생성된 Prefab에 필요한 컴포넌트를 연결시켜 주는 기능
/// by 정진솔
/// </summary>

namespace Jinsol
{
    public class UIDialogueOption : MonoBehaviour
    {
        DialogueInteract dialogueInteract;
        DialogueSO dialogueObject;
        [SerializeField] TextMeshProUGUI dialogueText;

        public void Setup(DialogueInteract _dialogueInteract, DialogueSO _dialogueObject, string _dialogueText)
        {
            dialogueInteract = _dialogueInteract;
            dialogueObject = _dialogueObject;
            dialogueText.text = _dialogueText;
        }

        public void SelectOption()
        {
            dialogueInteract.OptionSelected(dialogueObject);
        }
    }
}
