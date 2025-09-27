using System;
using Lean.Touch;
using UnityEngine;

namespace FunnyBlox
{
  public class TowerDisconnections : MonoBehaviour
  {
    private int _layerMask;
    private bool _isActive;
    private bool _isAiming;

    private void Start()
    {
      _layerMask = 1 << LayerMask.NameToLayer("Game");
    }

    void OnEnable()
    {
      EventsHandler.OnAimingStart += OnAimingStart;
      EventsHandler.OnAimingStop += OnAimingStop;
      LeanTouch.OnFingerDown += OnFingerDown;
      LeanTouch.OnFingerUp += OnFingerUp;
      LeanTouch.OnFingerUpdate += HandleFingerSet;
    }

    void OnDisable()
    {
      EventsHandler.OnAimingStart -= OnAimingStart;
      EventsHandler.OnAimingStop -= OnAimingStop;
      LeanTouch.OnFingerDown -= OnFingerDown;
      LeanTouch.OnFingerUp -= OnFingerUp;
      LeanTouch.OnFingerUpdate -= HandleFingerSet;
    }

    private void OnAimingStart(TowerController tower) => _isAiming = true;

    private void OnAimingStop(Vector3 position) => _isAiming = false;

    private void OnFingerDown(LeanFinger finger)
    {
      _isActive = true;
    }

    private void OnFingerUp(LeanFinger finger)
    {
      _isActive = false;
    }

    private void HandleFingerSet(LeanFinger finger)
    {
      if (_isActive && !_isAiming)
      {
        Ray ray = Camera.main.ScreenPointToRay(finger.ScreenPosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 200, _layerMask))
        {
          if (hit.transform.TryGetComponent(out ConnectionPath connectionPath))
          {
            connectionPath.Tower.OnDestroyConnection(hit.transform);
          }
        }
      }
    }
  }
}