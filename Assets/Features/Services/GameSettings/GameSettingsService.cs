using Features.Coefficients.Data;
using Features.Perks.Data;

namespace Features.Services.GameSettings
{
  public class GameSettingsService : IGameSettings
  {
    private readonly CoefficientsSettings coefficientsSettings;
    public GameDifficultType DifficultType { get; private set; }
    public PerkType PerkType { get; private set; }
    public int CurrentBet { get; private set; }

    public GameSettingsService(CoefficientsSettings coefficientsSettings)
    {
      this.coefficientsSettings = coefficientsSettings;
    }

    public void SetType(GameDifficultType type) => 
      DifficultType = type;

    public void InitializeBet(int bet) => 
      CurrentBet = bet;

    public float CurrentCoefficient() => 
      coefficientsSettings.WinCoefficients[DifficultType];

    public void AddPerk(PerkType type) => 
      PerkType = type;
  }
}