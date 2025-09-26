using DG.Tweening;
using UnityEngine;

namespace FunnyBlox
{
  public class CameraTriggerDetector : MonoBehaviour
  {

    [SerializeField]  private Transform target;
    private Material _material;

    private void Start()
    {
      _material = target.GetComponent<Renderer>().material;
    }

    private void OnTriggerEnter(Collider other)
    {
      if (other.tag == "CameraTrigger")
      {
        _material.DOFade(0.2f, 0.5f)
          .SetEase(Ease.InCubic);
      }
    }

    private void OnTriggerExit(Collider other)
    {
      if (other.tag == "CameraTrigger")
      {
        _material.DOFade(1f, 0.5f)
          .SetEase(Ease.InCubic);
      }
    }
  }
}