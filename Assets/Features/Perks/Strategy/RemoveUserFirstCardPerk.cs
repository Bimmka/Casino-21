using Features.Hands.Scripts.User;
using Features.Perks.Data;
using Features.Services.UI.Factory;
using Features.Services.UI.Windows;
using Features.UI.Windows.Actions.Scripts;

namespace Features.Perks.Strategy
{
  public class RemoveUserFirstCardPerk : PerkStrategy
  {
    private readonly UserHands userHands;
    private readonly IWindowsService windowsService;

    public RemoveUserFirstCardPerk(PerkSettings settings, UserHands userHands, IWindowsService windowsService) : base(settings)
    {
      this.userHands = userHands;
      this.windowsService = windowsService;
    }

    public override bool IsCanBeUsed() => 
      userHands.IsNotEmpty;

    public override void Use()
    {
      ActionsWindow().Hide();
      userHands.RemoveFirstCard(OnRemoved);
    }

    private void OnRemoved()
    {
      ActionsWindow().Open();
    }
    
    private UIActionsWindow ActionsWindow() => 
      ((UIActionsWindow) windowsService.Window(WindowId.Action));
  }
}