using System;
using Features.Level.Scripts.Info;
using Features.Perks.Data;
using Features.Services.GameSettings;

namespace Features.Perks.Strategy
{
  public class MultiplyBetPerk : PerkStrategy
  {
    private readonly IGameSettings gameSettings;
    private readonly LevelInfoDisplayer levelInfoDisplayer;

    public MultiplyBetPerk(PerkSettings settings, IGameSettings gameSettings, LevelInfoDisplayer levelInfoDisplayer) : base(settings)
    {
      this.gameSettings = gameSettings;
      this.levelInfoDisplayer = levelInfoDisplayer;
    }

    public override bool IsCanBeUsed() => 
      true;

    public override void Use(Action callback)
    {
      gameSettings.InitializeBet((int)(gameSettings.CurrentBet * settings.Coeff));
      levelInfoDisplayer.DisplayBet(gameSettings.CurrentBet);
      callback?.Invoke();
    }
  }
}