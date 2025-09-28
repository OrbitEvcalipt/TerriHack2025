using System;
using UnityEngine;

namespace FunnyBlox
{
  public class EventsHandler
  {
    
    public static event Action OnStartGame;

    public static void StartGame()
    {
      OnStartGame?.Invoke();
    }
    
    public static event Action OnChangeTowerOwner;

    public static void ChangeTowerOwner()
    {
      OnChangeTowerOwner?.Invoke();
    }
    
    public static event Action OnGameWin;

    public static void GameWin()
    {
      OnGameWin?.Invoke();
    }
    
    public static event Action OnGameLose;

    public static void GameLose()
    {
      OnGameLose?.Invoke();
    }
    
    public static event Action<TowerController[]> OnLevelLoadComplete;

    public static void LevelLoadComplete(TowerController[] towers)
    {
      OnLevelLoadComplete?.Invoke(towers);
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