using Features.GameStates;
using Features.GameStates.States;
using Features.Level.Scripts.LevelStates.Machine;
using Features.Level.Scripts.LevelStates.States;
using Features.Services.Audio;
using Features.Services.GameSettings;
using Features.Services.UI.Windows;
using Features.Services.UserProvider;
using Features.StaticData.Audio;
using Features.UI.Windows.Base.Scripts;
using FMOD.Studio;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Features.UI.Windows.Lose.Scripts
{
  public class UILoseWindow : BaseWindow
  {
    [SerializeField] private TextMeshProUGUI lostText;
    [SerializeField] private Button leaveButton;
    [SerializeField] private Button restartButton;
    
    private IGameSettings gameSettings;
    private IGameStateMachine gameStateMachine;
    private ILevelStateMachine levelStateMachine;
    private IWindowsService windowsService;
    private IAudioService audioService;
    private IUserProvider userProvider;

    [Inject]
    public void Construct(IGameSettings gameSettings, IGameStateMachine gameStateMachine,
      ILevelStateMachine levelStateMachine, IWindowsService windowsService, IAudioService audioService,
      IUserProvider userProvider)
    {
      this.userProvider = userProvider;
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
      lostText.text = gameSettings.CurrentBet.ToString();
      audioService.Play(AudioEventType.Lose);
      if (userProvider.User.PointsData.CurrentPoints == 0)
        restartButton.gameObject.SetActive(false);
      base.Open();
    }

    private void LoadMainMenu()
    {
      audioService.Stop(AudioEventType.GameAmbient, STOP_MODE.ALLOWFADEOUT);
      audioService.Stop(AudioEventType.GameMusic, STOP_MODE.ALLOWFADEOUT);
      audioService.Play(AudioEventType.Click);
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