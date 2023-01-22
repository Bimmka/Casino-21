using System;
using Features.Cards.Scripts.Deck;
using Features.Cards.Scripts.Element;
using Services.Audio;
using StaticData.Audio;
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

    public virtual void TakeCard()
    {
      IsTakingCard = true;
      audioService.Play(AudioEventType.Card);
      Card card = deck.TopCard();
      HandPoint freePoint = FreePoint();
      freePoint.SetCard(card, OnTookCard);
    }

    public void ReleaseCards()
    {
      for (int i = 0; i < cardPoints.Length; i++)
      {
        cardPoints[i].Release();
      }
    }
    
    public void RemoveFirstCard()
    {
      HandPoint point = FirstFulledPoint();
      point.Release();
    }
    
    public void RemoveLastCard()
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

    protected virtual void OnTookCard()
    {
      IsTakingCard = false;
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