using System.Collections.Generic;
using Lean.Touch;
using UnityEngine;

namespace FunnyBlox
{
  public class TowerConnections : MonoBehaviour
  {
    private TowerController _towerController;
    private LeanSelectableByFinger _selectableByFinger;
    [SerializeField] private Transform _pathTransform;

    public List<Connections> Connections => _connections;

    [SerializeField] private List<Connections> _connections;


    public void UpdateEnabledConnections() =>
      _selectableByFinger.enabled = !(_connections.Count >= _towerController.Level + 1);

    public bool IsEnableConnections => _selectableByFinger.enabled;

    private void Start()
    {
      _towerController = GetComponent<TowerController>();
      _selectableByFinger = GetComponent<LeanSelectableByFinger>();
      _connections = new List<Connections>();
    }

    public void OnCreateConnection(TowerController towerTo)
    {
      float lenght = Vector3.Distance(_towerController.transform.position, towerTo.transform.position);
      Transform path = Instantiate(_pathTransform, _towerController.transform.position, Quaternion.identity);
      path.LookAt(towerTo.transform.position);
      path.localScale = new Vector3(1f, 1f, lenght);
      if (path.TryGetComponent(out ConnectionPath connectionPath))
      {
        connectionPath.PlaceInTower(_towerController);
      }

      _connections.Add(
        new()
        {
          Tower = towerTo,
          ConnectionPath = path
        });

      UpdateEnabledConnections();

      if (towerTo.OwnerType == _towerController.OwnerType)
        towerTo.OnDestroyConnection(_towerController);
    }

    public void OnDestroyConnection(Transform path)
    {
      for (int i = 0; i < _connections.Count; i++)
      {
        if (_connections[i].ConnectionPath == path)
        {
          Destroy(_connections[i].ConnectionPath.gameObject);
          _connections.RemoveAt(i);
          break;
        }
      }

      UpdateEnabledConnections();
    }

    public void OnDestroyConnection(TowerController tower)
    {
      for (int i = 0; i < _connections.Count; i++)
      {
        if (_connections[i].Tower == tower)
        {
          Destroy(_connections[i].ConnectionPath.gameObject);
          _connections.RemoveAt(i);
          break;
        }
      }

      UpdateEnabledConnections();
    }

    public void DestroyAllConnections()
    {
      foreach (var connection in _connections)
      {
        Destroy(connection.ConnectionPath.gameObject);
      }

      _connections.Clear();
    }
  }
}