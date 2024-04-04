using UnityEngine;
using TMPro;

namespace Jinsol
{
    /// <summary>
    /// ����Ʈ ���൵ ������Ʈ UI
    /// 
    /// - ������
    /// </summary>

    public class UpdateQuestStepText : MonoBehaviour
    {
        private TextMeshProUGUI m_TextMeshPro;
        [SerializeField] private CollectTokensQuestStep questStep;

        private void OnEnable()
        {
            m_TextMeshPro = (TextMeshProUGUI)GetComponent("TextMeshProUGUI");
            questStep = FindObjectOfType<CollectTokensQuestStep>();
            m_TextMeshPro.text = "<sprite=1> ��� �ν��� NPC�� ��ȭ: " + questStep.tokensCollected + "/" + questStep.tokensRequired;
        }

        private void Update()
        {
            m_TextMeshPro.text = "<sprite=1> ��� �ν��� NPC�� ��ȭ: " + questStep.tokensCollected + "/" + questStep.tokensRequired;
        }
    }
}
