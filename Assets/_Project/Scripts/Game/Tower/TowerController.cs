using System;
using UnityEngine;

namespace FunnyBlox
{
  public class TowerController : MonoBehaviour
  {
    public CommonData.ETowerType TowerType;

    [SerializeField] private Transform bodyTransform;

    private bool _isSelected;
    private bool _isTriggered;

    private void Start()
    {
      Color bodyColor;
      switch (TowerType)
      {
        case CommonData.ETowerType.Neutral:
          bodyColor = Color.grey;
          break;
        case CommonData.ETowerType.Player:
          bodyColor = Color.blue;
          break;
        case CommonData.ETowerType.Enemy:
          bodyColor = Color.red;
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }

      bodyTransform.GetComponent<Renderer>().material.SetColor("_BaseColor", bodyColor);
    }

    public void OnSelect()
    {
      if (TowerType == CommonData.ETowerType.Player)
      {
        _isSelected = true;
        InputHandler.Instance.OnSelectTower(this);
      }
    }

    public void OnDeselect()
    {
      if (TowerType == CommonData.ETowerType.Player)
      {
        _isSelected = false;
        InputHandler.Instance.OnDeselectTower();
      }
    }

    private void OnTriggerEnter(Collider other)
    {
      if (other.tag == "Point")
      {
        if (TowerType != CommonData.ETowerType.Player && !_isSelected)
        {
          _isTriggered = true;
          EventsHandler.SelectTower(this);
        }
      }
    }

    private void OnTriggerExit(Collider other)
    {
      if (other.tag == "Point")
      {
        if (TowerType != CommonData.ETowerType.Player && _isTriggered)
        {
          EventsHandler.DeselectTower(this);
          
          _isTriggered = false;
        }
      }
    }
  }
}