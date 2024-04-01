using UnityEngine;
using UnityEngine.UI;
using FMODUnity;
using FMOD.Studio;
using TMPro;

/// <summary>
/// 
/// 현재 볼륨을 조회해서 UI에 반영해주는 기능
/// UI를 통해서 각 볼륨을 조정 가능
/// 
/// - 정진솔 
/// </summary>

namespace Jinsol
{
    public class VolumeSlider : MonoBehaviour
    {
        private Bus bus; // 제어할 FMOD 사운드 요소 (e.g. 음악, 환경음, 플레이어 효과음, UI)
        [SerializeField] private string busPath = ""; // 해당 요소의 FMOD 버스 경로
        [SerializeField] private Slider slider = null; // 볼륨 슬라이더 UI
        [SerializeField] private TextMeshProUGUI field = null; // 볼륨값을 표시해 주는 부분

        private void Start()
        {
            if (busPath != "")
            {
                bus = RuntimeManager.GetBus(busPath);
            }

            bus.getVolume(out float volume); // 현재 볼륨 가져오기
            slider.value = volume * slider.maxValue; // 슬라이더 최대값에 따라 수치 조정해서 볼륨값과 동기화
        }

        private void Update() // 현재 볼륨에 맞게 UI 조절
        {
            #region 볼륨 조절 시 슬라이더 수치에 맞게 숫자 표시
            UpdateSliderOutput();
            #endregion
        }

        public void UpdateSliderOutput() // 슬라이더를 움직이면 실시간으로 볼륨 조절
        {
            if (field != null && slider != null)
            {
                field.text = slider.value.ToString("N0");
                bus.setVolume(slider.value / slider.maxValue);
            }
        }
    }
}