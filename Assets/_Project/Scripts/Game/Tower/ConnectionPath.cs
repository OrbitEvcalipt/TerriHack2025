using UnityEngine;

namespace FunnyBlox
{
  public class ConnectionPath : MonoBehaviour
  {
    [SerializeField] private Settings _settings;
    public TowerController Tower;
    private Material _pathMaterial;


    public void PlaceInTower(TowerController tower)
    {
      _pathMaterial = transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material;


      Tower = tower;

      if (tower.OwnerType == ETowerOwnerType.Player)
      {
        ColorizeLine(_settings.ConnectionPathColor[0]);
      }
      else if (tower.OwnerType == ETowerOwnerType.Enemy)
      {
        ColorizeLine(_settings.ConnectionPathColor[1]);
        
      }
    }

    private void ColorizeLine(Color color)
    {
        _pathMaterial.SetColor("_LineColor", color);
        _pathMaterial.SetColor("_DotTint", color);
      
    }
  }
}