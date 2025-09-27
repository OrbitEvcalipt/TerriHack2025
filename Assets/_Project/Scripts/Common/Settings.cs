﻿using UnityEngine;

namespace FunnyBlox
{
  [CreateAssetMenu(menuName = "Data/Settings", fileName = "Settings", order = 52)]
  public class Settings : ScriptableObject
  {
    public string[] UnitPrefabPaths;
  }
}