using System;
using UnityEngine;

namespace FunnyBlox
{
  public class AimingLine : MonoBehaviour
  {
    private Transform _aimingVisual;

    private void OnEnable()
    {
      EventsHandler.OnAimingStart += OnAimingStart;
      EventsHandler.OnAiming += OnAiming;
      EventsHandler.OnAimingStop += OnAimingStop;
    }

    private void OnDisable()
    {
      EventsHandler.OnAimingStart -= OnAimingStart;
      EventsHandler.OnAiming -= OnAiming;
      EventsHandler.OnAimingStop -= OnAimingStop;
    }

    private void Start()
    {
      _aimingVisual = transform.GetChild(0);
    }

    public void OnAimingStart(TowerController tower)
    {
      transform.position = tower.transform.position;
    }

    public void OnAiming(Vector3 position)
    {
      transform.LookAt(position);
      float lenght = Vector3.Distance(transform.position, position);
      transform.localScale = new Vector3(1f, 1f, lenght);
    }

    public void OnAimingStop(Vector3 position)
    {
      transform.position = new Vector3(1000f, 1000f, 1000f);
    }
  }
}