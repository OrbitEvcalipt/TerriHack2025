using System.Collections.Generic;
using UnityEngine;

namespace FunnyBlox
{
  public class AimingLine : MonoBehaviour
  {
    [SerializeField] private List<Transform> _towersInTouched;
    private Material _aimingLineMaterial;

    public int AmountTouched => _towersInTouched.Count;

    private void OnEnable()
    {
      EventsHandler.OnAimingStart += OnAimingStart;
      EventsHandler.OnAiming += OnAiming;
      EventsHandler.OnAimingStop += OnAimingStop;
    }

    private void OnDisable()
    {
      EventsHandler.OnAimingStart -= OnAimingStart;
      EventsHandler.OnAiming -= OnAiming;
      EventsHandler.OnAimingStop -= OnAimingStop;
    }

    private void Start()
    {
      _aimingLineMaterial = transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material;
      _towersInTouched = new List<Transform>();
    }

    private void OnAimingStart(TowerController tower)
    {
      transform.position = tower.transform.position;
    }

    private void OnAiming(Vector3 position)
    {
      transform.LookAt(position);
      float lenght = Vector3.Distance(transform.position, position);
      transform.localScale = new Vector3(1f, 1f, lenght);
    }

    private void OnAimingStop(Vector3 position)
    {
      transform.position = new Vector3(1000f, 1000f, 1000f);
    }

    private void OnTriggerEnter(Collider other)
    {
      if (other.CompareTag("Tower") || other.CompareTag("Obstacle"))
      {
        _towersInTouched.Add(other.transform);
        ColorizeLine();
      }
    }

    /// <summary>
    /// Что-то вышло из башни
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
      if (other.CompareTag("Tower")|| other.CompareTag("Obstacle"))
      {
        _towersInTouched.Remove(other.transform);
        ColorizeLine();
      }
    }

    private void ColorizeLine()
    {
      if (_towersInTouched.Count > 2)
      {
        _aimingLineMaterial.SetColor("_LineColor", Color.grey);
        _aimingLineMaterial.SetColor("_DotTint", Color.grey);
      }
      else
      {
        _aimingLineMaterial.SetColor("_LineColor", Color.cyan);
        _aimingLineMaterial.SetColor("_DotTint", Color.cyan);
      }
    }
  }
}