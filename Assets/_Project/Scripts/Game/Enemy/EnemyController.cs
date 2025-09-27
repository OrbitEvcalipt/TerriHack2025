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
        .Where(t => t != null && t.OwnerType == ETowerOwnerType.Enemy)
        .ToList();
      if (enemies.Count == 0)
      {
        Debug.Log("No enemies found");
      }

      var towerFrom = enemies[Random.Range(0, enemies.Count)];
      if (towerFrom.IsEnableConnections)
      {
        var towerTo = _towers
          .Where(t => t != null && t != towerFrom)
          .OrderBy(t => Vector3.Distance(towerFrom.transform.position, t.transform.position))
          .FirstOrDefault();

        towerFrom.OnCreateConnection(towerTo);
      }
    }
  }
}