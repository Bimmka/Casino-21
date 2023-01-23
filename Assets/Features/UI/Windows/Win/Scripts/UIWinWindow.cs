using Features.GameStates;
using Features.GameStates.States;
using Features.Level.Scripts.LevelStates.Machine;
using Features.Level.Scripts.LevelStates.States;
using Features.Services.Audio;
using Features.Services.GameSettings;
using Features.Services.UI.Windows;
using Features.StaticData.Audio;
using Features.UI.Windows.Base.Scripts;
using FMOD.Studio;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Features.UI.Windows.Win.Scripts
{
  public class UIWinWindow : BaseWindow
  {
    [SerializeField] private TextMeshProUGUI winText;
    [SerializeField] private Button leaveButton;
    [SerializeField] private Button restartButton;
    
    private IGameSettings gameSettings;
    private IGameStateMachine gameStateMachine;
    private ILevelStateMachine levelStateMachine;
    private IWindowsService windowsService;
    private IAudioService audioService;

    [Inject]
    public void Construct(IGameSettings gameSettings, IGameStateMachine gameStateMachine,
      ILevelStateMachine levelStateMachine, IWindowsService windowsService, IAudioService audioService)
    {
      this.audioService = audioService;
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
      winText.text = ((int)(gameSettings.CurrentBet * gameSettings.CurrentCoefficient())).ToString();
      audioService.Play(AudioEventType.Win);
      base.Open();
    }

    private void LoadMainMenu()
    {
      audioService.Play(AudioEventType.Click);
      audioService.Stop(AudioEventType.GameAmbient, STOP_MODE.ALLOWFADEOUT);
      audioService.Stop(AudioEventType.GameMusic, STOP_MODE.ALLOWFADEOUT);
      gameStateMachine.Enter<MainMenuState>();
    }

    private void RestartGame()
    {
      audioService.Play(AudioEventType.Click);
      levelStateMachine.Enter<LevelResetState>();
      windowsService.Close(ID);
    }
  }
}