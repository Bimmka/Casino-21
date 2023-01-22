using System.Collections.Generic;
using Features.Services.GameSettings;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Features.Coefficients.Data
{
  [CreateAssetMenu(fileName = "CoefficientsSettings", menuName = "StaticData/Coefficients/Create Coefficients Settings", order = 52)]
  public class CoefficientsSettings : SerializedScriptableObject
  {
    public Dictionary<GameDifficultType, float> WinCoefficients;
  }
}