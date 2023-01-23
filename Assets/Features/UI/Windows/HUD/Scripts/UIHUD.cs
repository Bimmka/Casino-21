using Features.Services.Audio;
using Features.Services.UI.Factory;
using Features.Services.UI.Windows;
using Features.Services.UserProvider;
using Features.StaticData.Audio;
using Features.UI.Windows.Base.Scripts;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Features.UI.Windows.HUD.Scripts
{
  public class UIHUD : BaseWindow
  {
    [SerializeField] private PointsDisplayer pointsDisplayer;
    [SerializeField] private Button pauseMenuButton;
    private IWindowsService windowsService;
    private IAudioService audioService;

    [Inject]
    public void Construct(IWindowsService windowsService, IUserProvider userProvider, IAudioService audioService)
    {
      this.audioService = audioService;
      this.windowsService = windowsService;
      pointsDisplayer.Construct(userProvider.User.PointsData);
    }

    protected override void Subscribe()
    {
      base.Subscribe();
      pauseMenuButton.onClick.AddListener(OpenPauseMenu);
    }

    protected override void Cleanup()
    {
      base.Cleanup();
      pauseMenuButton.onClick.RemoveListener(OpenPauseMenu);
      pointsDisplayer.Cleanup();
    }

    private void OpenPauseMenu()
    {
      windowsService.Open(WindowId.PauseMenu);
      audioService.Play(AudioEventType.Click);
    }
  }
}