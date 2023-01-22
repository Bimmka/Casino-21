using Features.Perks.Data;
using Features.Services.UI.Factory;
using Features.UI.Windows.Base.Scripts;
using Features.UI.Windows.Data;

namespace Features.Services.StaticData
{
  public class StaticDataService : IStaticDataService
  {
    private readonly WindowsContainer windowsContainer;
    private readonly PerksSettingsContainer perksSettingsContainer;

    public StaticDataService(WindowsContainer windowsContainer, PerksSettingsContainer perksSettingsContainer)
    {
      this.windowsContainer = windowsContainer;
      this.perksSettingsContainer = perksSettingsContainer;
    }

    public BaseWindow ForWindow(WindowId id) => 
      windowsContainer.InstantiateData[id];

    public PerksSettingsContainer ForPerks() => 
      perksSettingsContainer;
  }
}