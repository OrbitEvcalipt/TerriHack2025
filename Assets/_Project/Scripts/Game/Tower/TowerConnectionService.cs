using System;
using System.Collections.Generic;
using UnityEngine;

namespace FunnyBlox
{
  public class TowerConnectionService : MonoBehaviour
  {
    [SerializeField] private Transform _pathTransform;
    private List<Transform> _paths;

    private TowerController _towerFrom;
    private TowerController _towerTo;

    private void OnEnable()
    {
      EventsHandler.OnAimingStart += OnSelectTowerFrom;
      EventsHandler.OnAimingStop += OnAimingStop;
      EventsHandler.OnSelectTower += OnSelectTower;
      EventsHandler.OnDeselectTower += OnDeselectTower;
    }

    private void OnSelectTowerFrom(TowerController tower)
    {
      _towerFrom = tower;
    }

    private void Start()
    {
      _paths = new List<Transform>();
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
      if (_towerFrom && _towerTo)
      {
        float lenght = Vector3.Distance(_towerFrom.transform.position, _towerTo.transform.position);

        Transform path = Instantiate(_pathTransform, _towerFrom.transform.position, Quaternion.identity);

        path.LookAt(_towerTo.transform.position);
        path.localScale = new Vector3(1f, 1f, lenght);
        _paths.Add(path);
      }

      _towerFrom = null;
      _towerTo = null;
    }
  }
}