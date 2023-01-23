using System;
using System.Collections;
using Features.Cards.Scripts.Deck;
using Features.Cards.Scripts.Element;
using Features.StaticData.Audio;
using Services.Audio;
using UnityEngine;
using Zenject;

namespace Features.Hands.Scripts.Base
{
  public abstract class BaseHands : MonoBehaviour
  {
    [SerializeField] private HandPoint[] cardPoints;
    
    private CardDeck deck;
    private IAudioService audioService;

    public bool IsFull => IsHandFull();
    public bool IsNotEmpty => FirstFulledPoint() != null;
    public bool IsTakingCard { get; private set; }

    public event Action<bool> TookedCard;

    [Inject]
    public void Construct(CardDeck deck, IAudioService audioService)
    {
      this.audioService = audioService;
      this.deck = deck;
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

    public virtual void TakeCard(Action callback = null)
    {
      IsTakingCard = true;
      NotifyAboutTookCard();
      audioService.Play(AudioEventType.Card);
      Card card = deck.TopCard();
      HandPoint freePoint = FreePoint();
      freePoint.SetCard(card, () => OnTookCard(callback));
    }
    
    public void SetCard(Card card)
    {
      IsTakingCard = true;
      NotifyAboutTookCard();
      HandPoint freePoint = FreePoint();
      freePoint.SetCard(card, () => OnTookCard());
    }

    public void ReleaseCards()
    {
      for (int i = 0; i < cardPoints.Length; i++)
      {
        cardPoints[i].Release();
      }
    }
    
    public Card PopFirstCard()
    {
      HandPoint point = FirstFulledPoint();
      Card card = point.Card;
      point.Reset();
      return card;
    }

    public void RemoveFirstCard(Action callback)
    {
      HandPoint point = FirstFulledPoint();
      point.Release();
      StartCoroutine(ShiftCards(callback));
    }

    public virtual void RemoveLastCard()
    {
      HandPoint point = LastFulledPoint();
      point.Release();
    }

    public void DisplayCardsCost(Action callback)
    {
      for (int i = 0; i < cardPoints.Length; i++)
      {
        if (cardPoints[i].Card == null)
          break;

        cardPoints[i].DisplayCardCost();
      }
      
      callback?.Invoke();
    }

    protected virtual void OnTookCard(Action callback = null)
    {
      IsTakingCard = false;
      NotifyAboutTookCard();
      callback?.Invoke();
    }

    protected HandPoint FirstFulledPoint()
    {
      for (int i = 0; i < cardPoints.Length; i++)
      {
        if (cardPoints[i].Card != null)
          return cardPoints[i];
      }

      return null;
    }

    protected HandPoint LastFulledPoint()
    {
      for (int i = cardPoints.Length - 1;  i >= 0; i--)
      {
        if (cardPoints[i].Card != null)
          return cardPoints[i];
      }

      return null;
    }

    protected IEnumerator ShiftCards(Action callback)
    {
      for (int i = 1; i < cardPoints.Length; i++)
      {
        if (cardPoints[i-1].Card != null || cardPoints[i].Card == null)
          continue;
        
        cardPoints[i-1].SetCard(cardPoints[i].Card);
        cardPoints[i].Reset();
      }

      while (IsHaveWaitingCardPoint())
      {
        yield return null;
      }
      
      callback?.Invoke();
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

    private bool IsHaveWaitingCardPoint()
    {
      for (int i = 0; i < cardPoints.Length; i++)
      {
        if (cardPoints[i].Card != null && cardPoints[i].IsWaitingCard)
          return true;
      }

      return false;
    }

    private void NotifyAboutTookCard() => 
      TookedCard?.Invoke(IsTakingCard);
  }
}