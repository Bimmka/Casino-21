using Features.Services.UI.Factory;
using Features.UI.Windows.Base;

namespace Features.Services.StaticData
{
  public interface IStaticDataService 
  {
    BaseWindow ForWindow(WindowId id);
  }
}