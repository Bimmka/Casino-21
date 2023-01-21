using TMPro;
using UnityEngine;

namespace Features.UI.Windows.Registration.Scripts
{
  public class UIRegistrationWindowView : MonoBehaviour
  {
    [SerializeField] private TextMeshProUGUI errorText;
    [SerializeField] private string emptyNicknameError;
    [SerializeField] private string invalidLettersError;

    public void HideErrorTip() => 
      errorText.enabled = false;

    public void DisplayEmptyNicknameError() => 
      DisplayTip(emptyNicknameError);

    public void DisplayInvalidLettersError() => 
      DisplayTip(invalidLettersError);

    private void DisplayTip(string tip)
    {
      errorText.enabled = true;
      errorText.text = tip;
    }
  }
}