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
    public bool IsConnected { get; set; }

    [SerializeField] private List<Connections> _connections;


    public void UpdateEnabledConnections() =>
      _selectableByFinger.enabled = !(_connections.Count >= _towerController.Level + 1);

    private void Start()
    {
      _towerController = GetComponent<TowerController>();
      _selectableByFinger = GetComponent<LeanSelectableByFinger>();
      _connections = new List<Connections>();
    }

    public void OnCreateConnection(TowerController towerTo)
    {
      IsConnected = true;
      float lenght = Vector3.Distance(_towerController.transform.position, towerTo.transform.position);
      Transform path = Instantiate(_pathTransform, _towerController.transform.position, Quaternion.identity);
      path.LookAt(towerTo.transform.position);
      path.localScale = new Vector3(1f, 1f, lenght);

      _connections.Add(
        new()
        {
          Tower = towerTo,
          ConnectionPath = path
        });

      UpdateEnabledConnections();
    }

    public void OnDestroyConnection(TowerController towerTo)
    {
      IsConnected = false;
      UpdateEnabledConnections();
    }
  }
}