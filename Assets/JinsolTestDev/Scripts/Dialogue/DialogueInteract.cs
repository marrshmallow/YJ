using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using TMPro;
using System.Linq;

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

    [SerializeField] private PlayableDirector director;

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
        director.playableGraph.GetRootPlayable(0).SetSpeed(1);
        Debug.Log(director.playableGraph.GetRootPlayable(0).GetSpeed());
        optionSelected = true;
        Debug.Log("Selected Option: " + selectedOption);
        startDialogueObject = selectedOption;
        StartDialogue (selectedOption);
        Debug.Log("Follow-up dialogue: " + startDialogueObject);
    }

    private IEnumerator DisplayDialogue(DialogueSO dialogueObject)
    {
        yield return null;
        List<GameObject> instantiatedButtons = new List<GameObject>();

        interactUI.SetActive(true);

        foreach (var dialogue in startDialogueObject.dialogueSegments)
        {
            textBubble.SetActive(true);
            dialogueText.text = dialogue.dialogueText;
            if (dialogue.nextCutscene != null)
                director.Play(dialogue.nextCutscene); // 지정된 타임라인 에셋이 있다면 재생
            
            if (dialogue.dialogueChoices.Count == 0)
            {
                yield return new WaitForSeconds (dialogue.displayTime);
            }
            else
            {
                director.playableGraph.GetRootPlayable(0).SetSpeed(0); // 선택지 표시 중에 타임라인 중지 (그러나 카메라는 계속 돌아감)
                yield return new WaitForSeconds(dialogue.displayTime);
                dialogueOptionsContainer.SetActive(true); // 선택지가 표시될 게임오브젝트 부모 활성화

                // 선택지 표시
                foreach (var option in dialogue.dialogueChoices)
                {
                    GameObject newButton = Instantiate(dialogueOptionsButtonPrefab, dialogueOptionsParent);
                    instantiatedButtons.Add(newButton);
                    newButton.GetComponent<UIDialogueOption>().Setup(this, option.followupDialogue, option.dialogueChoice);
                    textBubble.SetActive(false);
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
