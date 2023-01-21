using Features.Hands.Scripts.User;
using Features.Level.LevelStates.Machine;
using Features.Level.LevelStates.States;
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
    
    private UserHands userHands;
    private ILevelStateMachine levelStateMachine;

    [Inject]
    public void Construct(UserHands userHands, ILevelStateMachine levelStateMachine)
    {
      this.levelStateMachine = levelStateMachine;
      this.userHands = userHands;
    }

    protected override void Subscribe()
    {
      base.Subscribe();
      takeButton.onClick.AddListener(TakeCard);
      checkButton.onClick.AddListener(CheckPoints);
    }

    protected override void Cleanup()
    {
      base.Cleanup();
      takeButton.onClick.RemoveListener(TakeCard);
      checkButton.onClick.RemoveListener(CheckPoints);
    }

    public override void Open()
    {
      base.Open();
      ShowButtons();
    }

    private void TakeCard()
    {
      if (userHands.IsFull || userHands.IsTakingCard)
        return;
      
      userHands.TakeCard();
    }

    private void CheckPoints()
    {
      levelStateMachine.Enter<LevelUserCheckState>();
      HideButtons();
    }

    private void ShowButtons() => 
      view.ShowButtons();

    private void HideButtons() => 
      view.HideButtons();
  }
}