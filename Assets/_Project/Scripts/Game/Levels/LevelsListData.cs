using System.Collections.Generic;
using UnityEngine;

namespace FunnyBlox
{
  [CreateAssetMenu(menuName = "Data/LevelsListData", fileName = "LevelsListData", order = 52)]
  public class LevelsListData : ScriptableObject
  {
    public List<string> LevelsPath;
  }
}