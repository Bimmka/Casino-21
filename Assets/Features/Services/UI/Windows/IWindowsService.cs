using Features.Services.UI.Factory;
using Features.Services.UI.Factory.BaseUI;
using Features.UI.Windows.Base;

namespace Features.Services.UI.Windows
{
  public interface IWindowsService
  {
    void Register(IUIFactory factory);
    void Open(WindowId windowId);
    void Close(WindowId windowId);
    BaseWindow Window(WindowId windowId);
  }
}