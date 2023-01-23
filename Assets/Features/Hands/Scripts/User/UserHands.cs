using System;
using Features.Cards.Scripts.Deck;
using Features.Hands.Scripts.Base;
using Features.Services.Audio;
using Zenject;


namespace Features.Hands.Scripts.User
{
  public class UserHands : BaseHands
  {
    private UserAnimation userAnimation;
    public event Action Refreshed;

    [Inject]
    public void Construct(CardDeck deck, IAudioService audioService, UserAnimation userAnimation)
    {
      this.userAnimation = userAnimation;
      base.Construct(deck, audioService);
    }

    public override void TakeCard(Action callback = null)
    {
      SetIsTakingCard(true);
      NotifyAboutTookCard();
      userAnimation.SetMore(() => base.TakeCard(callback));
    }

    public void CheckAnimation(Action callback) => 
      userAnimation.SetStop(callback);

    public void WinAnimation() => 
      userAnimation.SetWin();

    public void LoseAnimation() => 
      userAnimation.SetLose();

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