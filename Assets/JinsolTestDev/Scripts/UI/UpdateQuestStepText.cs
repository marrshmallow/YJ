using UnityEngine;
using TMPro;

namespace Jinsol
{
    /// <summary>
    /// 퀘스트 진행도 업데이트 UI
    /// 
    /// - 정진솔
    /// </summary>

    public class UpdateQuestStepText : MonoBehaviour
    {
        private TextMeshProUGUI m_TextMeshPro;
        [SerializeField] private CollectTokensQuestStep questStep;

        private void OnEnable()
        {
            m_TextMeshPro = (TextMeshProUGUI)GetComponent("TextMeshProUGUI");
            questStep = FindObjectOfType<CollectTokensQuestStep>();
            m_TextMeshPro.text = "<sprite=1> 모든 부스의 NPC와 대화: " + questStep.tokensCollected + "/" + questStep.tokensRequired;
        }

        private void Update()
        {
            m_TextMeshPro.text = "<sprite=1> 모든 부스의 NPC와 대화: " + questStep.tokensCollected + "/" + questStep.tokensRequired;
        }
    }
}
