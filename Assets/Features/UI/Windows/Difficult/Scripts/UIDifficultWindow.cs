using Features.Services.GameSettings;
using Features.Services.UI.Factory;
using Features.Services.UI.Windows;
using Features.UI.Windows.Base.Scripts;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Features.UI.Windows.Difficult.Scripts
{
  public class UIDifficultWindow : BaseWindow
  {
    [SerializeField] private Button easyPlayButton;
    [SerializeField] private Button mediumPlayButton;
    [SerializeField] private Button hardPlayButton;
    [SerializeField] private Button backButton;
    
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
      backButton.onClick.AddListener(Back);
    }

    protected override void Cleanup()
    {
      base.Cleanup();
      easyPlayButton.onClick.RemoveListener(StartEasyPlay);
      mediumPlayButton.onClick.RemoveListener(StartMediumPlay);
      hardPlayButton.onClick.RemoveListener(StartHardPlay);
      backButton.onClick.RemoveListener(Back);
    }

    private void Back()
    {
      windowsService.Open(WindowId.MainMenu);
      windowsService.Close(ID);
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
      windowsService.Open(WindowId.Perks);
      windowsService.Close(ID);
    }
  }
}