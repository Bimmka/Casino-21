using Features.Hands.Scripts.Base;
using Features.Perks.Data;
using Features.Perks.Strategy;
using Features.User.Data;

namespace Features.Hands.Scripts.User
{
  public class UserHands : BaseHands
  {
    private PerkStrategy perkStrategy;
    private UserPointsData pointsData;
    public bool IsCanUsePerk => IsHavePerk && perkStrategy.IsCanBeUsed() && pointsData.IsEnough(perkStrategy.Cost);
    public bool IsHavePerk => perkStrategy != null;
    public PerkType PerkType => perkStrategy.Type;

    public void Initialize(PerkStrategy perkStrategy, UserPointsData pointsData)
    {
      this.pointsData = pointsData;
      this.perkStrategy = perkStrategy;
    }

    public void OpenFirstCard() => 
      FirstFulledPoint().DisplayCardCost();

    public void OpenLastCard() => 
      LastFulledPoint().DisplayCardCost();

    public void UsePerk()
    {
      perkStrategy.Use();
    }
  }
}