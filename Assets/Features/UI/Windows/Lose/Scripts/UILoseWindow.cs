using Features.GameStates;
using Features.GameStates.States;
using Features.Level.Scripts.LevelStates.Machine;
using Features.Level.Scripts.LevelStates.States;
using Features.Services.GameSettings;
using Features.Services.UI.Windows;
using Features.UI.Windows.Base.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Features.UI.Windows.Lose.Scripts
{
  public class UILoseWindow : BaseWindow
  {
    [SerializeField] private TextMeshProUGUI lostText;
    [SerializeField] private string lostTextFormat;
    [SerializeField] private Button leaveButton;
    [SerializeField] private Button restartButton;
    
    private IGameSettings gameSettings;
    private IGameStateMachine gameStateMachine;
    private ILevelStateMachine levelStateMachine;
    private IWindowsService windowsService;

    [Inject]
    public void Construct(IGameSettings gameSettings, IGameStateMachine gameStateMachine,
      ILevelStateMachine levelStateMachine, IWindowsService windowsService)
    {
      this.windowsService = windowsService;
      this.levelStateMachine = levelStateMachine;
      this.gameStateMachine = gameStateMachine;
      this.gameSettings = gameSettings;
    }

    protected override void Subscribe()
    {
      base.Subscribe();
      leaveButton.onClick.AddListener(LoadMainMenu);
      restartButton.onClick.AddListener(RestartGame);
    }

    protected override void Cleanup()
    {
      base.Cleanup();
      leaveButton.onClick.RemoveListener(LoadMainMenu);
      restartButton.onClick.RemoveListener(RestartGame);
    }

    public override void Open()
    {
      lostText.text = string.Format(lostTextFormat, gameSettings.CurrentBet);
      base.Open();
    }

    private void LoadMainMenu() => 
      gameStateMachine.Enter<MainMenuState>();

    private void RestartGame()
    {
      levelStateMachine.Enter<LevelResetState>();
      windowsService.Close(ID);
    }
  }
}