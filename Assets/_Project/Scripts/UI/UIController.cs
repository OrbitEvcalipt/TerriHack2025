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
      [SerializeField] private GUICanvasGroup _infoRootScreen;
      [SerializeField] private GUICanvasGroup _winScreen;
      [SerializeField] private GUICanvasGroup _loseScreen;
      [SerializeField] private GUICanvasGroup _startScreen;
      [SerializeField] private GUICanvasGroup _tutorialScreen;

      public GUICanvasGroup WinScreen => _winScreen;
      public GUICanvasGroup LoseScreen  => _loseScreen;
      public GUICanvasGroup InGameScreen => _infoRootScreen;
      public GUICanvasGroup StartScreen => _startScreen;

      public GUICanvasGroup TutorialScreen => _tutorialScreen;


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

      public void ShowStartScreen()
      {
         InGameScreen.Hide();
         WinScreen.Hide();
         LoseScreen.Hide();
         StartScreen.Show();
      }

      public void HideStartScreen()
      {
         InGameScreen.Show();
         StartScreen.Hide();
         EventsHandler.GameStart();
         ShowTutorialScreen();
      }

      private void ShowTutorialScreen() => TutorialScreen.Show(() => TutorialScreen.Hide());
   }
}
