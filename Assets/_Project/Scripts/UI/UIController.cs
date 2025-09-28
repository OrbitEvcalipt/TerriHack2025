using System.Collections.Generic;
using UnityEngine;

namespace FunnyBlox.UI
{

   public class UIController : MonoSingleton<UIController>
   {
      [SerializeField] private List<BuildingInfo> _buildingInfos;
      [SerializeField] private GameObject _buildingInfoPrefab;
      [SerializeField] private GameObject _buildingInfoRoot;

      public void SetupBuildings(List<TowerController> buildings)
      {
         foreach (var building in buildings)
         {
            var buildingInfo = Instantiate(_buildingInfoPrefab, _buildingInfoRoot.transform).GetComponent<BuildingInfo>();
            building.Visualization.Setup(buildingInfo);
            buildingInfo.SetTarget(building.transform);
            _buildingInfos.Add(buildingInfo);
         }
      }
   }
}
