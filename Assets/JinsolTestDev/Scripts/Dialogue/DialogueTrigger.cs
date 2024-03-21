using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    //[SerializeField] private DialogueManager dManager;
    [SerializeField] private DialogueManager dManager; // 테스트용

    public void TriggerDialogue()
    {
        dManager.StartDialogue(dialogue);
    }
}
