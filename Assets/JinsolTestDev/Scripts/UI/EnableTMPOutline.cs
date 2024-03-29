using UnityEngine;
using TMPro;

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

    private void EnableOutline()
    {
        m_TextMeshProUGUI.fontMaterial.SetFloat(ShaderUtilities.ID_OutlineSoftness, 0.16f);
        m_TextMeshProUGUI.fontMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, 0.1f);
        m_TextMeshProUGUI.fontMaterial.SetFloat(ShaderUtilities.ID_OutlineWidth, 0.08f);
    }
}
