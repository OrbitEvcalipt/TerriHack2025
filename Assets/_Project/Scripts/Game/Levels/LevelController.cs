using System.Linq;
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
    }
  }
}