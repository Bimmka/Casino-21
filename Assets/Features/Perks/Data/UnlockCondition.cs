using System;

namespace Features.Perks.Data
{
  [Serializable]
  public struct UnlockCondition
  {
    public UnlockConditionType Type;
    public int Count;
    public string TextFormat;
  }
}