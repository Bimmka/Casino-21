using System;

namespace Features.StaticData.Audio
{
  [Serializable]
  public struct VolumeParameter
  {
    public AudioVolumeType Type;
    public string Name;
    public float DefaultValue;
  }
}