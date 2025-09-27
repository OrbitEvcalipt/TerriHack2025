using System.Collections;
using FunnyBlox.Game;
using UnityEngine;
using Zenject;

namespace FunnyBlox
{
  public class UnitsFactory : MonoBehaviour
  {
    public TowerData TowerData;
    [SerializeField] private string[] _prefabPaths;
    private TowerController _towerController;
    private TowerConnections _towerConnections;
    private Coroutine _productionCoroutine;

    private IObjectFactory _objectFactory;

    [Inject]
    public void Construct(IObjectFactory factory)
    {
      _objectFactory = factory;
    }

    public void SpawnUnit(EUnitType unitType, Vector3 position, Vector3 targetPosition)
    {
      UnitController unit = _objectFactory.CreateObject<UnitController>(_prefabPaths[(int)unitType]);

      unit.Spawn(position, targetPosition);
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
      WaitForSeconds wait = new WaitForSeconds(1f / TowerData.FactorySpeed);

      while (true)
      {
        foreach (var connection in _towerConnections.Connections)
        {
          SpawnUnit(TowerData.UnitType, transform.position, connection.Tower.transform.position);
        }

        yield return wait;
      }
    }
  }
}