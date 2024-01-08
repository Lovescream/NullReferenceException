using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public Slider musicVolumeSlider;
    public Slider effectsVolumeSlider;
    public Button musicToggleButton;
    public Button effectsToggleButton;

    void Start()
    {// 초기 볼륨 설정
        musicVolumeSlider.value = 0.5f;
        effectsVolumeSlider.value = 0.5f;
        //버트 이벤트 리스너 추가
        musicToggleButton.onClick.AddListener(SoundManager.Instance.ToggleMusic);
        effectsToggleButton.onClick.AddListener(SoundManager.Instance.ToggleEffects);
        // 슬라이더 이벤트 리스너 추가
        musicVolumeSlider.onValueChanged.AddListener(HandleMusicVolumeChanged);
        effectsVolumeSlider.onValueChanged.AddListener(HandleEffectsVolumeChanged);
    }

    private void HandleMusicVolumeChanged(float volume)
    {
        // 배경음악 볼륨 조절
        SoundManager.Instance.SetMusicVolume(volume);
    }

    private void HandleEffectsVolumeChanged(float volume)
    {
        // 효과음 볼륨 조절
        SoundManager.Instance.SetEffectVolume(volume);
    }
}
