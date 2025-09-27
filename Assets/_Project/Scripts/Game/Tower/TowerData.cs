using UnityEngine;

namespace FunnyBlox
{
  [CreateAssetMenu(menuName = "Data/TowerData", fileName = "TowerData", order = 52)]
  public class TowerData : ScriptableObject
  {
    public EUnitType UnitType;
    public float FactorySpeed;
  }
}