using Features.Level.Scripts.Info;
using Features.Level.Scripts.LevelStates.Machine;
using Features.Level.Scripts.LevelStates.States;
using Features.Services.GameSettings;
using Features.Services.Leaderboard;
using Features.Services.Save;
using Features.Services.UI.Windows;
using Features.Services.UserProvider;
using Features.UI.Windows.Base.Scripts;
using Features.User.Data;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Features.UI.Windows.Bet.Scripts
{
  [RequireComponent(typeof(UIBetWindowView))]
  public class UIBetWindow : BaseWindow
  {
    [SerializeField] private UIBetWindowView view;
    [SerializeField] private Slider betSlider;
    [SerializeField] private Button betButton;
    
    private UserData user;
    private ILevelStateMachine levelStateMachine;
    private IGameSettings gameSettings;
    private ISaveService saveService;
    private IWindowsService windowsService;
    private LevelInfoDisplayer levelInfoDisplayer;
    private ILeaderboard leaderboard;

    [Inject]
    public void Construct(IUserProvider userProvider, ILevelStateMachine levelStateMachine,
      IGameSettings gameSettings, ISaveService saveService, IWindowsService windowsService,
      LevelInfoDisplayer levelInfoDisplayer, ILeaderboard leaderboard)
    {
      this.leaderboard = leaderboard;
      this.levelInfoDisplayer = levelInfoDisplayer;
      this.windowsService = windowsService;
      this.saveService = saveService;
      this.gameSettings = gameSettings;
      this.levelStateMachine = levelStateMachine;
      user = userProvider.User;
      betSlider.minValue = 0;
      betSlider.maxValue = user.PointsData.CurrentPoints;
      betSlider.wholeNumbers = true;
    }

    protected override void Subscribe()
    {
      base.Subscribe();
      betSlider.onValueChanged.AddListener(OnChangeSlider);
      betButton.onClick.AddListener(ConfirmBet);
    }

    protected override void Cleanup()
    {
      base.Cleanup();
      betSlider.onValueChanged.RemoveListener(OnChangeSlider);
      betButton.onClick.RemoveListener(ConfirmBet);
    }

    private void OnChangeSlider(float sliderValue)
    {
      view.DisplayBet(sliderValue);
    }

    private void ConfirmBet()
    {
      gameSettings.InitializeBet((int)betSlider.value);
      user.PointsData.Reduce((int)betSlider.value);
      leaderboard.LogPoints(user.PointsData.CurrentPoints);
      saveService.SavePlayer(user);
      levelStateMachine.Enter<LevelCardDeckShuffleState>();
      levelInfoDisplayer.DisplayBet(gameSettings.CurrentBet);
      windowsService.Close(ID);
    }
  }
}