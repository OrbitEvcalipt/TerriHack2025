using System;
using UnityEngine;

namespace FunnyBlox
{
  public class TowerController : MonoBehaviour
  {
    public CommonData.ETowerType TowerType;

    [SerializeField] private Transform bodyTransform;

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
  }
}