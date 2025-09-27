using FunnyBlox.Game;
using UnityEngine;
using Zenject;

namespace FunnyBlox
{
  public class UnitsFactory : MonoBehaviour
  {
    [SerializeField] private string[] _prefabPaths;

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
  }
}