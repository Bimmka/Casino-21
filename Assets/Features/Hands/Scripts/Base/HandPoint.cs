using Features.Cards.Scripts.Element;
using UnityEngine;

namespace Features.Hands.Scripts.Base
{
  public class HandPoint : MonoBehaviour
  {
    public Card Card { get; private set; }

    public void SetCard(Card card) => 
      Card = card;

    public void Release() => 
      Card = null;
  }
}