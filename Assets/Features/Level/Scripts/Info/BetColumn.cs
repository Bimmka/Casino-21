using System;
using System.Collections.Generic;
using UnityEngine;

namespace Features.Level.Scripts.Info
{
  public class BetColumn : MonoBehaviour
  {
    [SerializeField] private Transform startPoint;
    [SerializeField] private Vector3 moveShift;

    private int maxCount;
    private List<Chip> chips;

    public bool IsFull => chips.Count >= maxCount;

    public void Initialize(int maxElements) => 
      maxCount = maxElements;

    private void Awake()
    {
      chips = new List<Chip>(maxCount);
    }

    public void AddChip(Chip chip)
    {
      chip.SetPosition(ChipPosition(chips.Count));
      chips.Add(chip);
    }

    public void Release()
    {
      for (int i = 0; i < chips.Count; i++)
      {
        chips[i].Hide();
      }
      chips.Clear();
    }

    private Vector3 ChipPosition(int index) => 
      startPoint.position + (index * moveShift);
  }
}