using System.Collections;
using FunnyBlox.Game;
using UnityEngine;
using Zenject;

namespace FunnyBlox
{
  public class UnitsFactory : MonoBehaviour
  {
    [SerializeField] private Settings _settings;
    private TowerController _towerController;
    private TowerConnections _towerConnections;
    private Coroutine _productionCoroutine;

    private IObjectFactory _objectFactory;

    [Inject]
    public void Construct(IObjectFactory factory)
    {
      _objectFactory = factory;
    }

    public void SpawnUnit(EUnitType unitType, ETowerOwnerType ownerType, Vector3 position, TowerController target)
    {
      UnitController unit = _objectFactory.CreateObject<UnitController>(_settings.UnitPrefabPaths[(int)unitType]);

      unit.Spawn(ownerType, position, target);
    }

    private void Start()
    {
      _towerController = GetComponent<TowerController>();
      _towerConnections = GetComponent<TowerConnections>();
    }

    public void StartProduction()
    {
      if (_productionCoroutine == null)
        _productionCoroutine = StartCoroutine(ProductionRoutine());
    }

    public void StopProduction()
    {
      if (_productionCoroutine != null)
      {
        StopCoroutine(_productionCoroutine);
      }

      _productionCoroutine = null;
    }

    private IEnumerator ProductionRoutine()
    {
      WaitForSeconds wait =
        new WaitForSeconds(1f / _towerController.TowerData.ProgressionData[_towerController.Level]
          .FactorySpeed);

      while (true)
      {
        foreach (var connection in _towerConnections.Connections)
        {
          SpawnUnit(_towerController.TowerData.UnitType, _towerController.OwnerType, transform.position,
            connection.Tower);
        }

        yield return wait;
      }
    }
  }
}