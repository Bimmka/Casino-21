using System.Collections.Generic;
using Features.Cards.Scripts.Element;
using Features.Services.GameSettings;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Features.Cards.Scripts.Data.Cards
{
  [CreateAssetMenu(fileName = "CardSettings", menuName = "StaticData/Cards/Create Card Settings", order = 52)]
  public class CardSettings : SerializedScriptableObject
  {
    public int Cost;
    public Card Prefab;
    public Dictionary<GameDifficultType, Texture2D[]> DifficultMasks;
    public float AlphaCutoff;
  }
}