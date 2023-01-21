using Features.GameStates;
using Features.GameStates.States;
using Features.Services.Save;
using Features.Services.UserProvider;
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

    [Inject]
    public void Construct(IGameStateMachine gameStateMachine, IUserProvider userProvider, ISaveService saveService)
    {
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
      false;

    private void Register()
    {
      userProvider.User.Initialize(nicknameInputField.text, 0);
      saveService.SavePlayer(userProvider.User);
      gameStateMachine.Enter<MainMenuState>();
    }
  }
}