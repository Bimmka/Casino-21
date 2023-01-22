using Features.Perks.Data;
using Features.Services.UI.Factory;
using Features.UI.Windows.Base.Scripts;

namespace Features.Services.StaticData
{
  public interface IStaticDataService 
  {
    BaseWindow ForWindow(WindowId id);
    PerksSettingsContainer ForPerks();
  }
}