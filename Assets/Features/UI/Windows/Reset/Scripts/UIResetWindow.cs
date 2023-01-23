using Features.Services.Audio;
using Features.Services.UI.Windows;
using Features.StaticData.Audio;
using Features.UI.Windows.Base.Scripts;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Features.UI.Windows.Reset.Scripts
{
  public class UIResetWindow : BaseWindow
  {
    [SerializeField] private Button claimButton;
    
    private IAudioService audioService;
    private IWindowsService windowsService;

    [Inject]
    public void Construct(IAudioService audioService, IWindowsService windowsService)
    {
      this.windowsService = windowsService;
      this.audioService = audioService;
    }

    protected override void Subscribe()
    {
      base.Subscribe();
      claimButton.onClick.AddListener(Close);
    }

    protected override void Cleanup()
    {
      base.Cleanup();
      claimButton.onClick.RemoveListener(Close);
    }

    private void Close()
    {
      audioService.Play(AudioEventType.Click);
      windowsService.Close(ID);
    }
  }
}