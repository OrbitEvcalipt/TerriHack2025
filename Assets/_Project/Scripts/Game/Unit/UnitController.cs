using DG.Tweening;
using FunnyBlox.Utils;
using UnityEngine;

namespace FunnyBlox
{
  public class UnitController : MonoBehaviour
  {
    [SerializeField] private UnitData _unitData;

    public EUnitType GetUnitType() => _unitData.UnitType;

    public void Spawn(Vector3 position, Vector3 targetPosition)
    {
      transform.position = position;
      MoveToTarget(targetPosition);
    }

    public void Despawn()
    {
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
        .OnComplete(Despawn);
    }
    
    /// <summary>
    /// Что-то задело юнита
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
      if (other.CompareTag("Tower"))
      {
        
      }
    }
  }
}