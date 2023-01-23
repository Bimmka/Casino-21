using Features.GameStates;
using Features.GameStates.States;
using Features.Services.Audio;
using Features.Services.UI.Factory;
using Features.Services.UI.Windows;
using Features.StaticData.Audio;
using Features.UI.Windows.Base.Scripts;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Features.UI.Windows.PauseMenu.Scripts
{
  public class UIPauseMenu : BaseWindow
  {
    [SerializeField] private Button exitButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button continueButton;
    private IGameStateMachine gameStateMachine;
    private IWindowsService windowsService;
    private IAudioService audioService;

    [Inject]
    public void Construct(IGameStateMachine gameStateMachine, IWindowsService windowsService, IAudioService audioService)
    {
      this.audioService = audioService;
      this.windowsService = windowsService;
      this.gameStateMachine = gameStateMachine;
    }

    protected override void Subscribe()
    {
      base.Subscribe();
      exitButton.onClick.AddListener(LoadMainMenu);
      continueButton.onClick.AddListener(Close);
      settingsButton.onClick.AddListener(OpenSettings);
    }

    protected override void Cleanup()
    {
      base.Cleanup();
      exitButton.onClick.RemoveListener(LoadMainMenu);
      continueButton.onClick.RemoveListener(Close);
      settingsButton.onClick.RemoveListener(OpenSettings);
    }

    private void OpenSettings()
    {
      windowsService.Open(WindowId.Settings);
      audioService.Play(AudioEventType.Click);
    }

    private void LoadMainMenu()
    {
      audioService.Play(AudioEventType.Click);
      gameStateMachine.Enter<MainMenuState>();
    }

    private void Close()
    {
      audioService.Play(AudioEventType.Click);
      windowsService.Close(ID);
    }
  }
}