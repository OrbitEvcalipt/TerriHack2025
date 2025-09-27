using UnityEngine;

namespace FunnyBlox
{
  public class TowerLevelVisualization : MonoBehaviour
  {
    [SerializeField] private GameObject[] _vizualForLevel;

    [SerializeField] private Renderer[] renderer;

    public void UpdateOwner(ETowerOwnerType ownerType)
    {
      float offset = ownerType switch
      {
        ETowerOwnerType.Enemy => 0.04f,
        ETowerOwnerType.Neutral => 0.07f,
        _ => 0f
      };
      
      foreach (Renderer r in renderer)
      {
        if(r.materials.Length==1)
          r.material.SetTextureOffset("_TextureSample1", new Vector2(offset, 0f));
        else
          r.materials[1].SetTextureOffset("_TextureSample1", new Vector2(offset, 0f));
      }
    }
    
    public void UpdateVisual(int level)
    {
      for (int i = 0; i < _vizualForLevel.Length; i++)
      {
        _vizualForLevel[i].SetActive(i == level);
      }
    }
  }
}