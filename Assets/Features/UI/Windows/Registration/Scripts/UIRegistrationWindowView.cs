using TMPro;
using UnityEngine;

namespace Features.UI.Windows.Registration.Scripts
{
  public class UIRegistrationWindowView : MonoBehaviour
  {
    [SerializeField] private TextMeshProUGUI errorText;
    [SerializeField] private string emptyNicknameError;
    [SerializeField] private string invalidLettersError;
    [SerializeField] private string setNameError;
    [SerializeField] private string setPointsError;

    public void HideErrorTip() => 
      errorText.enabled = false;

    public void DisplayEmptyNicknameError() => 
      DisplayTip(emptyNicknameError);

    public void DisplayInvalidLettersError() => 
      DisplayTip(invalidLettersError);

    public void DisplaySetNameError() => 
      DisplayTip(setNameError);

    public void DisplaySetPointsError() => 
      DisplayTip(setPointsError);

    private void DisplayTip(string tip)
    {
      errorText.enabled = true;
      errorText.text = tip;
    }
  }
}