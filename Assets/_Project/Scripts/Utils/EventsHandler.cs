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

    
  }
}