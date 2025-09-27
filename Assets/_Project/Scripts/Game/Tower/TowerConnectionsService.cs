using UnityEngine;

namespace FunnyBlox
{
  public class TowerConnectionsService : MonoBehaviour
  {
    [SerializeField] private AimingLine _aimingLine;

    private TowerController _towerFrom;
    private TowerController _towerTo;

    private void OnEnable()
    {
      EventsHandler.OnAimingStart += OnSelectTowerFrom;
      EventsHandler.OnAimingStop += OnAimingStop;
      EventsHandler.OnSelectTower += OnSelectTower;
      EventsHandler.OnDeselectTower += OnDeselectTower;
    }

    private void OnDisable()
    {
      EventsHandler.OnAimingStart -= OnSelectTowerFrom;
      EventsHandler.OnAimingStop -= OnAimingStop;
      EventsHandler.OnSelectTower -= OnSelectTower;
      EventsHandler.OnDeselectTower -= OnDeselectTower;
    }

    private void OnSelectTowerFrom(TowerController tower)
    {
      _towerFrom = tower;
    }


    public void OnSelectTower(TowerController tower)
    {
      _towerTo = tower;
    }

    public void OnDeselectTower(TowerController tower)
    {
      _towerTo = null;
    }

    private void OnAimingStop(Vector3 position)
    {
      if (_towerFrom && _towerTo && _aimingLine.AmountTouched == 2)
      {
        _towerFrom.OnCreateConnection(_towerTo);
      }

      _towerFrom = null;
      _towerTo = null;
    }
  }
}