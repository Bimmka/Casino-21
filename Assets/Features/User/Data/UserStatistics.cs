using System.Collections.Generic;

namespace Features.User.Data
{
  public class UserStatistics
  {
    private readonly Dictionary<StatisticsType, int> statistics;

    public UserStatistics()
    {
      statistics = new Dictionary<StatisticsType, int>()
      {
        {StatisticsType.TotalGames, 0},
        {StatisticsType.TotalWins, 0},
        {StatisticsType.WinStreak, 0},
        {StatisticsType.TotalLose, 0},
        {StatisticsType.EasyWin, 0},
        {StatisticsType.MediumWin, 0},
        {StatisticsType.HardWin, 0},
        
      };
    }

    public void Restore(List<int> openStatistics)
    {
      for (int i = 0; i < openStatistics.Count; i++)
      {
        statistics[(StatisticsType) i] = openStatistics[i];
      }
    }

    public void IncStatistic(StatisticsType type) => 
      statistics[type]++;

    public void ResetStatistic(StatisticsType type) => 
      statistics[type] = 0;

    public int Statistic(StatisticsType type) => 
      statistics[type];

    public List<int> TotalStatistics()
    {
      List<int> currentStatistics = new List<int>(statistics.Count);
      foreach (KeyValuePair<StatisticsType,int> statistic in statistics)
      {
        currentStatistics.Add(statistic.Value);
      }

      return currentStatistics;
    }
  }
}