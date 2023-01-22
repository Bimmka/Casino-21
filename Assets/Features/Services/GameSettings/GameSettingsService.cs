using Features.Coefficients.Data;

namespace Features.Services.GameSettings
{
  public class GameSettingsService : IGameSettings
  {
    private readonly CoefficientsSettings coefficientsSettings;
    public GameDifficultType DifficultType { get; private set; }
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
  }
}