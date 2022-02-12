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

    public bool isGameStarted { get; private set; }

    public UiManager UI_Manager;

    private void Start()
    {
        StartCoroutine(StartTheRound(1.5f));
    }

    private IEnumerator StartTheRound(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        isGameStarted = true;
    }

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
}
