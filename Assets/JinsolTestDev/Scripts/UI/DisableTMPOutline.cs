using UnityEngine;
using TMPro;

/// <summary>
/// 
/// TMP 사용 시 텍스트를 기본형으로 되돌려 놓는 기능
/// 
/// - 정진솔
/// </summary>

namespace Jinsol
{
    public class DisableTMPOutline : MonoBehaviour
    {
        private TextMeshProUGUI m_TextMeshProUGUI;

        private void Awake()
        {
            m_TextMeshProUGUI = (TextMeshProUGUI)GetComponent("TextMeshProUGUI");
            DisableOutline();
        }

        private void DisableOutline()
        {
            m_TextMeshProUGUI.fontMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, 0.04f);
            m_TextMeshProUGUI.fontMaterial.SetFloat(ShaderUtilities.ID_OutlineSoftness, 0f);
            m_TextMeshProUGUI.fontMaterial.SetFloat(ShaderUtilities.ID_OutlineWidth, 0.01f);
        }
    }
}
