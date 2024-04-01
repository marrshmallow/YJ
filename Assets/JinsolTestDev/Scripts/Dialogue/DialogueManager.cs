using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.Playables;

/// <summary>
/// 대화를 시작하고 UI에 표시시켜주며 컷씬을 진행시키는 대화 매니저
/// - 정진솔
/// </summary>

namespace Jinsol
{
    public class DialogueManager : MonoBehaviour
    {
        private Queue<string> sentences;
        public TextMeshProUGUI showName;
        public TextMeshProUGUI showDialogue;
        public DialogueButton dButton; // 대화창 계속 버튼
        [SerializeField] private PlayableDirector director;

        private void Start()
        {
            sentences = new Queue<string>();
            dButton.standby = false;
        }

        /* public void StartDialogue(Dialogue dialogue)
        {
            sentences.Clear();
            foreach (string sentence in dialogue.sentences)
            {
                sentences.Enqueue(sentence); // 첫번째 문장을 큐에 추가
            }

            DisplayNextSentence();
        } */

        public void DisplayNextSentence() // Continue 버튼
        {
            if (sentences.Count == 0)
            {
                dButton.standby = true;
                return;
            }

            string sentence = sentences.Dequeue(); // 대기열에서 제거
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
        }

        private IEnumerator TypeSentence(string sentence)
        {
            showDialogue.text = "";
            foreach (char letter in sentence.ToCharArray())
            {
                showDialogue.text += letter;
                yield return null;
            }
        }
    }
}
