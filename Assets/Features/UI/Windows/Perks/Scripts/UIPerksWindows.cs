using System.Collections.Generic;
using Features.GameStates;
using Features.GameStates.States;
using Features.Perks.Data;
using Features.Services.Assets;
using Features.Services.GameSettings;
using Features.Services.StaticData;
using Features.Services.UI.Factory;
using Features.Services.UI.Windows;
using Features.Services.UserProvider;
using Features.UI.Windows.Base.Scripts;
using Features.User.Data;
using UnityEngine;
using UnityEngine.UI;
using Zenject;


namespace Features.UI.Windows.Perks.Scripts
{
  public class UIPerksWindows : BaseWindow
  {
    [SerializeField] private Transform spawnParent;
    [SerializeField] private PerkElement perkPrefab;
    [SerializeField] private Button backButton;
    [SerializeField] private Button playButton;
    
    private IGameStateMachine gameStateMachine;
    private IGameSettings gameSettings;
    private PerksSpawner perksSpawner;
    private IWindowsService windowsService;

    private List<PerkElement> perks;
    private PerksSettingsContainer perksSettingsContainer;

    private PerkElement clickedPerk;
    private UserOpenPerks userPerksData;

    [Inject]
    public void Construct(IGameStateMachine gameStateMachine, IGameSettings gameSettings, IAssetProvider assetProvider,
      IStaticDataService staticDataService, IWindowsService windowsService, IUserProvider userProvider)
    {
      this.windowsService = windowsService;
      this.gameSettings = gameSettings;
      this.gameStateMachine = gameStateMachine;
      perksSpawner = new PerksSpawner(assetProvider, spawnParent, perkPrefab);
      perks = new List<PerkElement>(10);
      perksSettingsContainer = staticDataService.ForPerks();
      userPerksData = userProvider.User.OpenPerksData;
    }

    public override void Open()
    {
      for (int i = 0; i < perksSettingsContainer.Perks.Length; i++)
      {
        CreatePerk(perksSettingsContainer.Perks[i]);
      }
      base.Open();
    }

    protected override void Subscribe()
    {
      base.Subscribe();
      backButton.onClick.AddListener(Close);
      playButton.onClick.AddListener(Play);
    }

    protected override void Cleanup()
    {
      base.Cleanup();
      backButton.onClick.RemoveListener(Close);
      playButton.onClick.RemoveListener(Play);

      for (int i = 0; i < perks.Count; i++)
      {
        perks[i].Clicked -= OnChoosePerk;
        perks[i].Cleanup();
      }
    }

    private void CreatePerk(PerkSettings settings)
    {
      PerkElement element = perksSpawner.Create(settings, userPerksData.IsOpen(settings.Type));
      element.Clicked += OnChoosePerk;
      perks.Add(element);
    }

    private void OnChoosePerk(PerkElement perk)
    {
      if (IsSame(perk))
        Deselect();
      else if (IsOther(perk))
      {
        Deselect();
        Select(perk);
      }
      else if (IsNew())
        Select(perk);
      
    }

    private void Select(PerkElement perk)
    {
      clickedPerk = perk;
      clickedPerk.Select();
    }

    private void Deselect()
    {
      clickedPerk.Deselect();
      clickedPerk = null;
    }

    private bool IsNew() => 
      clickedPerk == null;

    private bool IsSame(PerkElement perk) => 
      clickedPerk != null && clickedPerk.Type == perk.Type;

    private bool IsOther(PerkElement perk) => 
      clickedPerk != null && clickedPerk.Type != perk.Type;

    private void Close()
    {
      windowsService.Open(WindowId.Difficult);
      windowsService.Close(ID);
    }

    private void Play()
    {
      gameSettings.AddPerk(clickedPerk == null ? PerkType.None : clickedPerk.Type);
      gameStateMachine.Enter<GameLoadState>();
    }
  }
}