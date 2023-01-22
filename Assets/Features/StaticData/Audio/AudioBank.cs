using System;

namespace StaticData.Audio
{
  [Serializable]
  public struct AudioBank
  {
    public string Path;
    public AudioBankType Type;
    public bool IsPreloadSimples;
  }
}