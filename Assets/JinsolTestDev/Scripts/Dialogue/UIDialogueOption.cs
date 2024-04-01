using UnityEngine;
using TMPro;

/// <summary>
/// ��ȭ�߿� �������� �ִ� ���, ������ Prefab�� �ʿ��� ������Ʈ�� ������� �ִ� ���
/// by ������
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
