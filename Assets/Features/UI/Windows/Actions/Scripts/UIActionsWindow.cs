using Features.Hands.Scripts.User;
using Features.Level.Scripts.LevelStates.Machine;
using Features.Level.Scripts.LevelStates.States;
using Features.Perks.Strategy;
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

    [Inject]
    public void Construct(UserHands userHands, ILevelStateMachine levelStateMachine, IStaticDataService staticDataService)
    {
      this.staticDataService = staticDataService;
      this.levelStateMachine = levelStateMachine;
      this.userHands = userHands;
    }

    protected override void Initialize()
    {
      base.Initialize();
      if (userHands.IsHavePerk)
        view.SetPerkView(staticDataService.ForPerks().Perk(userHands.PerkType));
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
    }

    public override void Open()
    {
      base.Open();
      Show();
      ShowBaseButtons();
      if (userHands.IsHavePerk)
      {
        view.ShowPerk();
        if (userHands.IsCanUsePerk)
          view.SetUnlockPerk();
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
      if (userHands.IsHavePerk)
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
      if (userHands.IsCanUsePerk)
      {
        userHands.UsePerk();
        LockPerk();
      }
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
  }
}