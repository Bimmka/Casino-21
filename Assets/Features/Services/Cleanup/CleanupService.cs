using System.Collections.Generic;

namespace Features.Services.Cleanup
{
  public class CleanupService : ICleanupService
  {
    private readonly List<ICleanup> cleanups;

    public CleanupService()
    {
      cleanups = new List<ICleanup>(20);
    }

    public void CleanupElements()
    {
      for (int i = 0; i < cleanups.Count; i++)
      {
        cleanups[i].Cleanup();
      }
    }

    public void Register(ICleanup cleanup)
    {
      cleanups.Add(cleanup);
    }

    public void RemoveElements()
    {
      cleanups.Clear();
    }
  }
}