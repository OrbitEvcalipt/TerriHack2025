using UnityEngine;

namespace FunnyBlox
{
  [System.Serializable]
  public class TowerProgressionData
  {
    public int HitPoints;
    public float FactorySpeed;
  }

  [CreateAssetMenu(menuName = "Data/TowerData", fileName = "TowerData", order = 52)]
  public class TowerData : ScriptableObject
  {
    public EUnitType UnitType;
    public TowerProgressionData[] ProgressionData;
  }
}