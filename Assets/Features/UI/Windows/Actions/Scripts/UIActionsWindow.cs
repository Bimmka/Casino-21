using Features.Hands.Scripts.User;
using Features.Level.Scripts.LevelStates.Machine;
using Features.Level.Scripts.LevelStates.States;
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

    public void Hide()
    {
      view.Hide();
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
      Show();
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
      if (userHands.IsTakingCard)
        return;
      
      levelStateMachine.Enter<LevelUserCheckState>();
      HideButtons();
    }

    private void Show() => 
      view.Show();

    private void ShowButtons() => 
      view.ShowButtons();

    private void HideButtons() => 
      view.HideButtons();
  }
}