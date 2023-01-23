using Features.Hands.Scripts.User;
using Features.Level.Scripts.LevelStates.Machine;
using Features.Level.Scripts.LevelStates.States;
using Features.Perks.Observer;
using Features.Services.StaticData;
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

    [Inject]
    public void Construct(UserHands userHands, ILevelStateMachine levelStateMachine, IStaticDataService staticDataService,
      PerksObserver perksObserver)
    {
      this.perksObserver = perksObserver;
      this.staticDataService = staticDataService;
      this.levelStateMachine = levelStateMachine;
      this.userHands = userHands;
      userHands.TookedCard += OnTookCard;
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
      
      userHands.TakeCard();
    }

    private void UsePerk()
    {
      if (perksObserver.IsCanUsePerk)
      {
        perksObserver.Use();
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

    private void CheckPoints()
    {
      if (userHands.IsTakingCard)
        return;
      
      levelStateMachine.Enter<LevelUserCheckState>();
      HideBaseButtons();
    }

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