using Features.Services.Audio;
using Features.Services.UI.Windows;
using Features.StaticData.Audio;
using Features.UI.Windows.Base.Scripts;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Features.UI.Windows.About.Scripts
{
  public class UIAboutWindow : BaseWindow
  {
    [SerializeField] private Button backButton;
    
    private IWindowsService windowsService;
    private IAudioService audioService;

    [Inject]
    public void Construct(IWindowsService windowsService, IAudioService audioService)
    {
      this.audioService = audioService;
      this.windowsService = windowsService;
    }

    protected override void Subscribe()
    {
      base.Subscribe();
      backButton.onClick.AddListener(Close);
    }

    protected override void Cleanup()
    {
      base.Cleanup();
      backButton.onClick.RemoveListener(Close);
    }

    private void Close()
    {
      windowsService.Close(ID);
      audioService.Play(AudioEventType.Click);
    }
  }
}