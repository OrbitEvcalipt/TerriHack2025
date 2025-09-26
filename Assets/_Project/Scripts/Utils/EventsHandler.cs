using System;
using UnityEngine;

namespace FunnyBlox
{
  public class EventsHandler
  {
    public static event Action OnGameStateChanged;

    public static void GameStateChanged()
    {
      OnGameStateChanged?.Invoke();
    }

    public static event Action<TowerController> OnAimingStart;

    public static void AimingStart(TowerController tower)
    {
      OnAimingStart?.Invoke(tower);
    }
    
    public static event Action<Vector3> OnAiming;

    public static void Aiming(Vector3 position)
    {
      OnAiming?.Invoke(position);
    }
    
    public static event Action<Vector3> OnAimingStop;

    public static void AimingStop(Vector3 position)
    {
      OnAimingStop?.Invoke(position);
    }
    
    public static event Action<TowerController> OnSelectTower;

    public static void SelectTower(TowerController tower)
    {
      OnSelectTower?.Invoke(tower);
    }
    
    public static event Action<TowerController> OnDeselectTower;

    public static void DeselectTower(TowerController tower)
    {
      OnDeselectTower?.Invoke(tower);
    }
      
      
  }
}