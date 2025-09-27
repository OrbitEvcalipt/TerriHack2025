using System.Collections.Generic;
using UnityEngine;

namespace FunnyBlox
{
  [System.Serializable]
  public class Connections
  {
    public TowerController Tower;
    public Transform ConnectionPath;
  }

  public class TowerConnections : MonoBehaviour
  {
    private TowerController _towerController;
    [SerializeField] private Transform _pathTransform;
    [SerializeField] private List<Connections> _connections;

    private void Start()
    {
      _towerController = GetComponent<TowerController>();
      _connections = new List<Connections>();
    }

    public void OnCreateConnection(TowerController towerTo)
    {
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
    }
  }
}