using System;
using Features.Cards.Scripts.Element;
using UnityEngine;

namespace Features.Hands.Scripts.Base
{
  public class HandPoint : MonoBehaviour
  {
    [SerializeField] private Vector3 cardRotation = new Vector3(-90,0,0);
    public Card Card { get; private set; }
    
    public bool IsWaitingCard { get; private set; }

    public void SetCard(Card card, Action callback = null)
    {
      Card = card;
      IsWaitingCard = true;
      Card.Move(transform.position, Quaternion.Euler(cardRotation), () => OnCardSet(callback));
    }

    public void Release()
    {
      if (Card != null)
      {
        Card.Hide();
        Reset();
      }
    }

    public void DisplayCardCost()
    {
      Card.DisplayCost();
    }

    public void Reset()
    {
      Card = null;
    }

    private void OnCardSet(Action callback)
    {
      IsWaitingCard = false;
      callback?.Invoke();
    }
  }
}