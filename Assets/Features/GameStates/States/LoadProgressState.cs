using Features.Constants;
using Features.GameStates.States.Interfaces;
using Features.Services.Audio;
using Features.Services.Leaderboard;
using Features.Services.Save;
using Features.Services.StaticData;
using Features.Services.UserProvider;
using Features.StaticData.Audio;
using Features.User.Data;
using UnityEngine;

namespace Features.GameStates.States
{
  public class LoadProgressState : IState
  {
    private readonly IGameStateMachine gameStateMachine;
    private readonly ISaveService saveService;
    private readonly IUserProvider userProvider;
    private readonly IAudioService audioService;
    private readonly ILeaderboard leaderboard;
    private readonly IStaticDataService staticDataService;

    public LoadProgressState(IGameStateMachine gameStateMachine, ISaveService saveService, IUserProvider userProvider,
      IAudioService audioService, ILeaderboard leaderboard, IStaticDataService staticDataService)
    {
      this.gameStateMachine = gameStateMachine;
      this.saveService = saveService;
      this.userProvider = userProvider;
      this.audioService = audioService;
      this.leaderboard = leaderboard;
      this.staticDataService = staticDataService;
    }
    
    public void Enter()
    {
      audioService.LoadBank(AudioBankType.Master);
      audioService.LoadBank(AudioBankType.UI);
      audioService.InitializeBuses();

      UserData data =  CreateUserData();
      SetToProvider(data);
      leaderboard.Login(OnLogin);
    }

    public void Exit()
    {
      
    }

    private void OnLogin(bool success)
    {
      if (success && IsLoginBefore())
      {
        SerializedUser user = saveService.LoadPlayer();
        userProvider.User.Restore(user);
        SetMainMenuState();
      }
      else if (success == false)
        DisplayErrorLogin();
      else
        SetRegistrationState();
    }

    private void DisplayErrorLogin()
    {
      
    }

    private UserData CreateUserData() => 
      new UserData(staticDataService.ForPerks());

    private void SetToProvider(UserData data) => 
      userProvider.Initialize(data);
    

    private void SetMainMenuState() => 
      gameStateMachine.Enter<MainMenuState>();

    private void SetRegistrationState() => 
      gameStateMachine.Enter<RegistrationState>();
    
    private bool IsLoginBefore() => 
      PlayerPrefs.HasKey(GameConstants.PlayerNickKey);
  }
}