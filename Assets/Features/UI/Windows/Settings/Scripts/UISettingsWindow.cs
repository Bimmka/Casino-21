using Features.Services.Audio;
using Features.Services.UI.Windows;
using Features.StaticData.Audio;
using Features.UI.Windows.Base.Scripts;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Features.UI.Windows.Settings.Scripts
{
  public class UISettingsWindow : BaseWindow
  {
    [SerializeField] private Button closeButton;
    [SerializeField] private Toggle soundsToggle;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider ambientSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider effectsSlider;
    
    private IAudioService audioService;
    private IWindowsService windowsService;

    [Inject]
    public void Construct(IAudioService audioService, IWindowsService windowsService)
    {
      this.windowsService = windowsService;
      this.audioService = audioService;
    }

    public override void Open()
    {
      RestoreSliders();
      RestoreToggle();
      base.Open();
    }

    protected override void Subscribe()
    {
      base.Subscribe();
      closeButton.onClick.AddListener(Close);
      soundsToggle.onValueChanged.AddListener(OnClickSound);
      masterSlider.onValueChanged.AddListener(OnChangeMaster);
      ambientSlider.onValueChanged.AddListener(OnChangeAmbient);
      musicSlider.onValueChanged.AddListener(OnMusicChange);
      effectsSlider.onValueChanged.AddListener(OnEffectsChange);
    }

    protected override void Cleanup()
    {
      base.Cleanup();
      closeButton.onClick.RemoveListener(Close);
      soundsToggle.onValueChanged.RemoveListener(OnClickSound);
      masterSlider.onValueChanged.RemoveListener(OnChangeMaster);
      ambientSlider.onValueChanged.RemoveListener(OnChangeAmbient);
      musicSlider.onValueChanged.RemoveListener(OnMusicChange);
      effectsSlider.onValueChanged.RemoveListener(OnEffectsChange);
    }

    private void RestoreToggle()
    {
      soundsToggle.SetIsOnWithoutNotify(audioService.IsEnable);
    }

    private void RestoreSliders()
    {
      //masterSlider.SetValueWithoutNotify(LinearToDecibel(audioService.GetVolumeValue(AudioVolumeType.MasterVolume)));
      ambientSlider.SetValueWithoutNotify(LinearToDecibel(audioService.GetVolumeValue(AudioVolumeType.AmbientVolume)));
      musicSlider.SetValueWithoutNotify(LinearToDecibel(audioService.GetVolumeValue(AudioVolumeType.MusicVolume)));
      //effectsSlider.SetValueWithoutNotify(LinearToDecibel(audioService.GetVolumeValue(AudioVolumeType.EffectVolume)));
    }

    private void Close() => 
      windowsService.Close(ID);

    private void OnClickSound(bool isOn) => 
      audioService.SetEnableState(isOn);

    private void OnChangeMaster(float value) => 
      audioService.SetVolumeValue(AudioVolumeType.MasterVolume, DecibelToLinear(value));

    private void OnMusicChange(float value) => 
      audioService.SetVolumeValue(AudioVolumeType.MusicVolume, DecibelToLinear(value));

    private void OnChangeAmbient(float value) => 
      audioService.SetVolumeValue(AudioVolumeType.AmbientVolume, DecibelToLinear(value));

    private void OnEffectsChange(float value) => 
      audioService.SetVolumeValue(AudioVolumeType.EffectVolume, DecibelToLinear(value));

    private float DecibelToLinear(float dB) => 
      Mathf.Pow(10, dB / 20);

    private float LinearToDecibel(float linear) => 
      20 * Mathf.Log10(linear);
  }
}