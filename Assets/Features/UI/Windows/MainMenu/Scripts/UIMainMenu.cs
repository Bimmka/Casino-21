using Features.Services.GameSettings;
using Features.Services.UI.Factory;
using Features.Services.UI.Windows;
using Features.UI.Windows.Base.Scripts;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Features.UI.Windows.MainMenu.Scripts
{
  public class UIMainMenu : BaseWindow
  {
    [SerializeField] private Button easyPlayButton;
    [SerializeField] private Button mediumPlayButton;
    [SerializeField] private Button hardPlayButton;
    [SerializeField] private Button leaderboardButton;
    
    private IGameSettings gameSettings;
    private IWindowsService windowsService;

    [Inject]
    public void Construct(IGameSettings gameSettings, IWindowsService windowsService)
    {
      this.windowsService = windowsService;
      this.gameSettings = gameSettings;
    }

    protected override void Subscribe()
    {
      base.Subscribe();
      easyPlayButton.onClick.AddListener(StartEasyPlay);
      mediumPlayButton.onClick.AddListener(StartMediumPlay);
      hardPlayButton.onClick.AddListener(StartHardPlay);
      leaderboardButton.onClick.AddListener(OpenLeaderboard);
    }

    protected override void Cleanup()
    {
      base.Cleanup();
      easyPlayButton.onClick.RemoveListener(StartEasyPlay);
      mediumPlayButton.onClick.RemoveListener(StartMediumPlay);
      hardPlayButton.onClick.RemoveListener(StartHardPlay);
      leaderboardButton.onClick.RemoveListener(OpenLeaderboard);
    }

    public override void Open()
    {
      base.Open();
      gameSettings.Reset();
    }

    private void StartEasyPlay() => 
      StartGame(GameDifficultType.Easy);

    private void StartMediumPlay() => 
      StartGame(GameDifficultType.Medium);

    private void StartHardPlay() => 
      StartGame(GameDifficultType.Hard);

    private void StartGame(GameDifficultType type)
    {
      gameSettings.SetType(type);
      windowsService.Close(ID);
      windowsService.Open(WindowId.Perks);
    }

    private void OpenLeaderboard() => 
      windowsService.Open(WindowId.Leaderboard);
  }
}