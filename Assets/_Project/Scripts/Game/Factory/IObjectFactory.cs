using UnityEngine;

namespace FunnyBlox.Game
{
  public interface IObjectFactory
  {
    GameObject Load(string prefabPath);
    T CreateObject<T>(string prefabPath) where T : Component;
  }
}