using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public Slider musicVolumeSlider;
    public Slider effectsVolumeSlider;
    public Button musicToggleButton;
    public Button effectsToggleButton;

    void Start()
    {// �ʱ� ���� ����
        musicVolumeSlider.value = 0.5f;
        effectsVolumeSlider.value = 0.5f;
        //��Ʈ �̺�Ʈ ������ �߰�
        musicToggleButton.onClick.AddListener(SoundManager.Instance.ToggleMusic);
        effectsToggleButton.onClick.AddListener(SoundManager.Instance.ToggleEffects);
        // �����̴� �̺�Ʈ ������ �߰�
        musicVolumeSlider.onValueChanged.AddListener(HandleMusicVolumeChanged);
        effectsVolumeSlider.onValueChanged.AddListener(HandleEffectsVolumeChanged);
    }

    private void HandleMusicVolumeChanged(float volume)
    {
        // ������� ���� ����
        SoundManager.Instance.SetMusicVolume(volume);
    }

    private void HandleEffectsVolumeChanged(float volume)
    {
        // ȿ���� ���� ����
        SoundManager.Instance.SetEffectVolume(volume);
    }
}
