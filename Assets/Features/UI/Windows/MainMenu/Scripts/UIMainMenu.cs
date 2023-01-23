using Features.Constants;
using Features.Services.Audio;
using Features.Services.GameSettings;
using Features.Services.Leaderboard;
using Features.Services.Save;
using Features.Services.UI.Factory;
using Features.Services.UI.Windows;
using Features.Services.UserProvider;
using Features.StaticData.Audio;
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
    private IUserProvider userProvider;
    private ISaveService saveService;
    private ILeaderboard leaderboard;
    private IAudioService audioService;

    [Inject]
    public void Construct(IWindowsService windowsService, IGameSettings gameSettings, IUserProvider userProvider,
      ISaveService saveService, ILeaderboard leaderboard, IAudioService audioService)
    {
      this.audioService = audioService;
      this.leaderboard = leaderboard;
      this.saveService = saveService;
      this.userProvider = userProvider;
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
      if (userProvider.User.PointsData.CurrentPoints == 0)
      {
        userProvider.User.PointsData.Add(GameConstants.PlayerDefaultRestartPoints);
        saveService.SavePlayer(userProvider.User);
        leaderboard.LogPoints(userProvider.User.PointsData.CurrentPoints);
        windowsService.Open(WindowId.Restart);
      }
      base.Open();
      gameSettings.Reset();
    }

    private void Quit() => 
      Application.Quit();

    private void StartPlay()
    {
      audioService.Play(AudioEventType.Click);
      windowsService.Open(WindowId.Difficult);
    }

    private void OpenSettings()
    {
      audioService.Play(AudioEventType.Click);
      windowsService.Open(WindowId.Settings);
    }

    private void OpenAbout()
    {
      audioService.Play(AudioEventType.Click);
      windowsService.Open(WindowId.About);
    }

    private void OpenLeaderboard()
    {
      audioService.Play(AudioEventType.Click);
      windowsService.Open(WindowId.Leaderboard);
    }
  }
}