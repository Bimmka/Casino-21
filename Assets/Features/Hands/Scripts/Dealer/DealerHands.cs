using Features.Cards.Scripts.Deck;
using Features.Hands.Scripts.Base;
using Features.Services.Audio;
using Zenject;

namespace Features.Hands.Scripts.Dealer
{
  public class DealerHands : BaseHands
  {
    [Inject]
    public void Construct(CardDeck deck, IAudioService audioService)
    {
      base.Construct(deck, audioService);
    }
  }
}