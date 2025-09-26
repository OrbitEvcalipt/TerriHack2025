using UnityEngine;
using Sirenix.OdinInspector;

namespace FunnyBlox
{
  public class Follower : MonoBehaviour
  {
    [LabelText("Цель следования")] [SuffixLabel("_target", Overlay = true)]
    public Transform _target;

    [LabelText("Скорость следования")] [SuffixLabel("_smoothing", Overlay = true)] [SerializeField] [Range(0.001f, 1f)]
    private float _smoothing = 0.5f;

    [LabelText("Использовать смещение стартовой позиции")] [SuffixLabel("_useOffset", Overlay = true)] [SerializeField]
    private bool _useOffset = true;

    [FoldoutGroup("Заморооженные оси")] [LabelText("Ось X")] [SuffixLabel("_freezeX", Overlay = true)] [SerializeField]
    private bool _freezeX = false;

    [FoldoutGroup("Заморооженные оси")] [LabelText("Ось Y")] [SuffixLabel("_freezeY", Overlay = true)] [SerializeField]
    private bool _freezeY = false;

    [FoldoutGroup("Заморооженные оси")] [LabelText("Ось Z")] [SuffixLabel("_freezeZ", Overlay = true)] [SerializeField]
    private bool _freezeZ = false;

    private Transform _transform;
    private Vector3 _targetPosition;
    private Vector3 _position;
    private Vector3 _offset;


    private void Start()
    {
      _transform = transform;

      Initialize();

      UpdateState();
    }

    private void Initialize()
    {
      if (!_target) return;
      if (_useOffset)
        _offset = _transform.position - _target.position;
      else
        _offset = Vector3.zero;
    }

    private void LateUpdate()
    {
      UpdateState();
    }

    private void UpdateState()
    {
      if (!_target)
      {
        this.enabled = false;
        return;
      }

      _targetPosition = _target.position + _offset;

      _position = Vector3.Lerp(_transform.position, _targetPosition, _smoothing);

      if (_freezeX) _position.x = 0f;
      if (_freezeY) _position.y = 0f;
      if (_freezeZ) _position.z = 0f;
      _transform.position = _position;
    }

    public void SetPosition()
    {
      if (!_target) return;

      _transform.position = _target.position + _offset;
    }

    public void SetTarget(Transform target)
    {
      _target = target;
      Initialize();
      this.enabled = true;
    }
  }
}