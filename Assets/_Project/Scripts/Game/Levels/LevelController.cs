using UnityEngine;

namespace FunnyBlox
{
  public class LevelController : MonoBehaviour
  {
    public TowerController[] Towers => _towers;
    [SerializeField] private TowerController[] _towers;
    
    public void Start()
    {
      _towers = GetComponentsInChildren<TowerController>();
    }
  }
}