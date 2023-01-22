using System;
using System.Collections;
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

    private IEnumerator Turn()
    {
      while (dealerHands.IsFull == false && IsNeedTake())
      {
        TakeCard();
        while (dealerHands.IsTakingCard)
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