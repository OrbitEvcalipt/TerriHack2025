using System;
using FunnyBlox.Utils;
using UnityEngine;
using UnityEngine.Serialization;

namespace FunnyBlox
{
  public class TowerController : MonoBehaviour
  {
    public TowerData TowerData;
    public ETowerOwnerType OwnerType;
    public int HitPoints;
    public int Level;
    public TowerLevelVisualization Visualization => _visualization;

    [HideInInspector] public TowerConnections TowerConnections;
    private UnitsFactory _unitsFactory;
    private TowerLevelVisualization _visualization;
    [SerializeField] private ParticleSystem _selectionParticles;
    [SerializeField] private ParticleSystem _upgradeParticles;

    private bool _isSelected;
    private bool _isTriggered;

    private void Awake()
    {
      TowerConnections = GetComponent<TowerConnections>();
      _unitsFactory = GetComponent<UnitsFactory>();
      _visualization = GetComponent<TowerLevelVisualization>();
    }

    public void Setup()
    {
      Level = LevelTower();
      _visualization.UpdateVisual(Level, null);
      SetOwner(OwnerType, start: true);
      _selectionParticles.gameObject.SetActive(false);
    }

    /// <summary>
    /// Выбрали башню
    /// </summary>
    public void OnSelect()
    {
      if (OwnerType == ETowerOwnerType.Player)
      {
        _isSelected = true;
        InputHandler.Instance.OnSelectTower(this);
      }
    }

    /// <summary>
    /// Отпустили башю
    /// </summary>
    public void OnDeselect()
    {
      if (OwnerType == ETowerOwnerType.Player)
      {
        _isSelected = false;
        InputHandler.Instance.OnDeselectTower();
      }
    }

    /// <summary>
    /// Создаём соединение с башней
    /// </summary>
    /// <param name="towerTo"></param>
    public void OnCreateConnection(TowerController towerTo)
    {
      TowerConnections.OnCreateConnection(towerTo);
      _unitsFactory.StartProduction();
    }

    public void OnDestroyConnection(Transform path)
    {
      TowerConnections.OnDestroyConnection(path);
    }

    public void OnDestroyConnection(TowerController tower)
    {
      TowerConnections.OnDestroyConnection(tower);
    }

    /// <summary>
    /// Что-то задело башню
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
      switch (other.tag)
      {
        case "Point":
          if (!_isSelected)
          {
            _isTriggered = true;
            EventsHandler.SelectTower(this);
            _selectionParticles.gameObject.SetActive(true);
            AudioManager.PlayOneShotSFX(AudioDatabaseEnum.Game_SelectionSFX);
          }

          break;
        case "Unit":
          if (other.TryGetComponent(out UnitController unit))
          {
            if (unit.TargetTower == this)
            {
              if (OwnerType != unit.OwnerType)
              {
                HitPoints -= unit.Attack;
                if (HitPoints <= 0)
                {
                  HitPoints = Mathf.Abs(HitPoints);
                  SetOwner(unit.OwnerType);
                }
              }
              else
              {
                HitPoints += unit.HitPoints;
              }

              UpdateLevelTower();
              unit.Despawn();
            }

            _visualization.UpdateHitPoints(HitPoints);
          }

          break;
      }
    }

    /// <summary>
    /// Что-то вышло из башни
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
      if (other.CompareTag("Point"))
      {
        if (!_isSelected && _isTriggered)
        {
          EventsHandler.DeselectTower(this);
          _selectionParticles.gameObject.SetActive(false);

          _isTriggered = false;
        }
      }
    }

    private void SetOwner(ETowerOwnerType ownerType, bool start = false)
    {
      OwnerType = ownerType;
      _visualization.UpdateOwner(ownerType, () =>
      {
        if (ownerType == ETowerOwnerType.Player && !start)
        {
          _upgradeParticles.Play();
          AudioManager.PlayOneShotSFX(AudioDatabaseEnum.Game_UpgradeSFX);
        }
      });

      TowerConnections.DestroyAllConnections();
    }

    public int LevelTower()
    {
      int level = 0;

      for (int i = 0; i < TowerData.ProgressionData.Length; i++)
      {
        if (HitPoints >= TowerData.ProgressionData[i].HitPoints)
        {
          level = i;
        }
        else
        {
          break;
        }
      }

      return level;
    }

    private void UpdateLevelTower()
    {
      int level = Level;
      Level = LevelTower();

      if (level != Level && Level < 3)
      {
        _visualization.UpdateVisual(Level, () =>
        {
          _upgradeParticles.Play();
          AudioManager.PlayOneShotSFX(AudioDatabaseEnum.Game_UpgradeSFX);
        });
        TowerConnections.UpdateEnabledConnections();
      }
    }
  }
}