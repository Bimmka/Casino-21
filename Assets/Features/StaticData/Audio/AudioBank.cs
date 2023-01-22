using System;

namespace Features.StaticData.Audio
{
  [Serializable]
  public struct AudioBank
  {
    public string Path;
    public AudioBankType Type;
    public bool IsPreloadSimples;
  }
}