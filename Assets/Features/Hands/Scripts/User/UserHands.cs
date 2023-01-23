using System;
using Features.Hands.Scripts.Base;


namespace Features.Hands.Scripts.User
{
  public class UserHands : BaseHands
  {
    public event Action Refreshed;
    
    public void OpenFirstCard() => 
      FirstFulledPoint().DisplayCardCost();

    public void OpenLastCard() => 
      LastFulledPoint().DisplayCardCost();

    public override void RemoveLastCard()
    {
      base.RemoveLastCard();
      Refreshed?.Invoke();
    }
  }
}