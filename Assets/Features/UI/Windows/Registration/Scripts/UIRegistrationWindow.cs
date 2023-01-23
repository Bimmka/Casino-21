using System.Text.RegularExpressions;
using Features.Constants;
using Features.GameStates;
using Features.GameStates.States;
using Features.Services.Audio;
using Features.Services.Leaderboard;
using Features.Services.Save;
using Features.Services.UserProvider;
using Features.StaticData.Audio;
using Features.UI.Windows.Base.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Features.UI.Windows.Registration.Scripts
{
  [RequireComponent(typeof(UIRegistrationWindowView))]
  public class UIRegistrationWindow : BaseWindow
  {
    [SerializeField] private UIRegistrationWindowView view;
    [SerializeField] private TMP_InputField nicknameInputField;
    [SerializeField] private Button registrationButton;
    
    private IGameStateMachine gameStateMachine;
    private IUserProvider userProvider;
    private ISaveService saveService;
    private ILeaderboard leaderboard;

    private bool isRegistrating;
    private IAudioService audioService;
    
    private Regex regex = new Regex("^[A-Za-z\\d]+$");

    [Inject]
    public void Construct(IGameStateMachine gameStateMachine, IUserProvider userProvider, ISaveService saveService,
      ILeaderboard leaderboard, IAudioService audioService)
    {
      this.audioService = audioService;
      this.leaderboard = leaderboard;
      this.saveService = saveService;
      this.userProvider = userProvider;
      this.gameStateMachine = gameStateMachine;
    }

    protected override void Subscribe()
    {
      base.Subscribe();
      registrationButton.onClick.AddListener(TryRegistration);
    }

    protected override void Cleanup()
    {
      base.Cleanup();
      registrationButton.onClick.RemoveListener(TryRegistration);
    }

    private void TryRegistration()
    {
      audioService.Play(AudioEventType.Click);
      
      if(isRegistrating)
        return;
      HideErrorTip();
      
      if (IsNicknameEmpty())
      {
        DisplayEmptyNicknameError();
        return;
      }

      if (IsNicknameInvalidLetters())
      {
        DisplayInvalidLettersError();
        return;
      }

      Register();
    }

    private void HideErrorTip() => 
      view.HideErrorTip();

    private void DisplayEmptyNicknameError() => 
      view.DisplayEmptyNicknameError();

    private void DisplayInvalidLettersError() => 
      view.DisplayInvalidLettersError();

    private bool IsNicknameEmpty() => 
      string.IsNullOrEmpty(nicknameInputField.text);

    private bool IsNicknameInvalidLetters() => 
      regex.IsMatch(nicknameInputField.text) == false;

    private void Register()
    {
      isRegistrating = true;
      leaderboard.SetNickname(nicknameInputField.text, OnRegister);
    }

    private void OnRegister(bool success)
    {
      if (success)
        SetDefaultPoints();
      else
      {
        ShowSetNameError();
        isRegistrating = false;
      }
    }

    private void SetDefaultPoints() => 
      leaderboard.LogPoints(GameConstants.PlayerDefaultPoints, OnSetPoints);

    private void ShowSetNameError() => 
      view.DisplaySetNameError();

    private void OnSetPoints(bool success)
    {
      if (success)
      {
        userProvider.User.Initialize(nicknameInputField.text, GameConstants.PlayerDefaultPoints);
        saveService.SavePlayer(userProvider.User);
        gameStateMachine.Enter<MainMenuState>();
      }
      else
      {
        isRegistrating = false;
        ShowSetPointsError();
      }
    }

    private void ShowSetPointsError() => 
      view.DisplaySetPointsError();
  }
}