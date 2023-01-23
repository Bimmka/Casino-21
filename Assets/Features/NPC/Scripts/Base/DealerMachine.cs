using System;
using System.Collections;
using Features.Cards.Scripts.Element;
using Features.Hands.Scripts.Dealer;
using Features.NPC.Scripts.Data;
using UnityEngine;
using Zenject;

namespace Features.NPC.Scripts.Base
{
  public class DealerMachine : MonoBehaviour
  {
    private DealerHands dealerHands;
    private NPCSettings settings;

    public event Action End;

    public int Points => dealerHands.CardPoints();
    public bool IsFull => dealerHands.IsFull;
    public bool IsNotEmpty => dealerHands.IsNotEmpty;
    public bool IsTakingCard => dealerHands.IsTakingCard;

    [Inject]
    public void Construct(DealerHands dealerHands, NPCSettings settings)
    {
      this.dealerHands = dealerHands;
      this.settings = settings;
    }

    public void StartTurn()
    {
      StartCoroutine(Turn());
    }

    public void TakeCard()
    {
      dealerHands.TakeCard();
    }

    public void ReleaseCards() => 
      dealerHands.ReleaseCards();

    public void RemoveLastCard() => 
      dealerHands.RemoveLastCard();

    public void DisplayCardsCost(Action callback) => 
      dealerHands.DisplayCardsCost(callback);

    public void SetCard(Card card) => 
      dealerHands.SetCard(card);

    public Card PopFirstCard() => 
      dealerHands.PopFirstCard();

    private IEnumerator Turn()
    {
      while (dealerHands.IsFull == false && IsNeedTake())
      {
        TakeCard();
        while (IsTakingCard)
        {
          yield return null;
        }
      }
      End?.Invoke();
    }

    private bool IsNeedTake() => 
      dealerHands.CardPoints() <= settings.MaxPointsToTake;
  }
}