using UnityEngine;

namespace FunnyBlox.Utils
{
  public interface IAssetProvider
  {
    GameObject LoadAsset(string path);
    T LoadAsset<T>(string path) where T : Component;
  }
}