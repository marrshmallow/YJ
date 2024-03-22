using System.Collections;
using UnityEngine;

public class DialogueInteract : MonoBehaviour
{
    [SerializeField] DialogueSO dialogueSO;
    public void StartDialogue()
    {
        StartCoroutine(DisplayDialogue());
    }

    private IEnumerator DisplayDialogue()
    {
        for (int i = 0; i < dialogueSO.dialogueLines.Count; i++ )
        {
            Debug.Log(dialogueSO.dialogueLines[i]);
            yield return new WaitForSeconds(1f);
        }
    }
}
