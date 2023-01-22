using System;
using Features.Cards.Scripts.Element;
using UnityEngine;

namespace Features.Hands.Scripts.Base
{
  public class HandPoint : MonoBehaviour
  {
    public Card Card { get; private set; }

    public void SetCard(Card card, Action callback = null)
    {
      Card = card;
      Card.Move(transform.position, Quaternion.Euler(90,0,0), callback);
    }

    public void Release()
    {
      if (Card != null)
      {
        Card.Hide();
        Card = null;
      }
    }

    public void DisplayCardCost()
    {
      Card.DisplayCost();
    }
  }
}