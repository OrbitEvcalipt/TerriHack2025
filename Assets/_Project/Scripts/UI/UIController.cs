using System.Collections.Generic;
using FunnyBlox.GUI;
using UnityEngine;

namespace FunnyBlox.UI
{

   public class UIController : MonoSingleton<UIController>
   {
      [SerializeField] private List<BuildingInfo> _buildingInfos;
      [SerializeField] private GameObject _buildingInfoPrefab;
      [SerializeField] private GameObject _buildingInfoRoot;
      [SerializeField] private GUICanvasGroup _winScreen;
      [SerializeField] private GUICanvasGroup _loseScreen;

      public GUICanvasGroup WinScreen => _winScreen;
      public GUICanvasGroup LoseScreen  => _loseScreen;
      

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
