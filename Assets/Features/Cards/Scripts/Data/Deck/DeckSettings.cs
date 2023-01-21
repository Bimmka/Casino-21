using Sirenix.OdinInspector;
using UnityEngine;

namespace Features.Cards.Scripts.Data.Deck
{
  [CreateAssetMenu(fileName = "DeckSettings", menuName = "StaticData/Cards/Create Deck Settings", order = 52)]
  public class DeckSettings : ScriptableObject
  {
    [OnValueChanged("UpdateTotalCardsCount", IncludeChildren = true)]
    public CardContainer[] Cards;
    [ReadOnly]
    public int TotalCardsCount;

    private void UpdateTotalCardsCount(CardContainer[] cards)
    {
      TotalCardsCount = 0;
      
      if (cards == null || cards.Length == 0)
        return;

      for (int i = 0; i < cards.Length; i++)
      {
        TotalCardsCount += cards[i].Count;
      }
    }
  }
}