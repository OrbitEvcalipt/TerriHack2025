using UnityEngine;

namespace FunnyBlox
{
  public class TowerController : MonoBehaviour
  {
    public TowerData TowerData;
    public ETowerOwnerType OwnerType;
    public int HitPoints;
    private TowerConnections _towerConnections;
    private UnitsFactory _unitsFactory;

    [SerializeField] private Transform bodyTransform;

    private bool _isSelected;
    private bool _isTriggered;

    private void Start()
    {
      _towerConnections = GetComponent<TowerConnections>();
      _unitsFactory = GetComponent<UnitsFactory>();
      SetOwner(OwnerType);
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
      _towerConnections.OnCreateConnection(towerTo);
      _unitsFactory.StartProduction();
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

          _isTriggered = false;
        }
      }
    }

    private void SetOwner(ETowerOwnerType ownerType)
    {
      OwnerType = ownerType;

      Color bodyColor = OwnerType switch
      {
        ETowerOwnerType.Player => Color.blue,
        ETowerOwnerType.Enemy => Color.red,
        _ => Color.grey
      };

      bodyTransform.GetComponent<Renderer>().material.SetColor("_BaseColor", bodyColor);
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

      return level + 1;
    }

    private void UpdateLevelTower()
    {
      if (HitPoints > 15)
      {
      }
    }
  }
}