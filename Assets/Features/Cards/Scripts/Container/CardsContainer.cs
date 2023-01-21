using System.Collections.Generic;
using Features.Cards.Scripts.Element;

namespace Features.Cards.Scripts.Container
{
  public class CardsContainer
  {
    public readonly Dictionary<string, Card> spawnedCards;

    public CardsContainer()
    {
      spawnedCards = new Dictionary<string, Card>(50);
    }

    public Card Card(string id) => 
      spawnedCards[id];

    public void Register(Card card) => 
      spawnedCards.Add(card.ID, card);
  }
}