using Features.Cards.Scripts.Deck;
using Features.Cards.Scripts.Element;
using UnityEngine;
using Zenject;

namespace Features.Hands.Scripts.Base
{
  public abstract class BaseHands : MonoBehaviour
  {
    [SerializeField] private HandPoint[] cardPoints;
    private CardDeck deck;

    public bool IsFull => IsHandFull();

    [Inject]
    public void Construct(CardDeck deck)
    {
      this.deck = deck;
    }

    public void Open()
    {
      
    }

    public int CardPoints()
    {
      int sum = 0;
      for (int i = 0; i < cardPoints.Length; i++)
      {
        if (cardPoints[i].Card == null)
          break;
        
        sum += cardPoints[i].Card.Cost;
      }

      return sum;
    }

    public void TakeCard()
    {
      Card card = deck.TopCard();
      HandPoint freePoint = FreePoint();
      card.transform.position = freePoint.transform.position;
      card.transform.rotation = Quaternion.Euler(90,0,0);
      freePoint.SetCard(card);
    }

    public void ReleaseCards()
    {
      for (int i = 0; i < cardPoints.Length; i++)
      {
        cardPoints[i].Release();
      }
    }

    private bool IsHandFull()
    {
      for (int i = 0; i < cardPoints.Length; i++)
      {
        if (cardPoints[i].Card == null)
          return false;
      }

      return true;
    }

    private HandPoint FreePoint()
    {
      for (int i = 0; i < cardPoints.Length; i++)
      {
        if (cardPoints[i].Card == null)
          return cardPoints[i];
      }

      return null;
    }
  }
}