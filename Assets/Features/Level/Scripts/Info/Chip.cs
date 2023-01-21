using System;
using UnityEngine;

namespace Features.Level.Scripts.Info
{
  public class Chip : MonoBehaviour
  {
    public event Action<Chip> Hiden; 
    public void SetPosition(Vector3 position) => 
      transform.position = position;

    public void Show() => 
      gameObject.SetActive(true);

    public void Hide()
    {
      gameObject.SetActive(false);
      Hiden?.Invoke(this);
    }
  }
}