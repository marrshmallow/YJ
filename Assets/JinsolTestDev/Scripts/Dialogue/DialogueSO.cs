using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueSO", menuName = "ScriptableObjects/DialogueSO")]
public class DialogueSO : ScriptableObject
{
    [Header("현재 대화")]
    public List<string> dialogueLines = new List<string>(); // 대사 목록. 외부에서 참조 가능하게

    [Header("추가 대화")]
    public DialogueSO endDialogue; // 대화 오브젝트 끝나면 다음 오브젝트로 넘겨줄 수 있게
}
