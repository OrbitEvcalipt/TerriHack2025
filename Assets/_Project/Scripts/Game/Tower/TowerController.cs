using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace FunnyBlox
{
  public class TowerController : MonoBehaviour
  {
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
      _unitsFactory= GetComponent<UnitsFactory>();

      Color bodyColor;
      switch (OwnerType)
      {
        case ETowerOwnerType.Neutral:
          bodyColor = Color.grey;
          break;
        case ETowerOwnerType.Player:
          bodyColor = Color.blue;
          break;
        case ETowerOwnerType.Enemy:
          bodyColor = Color.red;
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }

      bodyTransform.GetComponent<Renderer>().material.SetColor("_BaseColor", bodyColor);
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
      if (other.CompareTag("Point"))
      {
        if (OwnerType != ETowerOwnerType.Player && !_isSelected)
        {
          _isTriggered = true;
          EventsHandler.SelectTower(this);
        }
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
        if (OwnerType != ETowerOwnerType.Player && _isTriggered)
        {
          EventsHandler.DeselectTower(this);

          _isTriggered = false;
        }
      }
    }
  }
}