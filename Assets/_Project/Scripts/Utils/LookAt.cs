using UnityEngine;

namespace FunnyBlox
{
  public class LookAt : MonoBehaviour
  {
    [SerializeField] private Transform target;

    private Vector3 _targetPosition;

    public void SetTarget(Transform _target) => target = _target;

    private void LateUpdate()
    {
      if (target == null) return;
      _targetPosition = target.position;
      _targetPosition.y = transform.position.y;
      transform.LookAt(_targetPosition);
      transform.Rotate(0f, 180f, 0f);
    }
  }
}