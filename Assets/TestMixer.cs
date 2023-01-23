using System;
using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;

public class TestMixer : MonoBehaviour
{
  [Range(-80f, 10f)]
  [SerializeField] private float value;

  private Bus bus;

  private void Start()
  {
    bus = FMODUnity.RuntimeManager.GetBus("Master Bus:/Music");
  }

  private void Update()
  {
    bus.setVolume(DecibelToLinear(value));
  }

  private float DecibelToLinear(float dB) => 
    Mathf.Pow(10, dB / 20);
}
