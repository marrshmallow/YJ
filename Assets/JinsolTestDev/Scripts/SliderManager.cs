using UnityEngine;
using UnityEngine.UI;
using FMODUnity;
using FMOD.Studio;
using TMPro;

public class SliderManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI field = null;
    [SerializeField] private Slider slider = null;
    [SerializeField] private string busPath = "";
    private Bus bus;

    private void Start()
    {
        if (busPath != "")
        {
            bus = RuntimeManager.GetBus(busPath);
        }

        bus.getVolume(out float volume); // 현재 볼륨 가져오기
        slider.value = volume * slider.maxValue; // 슬라이더 최대값에 따라 수치 조정해서 볼륨값과 동기화
    }

    private void Update()
    {
        #region 볼륨 조절 시 슬라이더 수치에 맞게 숫자 표시
        UpdateSliderOutput();
        #endregion
    }

    public void UpdateSliderOutput()
    {
        if (field != null && slider != null)
        {
            field.text = slider.value.ToString("N0");
            bus.setVolume(slider.value / slider.maxValue);
        }
    }
}