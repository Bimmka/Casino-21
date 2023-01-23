using Features.Hands.Scripts.User;
using Features.Level.Scripts.LevelStates.Machine;
using Features.Level.Scripts.LevelStates.States;
using Features.Perks.Data;
using Features.Perks.Observer;
using Features.Services.Audio;
using Features.Services.StaticData;
using Features.StaticData.Audio;
using Features.UI.Windows.Base.Scripts;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Features.UI.Windows.Actions.Scripts
{
  [RequireComponent(typeof(UIActionsWindowView))]
  public class UIActionsWindow : BaseWindow
  {
    [SerializeField] private UIActionsWindowView view;
    [SerializeField] private Button takeButton;
    [SerializeField] private Button checkButton;
    [SerializeField] private Button usePerkButton;

    private UserHands userHands;
    private ILevelStateMachine levelStateMachine;
    private IStaticDataService staticDataService;
    private PerksObserver perksObserver;
    private IAudioService audioService;

    [Inject]
    public void Construct(UserHands userHands, ILevelStateMachine levelStateMachine, IStaticDataService staticDataService,
      PerksObserver perksObserver, IAudioService audioService)
    {
      this.audioService = audioService;
      this.perksObserver = perksObserver;
      this.staticDataService = staticDataService;
      this.levelStateMachine = levelStateMachine;
      this.userHands = userHands;
      userHands.TookedCard += OnTookCard;
      userHands.Refreshed += OnHandsRefresh;
    }

    protected override void Initialize()
    {
      base.Initialize();
      if (perksObserver.IsHavePerk)
        view.SetPerkView(staticDataService.ForPerks().Perk(perksObserver.PerkType));
    }

    protected override void Subscribe()
    {
      base.Subscribe();
      takeButton.onClick.AddListener(TakeCard);
      checkButton.onClick.AddListener(CheckPoints);
      usePerkButton.onClick.AddListener(UsePerk);
    }

    protected override void Cleanup()
    {
      base.Cleanup();
      takeButton.onClick.RemoveListener(TakeCard);
      checkButton.onClick.RemoveListener(CheckPoints);
      usePerkButton.onClick.RemoveListener(UsePerk);
      userHands.TookedCard -= OnTookCard;
      userHands.Refreshed -= OnHandsRefresh;
    }

    public override void Open()
    {
      base.Open();
      Show();
      ShowBaseButtons();
      if (perksObserver.IsHavePerk)
      {
        view.ShowPerk();
        if (perksObserver.IsCanUsePerk)
          UnlockPerk();
        else
          view.SetLockPerk();
      }
      else
        view.HidePerk();
    }

    public void Hide()
    {
      view.Hide();
    }

    public void LockPerk()
    {
      if (perksObserver.IsHavePerk)
        view.SetLockPerk();
    }

    private void TakeCard()
    {
      if (userHands.IsFull || userHands.IsTakingCard)
        return;
      
      audioService.Play(AudioEventType.Click);
      userHands.TakeCard();
    }

    private void UsePerk()
    {
      if (perksObserver.IsCanUsePerk)
      {
        audioService.Play(AudioEventType.Click);
        perksObserver.Use(PerkTargetType.User);
        LockPerk();
      }
    }

    private void OnTookCard(bool isBusy)
    {
      if (perksObserver.IsHavePerk == false)
        return;
      
      if (perksObserver.IsCanUsePerk && isBusy == false)
        UnlockPerk();
      else 
        LockPerk();
    }

    private void OnHandsRefresh() => 
      ShowBaseButtons();

    private void CheckPoints()
    {
      if (userHands.IsTakingCard)
        return;
      
      HideBaseButtons();
      view.HidePerk();
      userHands.CheckAnimation(OnCheck);
    }

    private void OnCheck() => 
      levelStateMachine.Enter<LevelUserCheckState>();

    private void Show() => 
      view.Show();

    private void ShowBaseButtons() => 
      view.ShowButtons();

    private void HideBaseButtons() => 
      view.HideButtons();

    private void UnlockPerk() => 
      view.SetUnlockPerk();
  }
}