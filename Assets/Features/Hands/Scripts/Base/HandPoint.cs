using Features.Cards.Scripts.Element;
using UnityEngine;

namespace Features.Hands.Scripts.Base
{
  public class HandPoint : MonoBehaviour
  {
    public Card Card { get; private set; }

    public void SetCard(Card card)
    {
      Card = card;
      Card.Move(transform.position, Quaternion.Euler(90,0,0));
    }

    public void Release()
    {
      if (Card != null)
      {
        Card.Hide();
        Card = null;
      }
    }
  }
}