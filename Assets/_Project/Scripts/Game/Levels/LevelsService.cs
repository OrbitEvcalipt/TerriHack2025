using FunnyBlox.Game;
using UnityEngine;
using Zenject;

namespace FunnyBlox
{
  public class LevelsService : MonoBehaviour
  {
    [SerializeField] private LevelsListData _levels;
    private int _currentLevel;
    private IObjectFactory _objectFactory;
    private DiContainer _container;

    [Inject]
    public void Construct(DiContainer container, IObjectFactory factory)
    {
      _container = container;
      _objectFactory = factory;
    }

    public void Start()
    {
      _currentLevel = SaveManager.LoadInt(CommonData.PLAYERPREFS_CURRENTLEVEL, 0);
      GameObject level = _objectFactory.Load(_levels.LevelsPath[_currentLevel]);
      _container.InstantiatePrefab(level);
    }

    private void OnEnable()
    {
      EventsHandler.OnGameWin += OnGameWin;
    }

    private void OnDisable()
    {
      EventsHandler.OnGameWin -= OnGameWin;
    }

    private void OnGameWin()
    {
      _currentLevel++;
      if (_currentLevel >= _levels.LevelsPath.Count)
        _currentLevel = 0;
      SaveManager.Save(CommonData.PLAYERPREFS_CURRENTLEVEL, _currentLevel);
    }
  }
}