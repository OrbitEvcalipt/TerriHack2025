﻿using System;
using Lean.Common;
using Lean.Touch;
using UnityEngine;
using UnityEngine.Serialization;

namespace FunnyBlox
{
  public class InputHandler : MonoSingleton<InputHandler>
  {
    [SerializeField] private LeanDragTranslateAlong _translateAlong;
    [SerializeField] private Transform _pointTransform;

    private bool _isAiming;

    private void Start()
    {
      _translateAlong.enabled = false;
    }

    private void Update()
    {
      if (_isAiming)
        EventsHandler.Aiming(_pointTransform.position);
    }

    public void OnSelectTower(TowerController tower)
    {
      _pointTransform.position = tower.transform.position;
      _translateAlong.enabled = true;
      _isAiming = true;
      EventsHandler.AimingStart(tower);
    }

    public void OnDeselectTower()
    {
      _translateAlong.enabled = false;
      _isAiming = false;
      EventsHandler.AimingStop(_pointTransform.position);
      _pointTransform.position = new Vector3(1000f, 1000f, 1000f);
    }
  }
}