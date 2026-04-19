using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public const int EXIST_MAP_N = 1;
    public const int LEVEL_SCENE_OFFSET = 1;

    public List<int> grades = new List<int>{ 1, 1 };
    public static GameManager Instance { get; private set; }
    public SceneChanger sceneChanger;
    public InputManager inputManager;

    // on Level Scenes
    public GameObject pauseMenu;
    public GameObject deathPopUp;
    public GameObject player;

    public bool IsPaused { get; private set; }
    public bool IsInputBlocked { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        IsPaused = false;
    }

    void Start()
    {
        sceneChanger.Init();

        inputManager.PauseKey += OnPauseKeyInput;
        SceneManager.sceneLoaded += OnSceneLoaded;

        OnSceneLoaded(SceneManager.GetActiveScene());
    }

    void Update()
    {
        inputManager.GetInput();
    }

    public void PlayerDie()
    {
        Debug.Log(sceneChanger.curScene - LEVEL_SCENE_OFFSET);

        Debug.Log(grades.Count);
        grades[sceneChanger.curScene - LEVEL_SCENE_OFFSET]++;
        deathPopUp.SetActive(true);
    }

    public void OnPauseKeyInput()
    {
        if (IsPaused) ResumeGame();
        else PauseGame();
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        IsPaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        IsPaused = false;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode = LoadSceneMode.Single)
    {
        Time.timeScale = 1;
        IsPaused = false;

        if (scene.name == "LevelSelect")
        {
            IsInputBlocked = true;
        }
        else if (scene.name == "Title")
        {
            IsInputBlocked = false;
        }
        else
        {
            IsInputBlocked = false;

            player = GameObject.Find("Player");
            pauseMenu = GameObject.Find("LevelCanvas").transform.Find("PauseMenu").gameObject;
            deathPopUp = GameObject.Find("LevelCanvas").transform.Find("DeathPopUp").gameObject;

            player.GetComponent<PlayerMovement>().Init();
        }
    }

    public void NewGameOrContinue()
    {
        sceneChanger.GoToNthLevel(1);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
