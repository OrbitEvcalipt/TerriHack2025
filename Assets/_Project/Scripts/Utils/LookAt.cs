using UnityEngine;

namespace FunnyBlox
{
  public class LookAt : MonoBehaviour
  {
    [SerializeField] private Transform target;

    private Vector3 _targetPosition;

    private void LateUpdate()
    {
      _targetPosition = target.position;
      _targetPosition.y = transform.position.y;
      transform.LookAt(_targetPosition);
      transform.Rotate(0f, 180f, 0f);
    }
  }
}