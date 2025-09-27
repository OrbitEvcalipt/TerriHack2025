using FunnyBlox.Game;
using FunnyBlox.Utils;
using Zenject;

namespace FunnyBlox
{
  public class BootstrapInstaller : MonoInstaller
  {

    public override void InstallBindings()
    {
      BindUtilities();
      BindGameplayFactories();
    }

    

    private void BindUtilities()
    {
      Container
        .Bind<IAssetProvider>()
        .To<AssetProvider>()
        .AsSingle();
    }
    
   

    private void BindGameplayFactories()
    {
      Container
        .Bind<IObjectFactory>()
        .To<ObjectFactory>()
        .AsSingle();
    }
  }
}