using System;

namespace Features.User.Data
{
  public class UserPointsData
  {
    public int CurrentPoints { get; private set; }

    public event Action<int> Changed;

    public void Initialize(int points)
    {
      CurrentPoints = points;
    }

    public void Add(int points)
    {
      CurrentPoints += points;
      NotifyAboutChange();
    }

    public void Reduce(int points)
    {
      CurrentPoints -= points;
      NotifyAboutChange();
    }

    private void NotifyAboutChange() => 
      Changed?.Invoke(CurrentPoints);
  }
}