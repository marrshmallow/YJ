using UnityEngine;
using TMPro;

/// <summary>
/// 
/// UI에 표시되는 글씨의 디자인을 제어하는 스크립트
/// 
/// - 정진솔
/// </summary>

public class EnableTMPOutline : MonoBehaviour
{
    private TextMeshProUGUI m_TextMeshProUGUI;

    private void Awake()
    {
        m_TextMeshProUGUI = (TextMeshProUGUI)GetComponent("TextMeshProUGUI");
    }

    private void OnEnable()
    {
        EnableOutline();
    }

    private void EnableOutline() // 테두리를 더해준다
    {
        m_TextMeshProUGUI.fontMaterial.SetFloat(ShaderUtilities.ID_OutlineSoftness, 0.16f);
        m_TextMeshProUGUI.fontMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, 0.1f);
        m_TextMeshProUGUI.fontMaterial.SetFloat(ShaderUtilities.ID_OutlineWidth, 0.08f);
    }
}
