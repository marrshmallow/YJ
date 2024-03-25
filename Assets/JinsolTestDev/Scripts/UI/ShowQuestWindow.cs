using UnityEngine;
using UnityEngine.UI;

public class ShowQuestWindow : MonoBehaviour
{
    private Button _button;
    [SerializeField] private GameObject questInfoWindow; // �� ������ �����տ� ������ �ȵ�...

    private void Awake()
    {
        _button = (Button)GetComponent("Button");
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(SeeQuestInfo);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(SeeQuestInfo);
    }

    private void SeeQuestInfo()
    {
        // ���� ����ȭ�� ������
        questInfoWindow.SetActive(true);
    }
}
