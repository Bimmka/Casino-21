using Features.GameStates.States.Interfaces;
using Features.Services.Leaderboard;
using Features.Services.Save;
using Features.Services.UserProvider;
using Features.StaticData.Audio;
using Features.User.Data;
using Services.Audio;

namespace Features.GameStates.States
{
  public class LoadProgressState : IState
  {
    private readonly IGameStateMachine gameStateMachine;
    private readonly ISaveService saveService;
    private readonly IUserProvider userProvider;
    private readonly IAudioService audioService;
    private readonly ILeaderboard leaderboard;

    public LoadProgressState(IGameStateMachine gameStateMachine, ISaveService saveService, IUserProvider userProvider,
      IAudioService audioService, ILeaderboard leaderboard)
    {
      this.gameStateMachine = gameStateMachine;
      this.saveService = saveService;
      this.userProvider = userProvider;
      this.audioService = audioService;
      this.leaderboard = leaderboard;
    }
    
    public void Enter()
    {
      audioService.LoadBank(AudioBankType.Master);
      audioService.LoadBank(AudioBankType.UI);
      UserData data =  CreateUserData();
      leaderboard.Login(OnComplete);
      SetToProvider(data);
      SerializedUser user = saveService.LoadPlayer();
      if (IsHaveSavedUser(user))
      {
        InitializeUser(data, user);
        SetMainMenuState();
      }
      else
        SetRegistrationState();
    }

    private void OnComplete(bool obj)
    {
      if (obj)
        leaderboard.SetNickname(userProvider.User.CommonData.Nickname, OnSetName);
    }

    private void OnSetName(bool obj)
    {
      if (obj)
        leaderboard.LogPoints(userProvider.User.PointsData.CurrentPoints, null);
    }

    public void Exit()
    {
      
    }

    private UserData CreateUserData() => 
      new UserData();

    private void SetToProvider(UserData data) => 
      userProvider.Initialize(data);

    private void InitializeUser(UserData data, SerializedUser user) => 
      data.Restore(user);

    private void SetMainMenuState() => 
      gameStateMachine.Enter<MainMenuState>();

    private void SetRegistrationState() => 
      gameStateMachine.Enter<RegistrationState>();

    private bool IsHaveSavedUser(SerializedUser serializedUser) => 
      string.IsNullOrEmpty(serializedUser.Nickname) == false;
  }
}