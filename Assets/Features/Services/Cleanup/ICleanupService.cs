namespace Features.Services.Cleanup
{
  public interface ICleanupService
  {
    void CleanupElements();
    void Register(ICleanup cleanup);
    void RemoveElements();
  }
}