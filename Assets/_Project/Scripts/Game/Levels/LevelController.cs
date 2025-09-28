using System.Linq;
using FunnyBlox.UI;
using UnityEngine;

namespace FunnyBlox
{
  public class LevelController : MonoBehaviour
  {
    public TowerController[] Towers => _towers;
    [SerializeField] private TowerController[] _towers;

    private void OnEnable()
    {
      EventsHandler.OnChangeTowerOwner += OnChangeTowerOwner;
    }

    private void OnDisable()
    {
      EventsHandler.OnChangeTowerOwner -= OnChangeTowerOwner;
    }

    private void OnChangeTowerOwner()
    {
      if (_towers.All(t => t.OwnerType == ETowerOwnerType.Player))
        EventsHandler.GameWin();
      else if (_towers.All(t => t.OwnerType != ETowerOwnerType.Player))
        EventsHandler.GameLose();
    }

    public void Start()
    {
      _towers = GetComponentsInChildren<TowerController>();
      UIController.Instance.SetupBuildings(_towers.ToList());
      foreach (var tower in _towers)
        tower.Setup();
    }
  }
}