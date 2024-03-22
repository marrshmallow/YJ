using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
