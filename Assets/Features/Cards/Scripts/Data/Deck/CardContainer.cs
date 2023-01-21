using System;
using Features.Cards.Scripts.Data.Cards;

namespace Features.Cards.Scripts.Data.Deck
{
  [Serializable]
  public struct CardContainer
  {
    public CardSettings Settings;
    public int Count;
  }
}