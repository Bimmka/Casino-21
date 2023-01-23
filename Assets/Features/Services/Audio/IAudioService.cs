using Features.Services.Cleanup;
using Features.StaticData.Audio;
using FMOD.Studio;

namespace Features.Services.Audio
{
  public interface IAudioService : ICleanup
  {
    bool IsEnable { get; }
    void SetEnableState(bool isEnable);
    void Play(AudioEventType type);
    void Stop(AudioEventType type, STOP_MODE stopMode);
    void LoadBank(AudioBankType type);
    void ReleaseBank(AudioBankType type);
    bool IsBankLoaded(AudioBankType type);
    void SetVolumeValue(AudioVolumeType volume, float value);
    float GetVolumeValue(AudioVolumeType type);
    void InitializeBuses();
  }
}