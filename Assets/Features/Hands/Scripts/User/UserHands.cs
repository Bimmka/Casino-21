using Features.Hands.Scripts.Base;

namespace Features.Hands.Scripts.User
{
  public class UserHands : BaseHands
  {
    public void OpenFirstCard() => 
      FirstFulledPoint().DisplayCardCost();

    public void OpenLastCard() => 
      LastFulledPoint().DisplayCardCost();
    
  }
}