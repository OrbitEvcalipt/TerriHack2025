using System.Collections;
using System.Linq;
using UnityEngine;

namespace FunnyBlox
{
  public class EnemyController : MonoBehaviour
  {
    [SerializeField] private Transform level;

    [SerializeField] private TowerController[] _towers;

    public void Start()
    {
      _towers = level.GetComponentsInChildren<TowerController>();

      StartCoroutine(AttackDurationRoutine());
    }

    private IEnumerator AttackDurationRoutine()
    {
      while (true)
      {
        yield return new WaitForSeconds(Random.Range(1f, 2f));
        ConnectTower();
      }
    }

    private void ConnectTower()
    {
      var enemies = _towers
        .Where(t => t != null
                    && t.OwnerType == ETowerOwnerType.Enemy
                    && t.TowerData.UnitType != EUnitType.UnitType_99)
        .ToList();
      if (enemies.Count == 0)
      {
        Debug.Log("No enemies found");
      }

      var towerFrom = enemies[Random.Range(0, enemies.Count)];
      if (towerFrom.TowerConnections.IsEnableConnections)
      {
        var towerTo = _towers
          .Where(t => t != null //не пустая
                      && t != towerFrom // не башня из которой запускаем соединение
                      && t.OwnerType != ETowerOwnerType.Enemy // башня не принадлежит врагу
                      && !towerFrom.TowerConnections.Connections.Any(c => c.Tower == t) //башня ещё не присоединена к вражеской 
                      )
          .OrderBy(t => Vector3.Distance(towerFrom.transform.position, t.transform.position)) // отсортировали по удалённости от фражеской
          .FirstOrDefault();

        if (towerTo != null)
          towerFrom.OnCreateConnection(towerTo);
      }
    }
  }
}