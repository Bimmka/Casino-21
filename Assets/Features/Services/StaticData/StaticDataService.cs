using Features.Services.UI.Factory;
using Features.UI.Windows.Base;
using Features.UI.Windows.Base.Scripts;
using Features.UI.Windows.Data;

namespace Features.Services.StaticData
{
  public class StaticDataService : IStaticDataService
  {
    private readonly WindowsContainer windowsContainer;

    public StaticDataService(WindowsContainer windowsContainer)
    {
      this.windowsContainer = windowsContainer;
    }

    public BaseWindow ForWindow(WindowId id) => 
      windowsContainer.InstantiateData[id];
  }
}