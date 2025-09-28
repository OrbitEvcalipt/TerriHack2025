﻿using DG.Tweening;
using FunnyBlox.Utils;
using UnityEngine;

namespace FunnyBlox
{
  public class UnitController : MonoBehaviour
  {
    public ETowerOwnerType OwnerType;
    public int HitPoints;
    public bool IsActive;

    public EUnitType GetUnitType() => _unitData.UnitType;
    public int Attack => _unitData.Attack;

    [SerializeField] private UnitData _unitData;
    public TowerController TargetTower => _targetTower;
    private TowerController _targetTower;

    public void Spawn(ETowerOwnerType ownerType, Vector3 position, TowerController target)
    {
      IsActive = true;
      OwnerType = ownerType;
      HitPoints = _unitData.HitPoints;
      transform.position = position;
      _targetTower = target;
      MoveToTarget(target.transform.position);
    }

    public void Despawn()
    {
      IsActive = false;
      _targetTower = null;
      transform.DOKill();
      PoolCollection.Unspawn(transform);
    }

    /// <summary>
    /// Запустили движение юнита
    /// </summary>
    /// <param name="targetPosition"></param>
    private void MoveToTarget(Vector3 targetPosition)
    {
      float distance = Vector3.Distance(transform.position, targetPosition);

      transform.DOMove(targetPosition, distance / _unitData.Speed)
        .SetEase(Ease.Linear)
        .OnComplete(Despawn);
    }

    /// <summary>
    /// Что-то задело юнита
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
      if (other.CompareTag("Unit"))
      {
        if (other.TryGetComponent(out UnitController unit))
        {
          if (!IsActive) return;
          if (unit.OwnerType != OwnerType)
          {
            unit.HitPoints -= Attack;
            if (unit.HitPoints <= 0)
              unit.Despawn();
            HitPoints -= unit.Attack;
            if (HitPoints <= 0)
              Despawn();
          }
        }
      }
    }
  }
}