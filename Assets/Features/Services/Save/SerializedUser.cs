using System.Collections.Generic;
using Features.Perks.Data;

namespace Features.Services.Save
{
  public struct SerializedUser
  {
    public string Nickname;
    public int Points;
    public List<PerkType> Perks;
    public List<int> Statistics;
  }
}