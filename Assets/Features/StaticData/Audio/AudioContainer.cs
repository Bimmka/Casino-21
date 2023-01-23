using StaticData.Audio;
using UnityEngine;

namespace Features.StaticData.Audio
{
  [CreateAssetMenu(fileName = "AudioContainer", menuName = "StaticData/Audio/Create Audio Container", order = 52)]
  public class AudioContainer : ScriptableObject
  {
    public int MaxHoldingInstancesCount = 10;
    public int MaxPlayingEventsOnOneTime = 5;
    public AudioEvent[] Events;
    public AudioBank[] Banks;
    public VolumeParameter[] Parameters;
  }
}