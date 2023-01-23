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
    [SerializeField] private Button playButton;
    [SerializeField] private Button leaderboardButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button aboutButton;
    [SerializeField] private Button quitButton;
    
    private IWindowsService windowsService;
    private IGameSettings gameSettings;

    [Inject]
    public void Construct(IWindowsService windowsService, IGameSettings gameSettings)
    {
      this.gameSettings = gameSettings;
      this.windowsService = windowsService;
    }

    protected override void Subscribe()
    {
      base.Subscribe();
      playButton.onClick.AddListener(StartPlay);
      settingsButton.onClick.AddListener(OpenSettings);
      aboutButton.onClick.AddListener(OpenAbout);
      quitButton.onClick.AddListener(Quit);
      leaderboardButton.onClick.AddListener(OpenLeaderboard);
    }

    protected override void Cleanup()
    {
      base.Cleanup();
      playButton.onClick.RemoveListener(StartPlay);
      settingsButton.onClick.RemoveListener(OpenSettings);
      aboutButton.onClick.RemoveListener(OpenAbout);
      leaderboardButton.onClick.RemoveListener(OpenLeaderboard);
      quitButton.onClick.RemoveListener(Quit);
    }

    public override void Open()
    {
      base.Open();
      gameSettings.Reset();
    }

    private void Quit()
    {
      Application.Quit();
    }

    private void StartPlay()
    {
      windowsService.Open(WindowId.Difficult);
      windowsService.Close(ID);
    }

    private void OpenSettings()
    {
      windowsService.Open(WindowId.Settings);
      windowsService.Close(ID);
    }

    private void OpenAbout() => 
      windowsService.Open(WindowId.About);

    private void OpenLeaderboard() => 
      windowsService.Open(WindowId.Leaderboard);
  }
}