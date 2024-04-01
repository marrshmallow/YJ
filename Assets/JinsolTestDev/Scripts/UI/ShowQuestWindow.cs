using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// 퀘스트 정보 창을 표시해주는 기능 (미완성)
/// 
/// - 정진솔
/// </summary>

namespace Jinsol
{

    public class ShowQuestWindow : MonoBehaviour
    {
        private Button _button;
        [SerializeField] private GameObject questInfoWindow; // 이 정보가 프리팹에 저장이 안됨...

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
            // 내용 동기화한 다음에
            questInfoWindow.SetActive(true);
        }
    }
}
