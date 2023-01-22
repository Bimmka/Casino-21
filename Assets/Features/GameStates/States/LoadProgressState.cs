using Features.GameStates.States.Interfaces;
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

    public LoadProgressState(IGameStateMachine gameStateMachine, ISaveService saveService, IUserProvider userProvider,
      IAudioService audioService)
    {
      this.gameStateMachine = gameStateMachine;
      this.saveService = saveService;
      this.userProvider = userProvider;
      this.audioService = audioService;
    }
    
    public void Enter()
    {
      audioService.LoadBank(AudioBankType.Master);
      audioService.LoadBank(AudioBankType.UI);
      UserData data =  CreateUserData();
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