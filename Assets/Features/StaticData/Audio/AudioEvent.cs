using System;
using Features.StaticData.Audio;

namespace StaticData.Audio
{
  [Serializable]
  public struct AudioEvent
  {
    public string Path;
    public AudioBankType BankType;
    public AudioEventType Type;
  }
}