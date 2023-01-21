using System;
using System.Collections.Generic;
using Features.Cards.Scripts.Container;
using Features.Cards.Scripts.Data.Deck;
using Features.Cards.Scripts.Element;
using Features.Cards.Scripts.Factory;
using Features.Cards.Scripts.Shuffle;
using UnityEngine;
using Zenject;

namespace Features.Cards.Scripts.Deck
{
  public class CardDeck : MonoBehaviour
  {
    [SerializeField] private Transform startPoint;
    [SerializeField] private Vector3 spawnShift = new Vector3(0,0.5f,0);
    [SerializeField] private Vector3 spawnRotation = new Vector3(-90,0,0);
    
    private CardsContainer cardsContainer;
    private CardFactory factory;
    private DeckSettings settings;
    private CardDeckShuffler shuffler;

    private List<string> cardIds;

    [Inject]
    public void Construct(CardsContainer cardsContainer, CardFactory factory)
    {
      this.factory = factory;
      this.cardsContainer = cardsContainer;
    }

    public void InitializeDeck(DeckSettings settings)
    {
      this.settings = settings;
      shuffler = new CardDeckShuffler();
      cardIds = new List<string>(settings.TotalCardsCount);
    }

    public void Create()
    {
      int index = 0;
      Card card;
      for (int i = 0; i < settings.Cards.Length; i++)
      {
        for (int j = 0; j < settings.Cards[i].Count; j++)
        {
          card = factory.Create(settings.Cards[i].Settings, j,transform, CardPosition(index), Quaternion.Euler(spawnRotation));
          cardsContainer.Register(card);
          cardIds.Add(card.ID);
          index++;
        }
      }
    }

    public void Shuffle(Action callback)
    {
      cardIds = shuffler.Shuffle(cardIds);
      SwapCards();
      callback?.Invoke();
    }

    private void SwapCards()
    {
      Card card;
      for (int i = 0; i < cardIds.Count; i++)
      {
        card = cardsContainer.Card(cardIds[i]);
        card.transform.position = CardPosition(cardIds.Count - i -1);
      }
    }

    private Vector3 CardPosition(int index) => 
      startPoint.position + spawnShift * index;
  }
}