using FunnyBlox.Utils;
using UnityEngine;
using Zenject;

namespace FunnyBlox.Game
{
  public class ObjectFactory : IObjectFactory
  {
    private IAssetProvider _assetProvider;

    [Inject]
    private void Construct(IAssetProvider assetProvider)
    {
      _assetProvider = assetProvider;
    }

    public GameObject Load(string prefabPath)
    {
      return _assetProvider.LoadAsset(prefabPath);
    }

    public T CreateObject<T>(string prefabPath) where T : Component
    {
      Component component = Load(prefabPath).GetComponent<T>();
      var obj = (T)PoolCollection.Spawn(component, Vector3.zero, Quaternion.identity).Component;
      return obj;
    }
  }
}