using Features.Services.Cleanup;
using Features.StaticData.Audio;
using FMOD.Studio;
using StaticData.Audio;

namespace Services.Audio
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
    void Play(AudioEventType type, AudioParameterType parameter, float value);
    void SetParameterValue(AudioEventType type, AudioParameterType parameter, int value);
    void AlwaysPlay(AudioEventType type);
  }
}