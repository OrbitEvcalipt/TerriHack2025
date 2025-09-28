using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace FunnyBlox
{
  public class LevelStateHandler : MonoBehaviour
  {
    [SerializeField] private Button startButton;
    [SerializeField] private Button resetButton;


    private void OnEnable()
    {
      EventsHandler.OnGameWin += OnGameWin;
      EventsHandler.OnGameLose += OnGameLose;
    }

    private void OnDisable()
    {
      EventsHandler.OnGameWin -= OnGameWin;
      EventsHandler.OnGameLose += OnGameLose;
    }

    private void Start()
    {
      startButton.onClick.AddListener(GameStart);
      resetButton.onClick.AddListener(OnGameReset);
    }

    private void GameStart()
    {
      EventsHandler.GameStart();
    }

    private void OnGameReset()
    {
      SceneManager.LoadScene(0);
    }

    private void OnGameWin()
    {
    }

    private void OnGameLose()
    {
    }
  }
}