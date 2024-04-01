using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using TMPro;

/// <summary>
/// 대화용 스크립터블 오브젝트를 불러와 UI를 활성화하여 대화 텍스트를 표시하고,
/// 연결된 플레이어블 애셋이 있으면 함께 재생시켜주는 기능
/// - 정진솔
/// </summary>

namespace Jinsol
{
    public class DialogueInteract : MonoBehaviour
    {
        #region 대화 표시 기능
        [SerializeField] DialogueSO startDialogueObject; // 불러올 대화 데이터 (스크립터블 오브젝트)
        [SerializeField] TextMeshProUGUI dialogueText; // 텍스트가 표시될 컴포넌트 (TextMeshPro)
        [SerializeField] TextMeshProUGUI speakerNameText; // 화자의 이름이 표시될 텍스트 컴포넌트 (TMP)
        private bool optionSelected = false; // 플레이어가 선택지에서 무언가를 골랐다면 true. 선택지 UI에 직접 OnClick 이벤트를 생성하여 적용.
        #endregion

        #region UI 생성
        [SerializeField] GameObject interactUI; // 선택지 UI (전체)
        [SerializeField] GameObject textBubble; // 말풍선 게임오브젝트
        [SerializeField] GameObject dialogueOptionsContainer; // 선택지 Prefab이 생성될 부모 오브젝트
        [SerializeField] Transform dialogueOptionsParent; // 그 부모 오브젝트의 위치
        [SerializeField] GameObject dialogueOptionsButtonPrefab; // 생성될 선택지 Prefab
        #endregion

        [SerializeField] private PlayableDirector director; // 현재 씬의 플레이어블 디렉터

        public void StartDialogue()
        {
            StartCoroutine(DisplayDialogue(startDialogueObject));
        }

        public void StartDialogue(DialogueSO dialogueObject) // 직접 대화를 출력할 스크립터블 오브젝트를 지정해서 실행시킬 수 있는 기능
        {
            StartCoroutine(DisplayDialogue(dialogueObject));
        }

        public void OptionSelected(DialogueSO selectedOption) // 각 선택지 버튼으로 직접 실행 가능한 기능
        {
            director.playableGraph.GetRootPlayable(0).SetSpeed(1); // 타임라인의 시간을 다시 흐르게 하고
            optionSelected = true; // 플레이어가 선택했음을 bool값으로 기록
            startDialogueObject = selectedOption; // 선택한 분기에 등록된 대화 데이터를 현재 대화 데이터로 불러오기
            StartDialogue(selectedOption); // 불러온 새 대화 데이터 출력 시작
        }

        private IEnumerator DisplayDialogue(DialogueSO dialogueObject)
        {
            yield return null;
            List<GameObject> instantiatedButtons = new List<GameObject>(); // 선택지를 생성할 새 리스트를 작성

            interactUI.SetActive(true); // 선택지 UI를 활성화

            foreach (var dialogue in startDialogueObject.dialogueSegments) // 대화 데이터에 담긴 정보를 순차적으로 표시
            {
                textBubble.SetActive(true); // 대화가 시작되면 대화창 UI를 표시
                speakerNameText.text = dialogue.speakerName; // 말하는 사람의 이름을 표시
                dialogueText.text = dialogue.dialogueText; // 대화 내용을 표시
                if (dialogue.nextCutscene != null) // 이 대화가 표시될 때 동시에 재생해줘야 할 타임라인 에셋이 있다면 재생
                    director.Play(dialogue.nextCutscene);

                if (dialogue.dialogueChoices.Count == 0) // 선택지가 없는 대화 데이터일 경우 일정 시간 대기
                {
                    yield return new WaitForSeconds(dialogue.displayTime);
                }
                else // 선택지가 있는 경우
                {
                    director.playableGraph.GetRootPlayable(0).SetSpeed(1); // 혹시 타임라인이 일시 중지 되었을 때를 대비해 타임라인의 시간을 원래대로 돌려놓고
                    yield return new WaitForSeconds(dialogue.displayTime); // 잠시 대기한 뒤
                    dialogueOptionsContainer.SetActive(true); // 선택지가 표시될 게임오브젝트 부모 활성화

                    // 스크립터블 오브젝트에서 확인된 선택지 수 만큼
                    foreach (var option in dialogue.dialogueChoices)
                    {
                        GameObject newButton = Instantiate(dialogueOptionsButtonPrefab, dialogueOptionsParent); // 새로운 선택지 버튼을 지정된 위치에 생성
                        instantiatedButtons.Add(newButton); // 각 버튼마다 새로운 OnClick 이벤트 지정
                        newButton.GetComponent<UIDialogueOption>().Setup(this, option.followupDialogue, option.dialogueChoice); // 새 버튼의 OnClick 이벤트에, 선택지와 그에 대한 답변으로 표시될 내용을 연결해줌 (선택지용 스크립트에서 미리 지정됨)
                        textBubble.SetActive(false); // 선택지가 준비되었다면 대화창 표시 끄기
                    }
                    director.playableGraph.GetRootPlayable(0).SetSpeed(0); // 타임라인의 재생 속도를 0으로 설정함으로서 일시 중지한 것과 동일한 효과. 그러나 카메라는 계속 돌아갈 수 있다.

                    while (!optionSelected) // 플레이어가 선택하지 않고 있는 경우 계속 대기
                    {
                        yield return null;
                    }
                    break; // 선택했다면 계속해서 대화 데이터를 표시
                }
            }

            dialogueOptionsContainer.SetActive(false); // 대화가 끝났을 때 UI 비활성화
            textBubble.SetActive(false);
            interactUI.SetActive(false);
            optionSelected = false; // 선택지 결정중 상태를 초기값(false)으로 설정

            instantiatedButtons.ForEach(x => Destroy(x)); // 생성된 선택지의 OnClick 이벤트를 모두 삭제
        }
    }
}
