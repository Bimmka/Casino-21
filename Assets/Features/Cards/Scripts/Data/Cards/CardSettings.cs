using Features.Cards.Scripts.Element;
using UnityEngine;

namespace Features.Cards.Scripts.Data.Cards
{
  [CreateAssetMenu(fileName = "CardSettings", menuName = "StaticData/Cards/Create Card Settings", order = 52)]
  public class CardSettings : ScriptableObject
  {
    public int Cost;
    public Card Prefab;
  }
}