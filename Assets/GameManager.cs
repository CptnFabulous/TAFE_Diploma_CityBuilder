using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance = null;

    private bool _isSetup = false;

    private float _gameTime = 0f;

    public enum GameStates
    {
        Main,
        Game,
        Pause,
        End,
    }

    private Stack<GameStates> _gameState = new Stack<GameStates>();

    public GameStates GameState
    {
        get { return _gameState.Peek(); }
    }

    public float GameTime
    {
        get { return _gameTime; }
    }

    public void OnDestroy()
    {
        _instance = null;
    }

    public static GameManager Instance
    {
        get
        {
            if (null == _instance)
            {
                var instances = FindObjectsOfType<GameManager>();
                var count = instances.Length;
                if (count > 0)
                {
                    for (var i = 1; i < count; ++i)
                    {
                        Destroy(instances[i]);
                    }

                    _instance = instances[0];
                }
                else
                {
                    _instance = new GameObject("GameManager").AddComponent<GameManager>();
                    _instance.Setup();
                }
            }

            return _instance;
        }
    }

    public void Start()
    {
        if (this == Instance)
        {
            Setup();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Update()
    {
        if (GameState == GameStates.Game)
        {
            _gameTime += Time.deltaTime;
        }
    }

    private void Setup()
    {
        if (!_isSetup)
        {
            _gameState.Push(GameStates.Main);

            DontDestroyOnLoad(gameObject);

            _isSetup = true;
        }
    }

    public void StartGame()
    {
        _gameState.Push(GameStates.Game);
        _gameTime = 0f;

        UnityEngine.SceneManagement.SceneManager.LoadScene("Level");
    }

    private void GameOver()
    {
        _gameState.Push(GameStates.End);

        // End game
    }

    public void PauseGame()
    {
        if (GameState == GameStates.Game)
        {
            _gameState.Push(GameStates.Pause);
            Time.timeScale = 0f;

            // Open pause menu
        }
    }

    public void ResumeGame()
    {
        if (GameState == GameStates.Pause)
        {
            Time.timeScale = 1f;
            _gameState.Pop();
        }
    }

    public void BackToMain()
    {
        _gameState.Clear();
        _gameState.Push(GameStates.Main);
        Time.timeScale = 1f;

        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
