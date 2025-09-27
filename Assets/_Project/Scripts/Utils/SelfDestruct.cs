using UnityEngine;

namespace FunnyBlox
{
  public class SelfDestruct : MonoBehaviour
  {
    private void Start()
    {
      Destroy(gameObject);
    }
  }
}