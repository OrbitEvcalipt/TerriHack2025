using UnityEngine;

namespace FunnyBlox
{
  public class ConnectionPath : MonoBehaviour
  {
    public TowerController Tower;

    public void PlaceInTower(TowerController tower) => Tower = tower;
  }
}