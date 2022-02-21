using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("Game Manager");
                go.AddComponent<GameManager>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }
    #endregion

    public CharacterSwitcher characterSwitcher;
    public UiManager UI_Manager;


    public void RestartRound()
    {
        UI_Manager.Score = 0;
        RestartScene();
    }

    public void RestartScene()
    {
        // add here a transition

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void StartRound()
    {
        UI_Manager.Score = 0;
        // Show the player round started in UI
    }
}
