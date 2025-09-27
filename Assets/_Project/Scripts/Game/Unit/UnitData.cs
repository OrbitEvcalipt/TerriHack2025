using UnityEngine;

namespace FunnyBlox
{
  [CreateAssetMenu(menuName = "Data/UnitData", fileName = "UnitData", order = 52)]
  public class UnitData : ScriptableObject
  {
    public EUnitType UnitType;
    public int HitPoints;
    public int Attack;
    public float Speed;
  }
}