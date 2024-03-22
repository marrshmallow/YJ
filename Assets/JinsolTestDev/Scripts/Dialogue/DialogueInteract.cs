using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueInteract : MonoBehaviour
{
    #region 대화 표시 기능
    [SerializeField] DialogueSO startDialogueObject;
    [SerializeField] TextMeshProUGUI dialogueText;
    private bool optionSelected = false; // 선택을 아직 안했으면 true
    #endregion

    #region UI 생성
    [SerializeField] GameObject interactUI;
    [SerializeField] GameObject textBubble;
    [SerializeField] GameObject dialogueOptionsContainer;
    [SerializeField] Transform dialogueOptionsParent;
    [SerializeField] GameObject dialogueOptionsButtonPrefab;
    #endregion

    public void StartDialogue()
    {
        StartCoroutine(DisplayDialogue(startDialogueObject));
    }

    public void StartDialogue(DialogueSO dialogueObject)
    {
        StartCoroutine(DisplayDialogue(dialogueObject));
    }

    public void OptionSelected(DialogueSO selectedOption)
    {
        optionSelected = true;
        StartDialogue (selectedOption);
    }

    private IEnumerator DisplayDialogue(DialogueSO dialogueObject)
    {
        yield return null;
        Debug.Log("Starting Dialogue Chain");
        List<GameObject> instantiatedButtons = new List<GameObject>();

        interactUI.SetActive(true);
        Debug.Log("Dialogue Active");
        foreach (var dialogue in startDialogueObject.dialogueSegments)
        {
            dialogueText.text = dialogue.dialogueText;
            
            if (dialogue.dialogueChoices.Count == 0)
            {
                yield return new WaitForSeconds (dialogue.displayTime);
            }
            else
            {
                dialogueOptionsContainer.SetActive(true);
                foreach (var option in dialogue.dialogueChoices)
                {
                    GameObject newButton = Instantiate(dialogueOptionsButtonPrefab, dialogueOptionsParent);
                    instantiatedButtons.Add(newButton);
                    newButton.GetComponent<UIDialogueOption>().Setup(this, option.followupDialogue, option.dialogueChoice);
                }

                while (!optionSelected)
                {
                    yield return null;
                }
                break;
            }
        }

        dialogueOptionsContainer.SetActive(false);
        textBubble.SetActive(false);
        interactUI.SetActive(false);
        optionSelected = false;

        instantiatedButtons.ForEach (x => Destroy (x));
        Debug.Log("Ending Dialogue Chain");
    }
}
