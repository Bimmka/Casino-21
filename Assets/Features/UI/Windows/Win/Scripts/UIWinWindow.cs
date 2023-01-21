using Features.GameStates;
using Features.GameStates.States;
using Features.Level.LevelStates.Machine;
using Features.Level.LevelStates.States;
using Features.Services.GameSettings;
using Features.UI.Windows.Base.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Features.UI.Windows.Win.Scripts
{
  public class UIWinWindow : BaseWindow
  {
    [SerializeField] private TextMeshProUGUI winText;
    [SerializeField] private string winTextFormat;
    [SerializeField] private Button leaveButton;
    [SerializeField] private Button restartButton;
    
    private IGameSettings gameSettings;
    private IGameStateMachine gameStateMachine;
    private ILevelStateMachine levelStateMachine;

    [Inject]
    public void Construct(IGameSettings gameSettings, IGameStateMachine gameStateMachine,
      ILevelStateMachine levelStateMachine)
    {
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
      winText.text = string.Format(winTextFormat, gameSettings.CurrentBet * 2);
      base.Open();
    }

    private void LoadMainMenu() => 
      gameStateMachine.Enter<MainMenuState>();

    private void RestartGame() => 
      levelStateMachine.Enter<LevelResetState>();
  }
}