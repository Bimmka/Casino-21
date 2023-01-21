using Features.Hands.Scripts.Base;

namespace Features.Hands.Scripts.User
{
  public class UserHands : BaseHands
  {
    public bool IsTakingCard { get; private set; }

    public override void TakeCard()
    {
      base.TakeCard();
      IsTakingCard = true;
    }

    protected override void OnTookCard()
    {
      base.OnTookCard();
      IsTakingCard = false;
    }
  }
}