using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public const int EXIST_MAP_N = 2;
    public const int LEVEL_SCENE_OFFSET = 2;
    public int levelReached = 1;
    public string lastDeathCause = "";

    public List<int> grades = new List<int>{ 0, 0, 0 };
    public static GameManager Instance { get; private set; }
    public SceneChanger sceneChanger;
    public InputManager inputManager;

    // on Level Scenes
    public LevelCanvas levelCanvas;
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
        levelCanvas.deathPopUp.SetActive(true);
    }
    public void PlayerDie(string deathMessage = "")
    {
        // 1. Store the cause so the UI can read it when it turns on
        lastDeathCause = deathMessage;

        // 2. You can still handle background logic here if you want
        if (deathMessage == "placeholder1")
        {
            Debug.Log("Hit placeholder 1 condition.");
        }
        else if (deathMessage == "placeholder2")
        {
            Debug.Log("Hit placeholder 2 condition.");
        }

        // --- Default function persists regardless of the above ---
        Debug.Log(sceneChanger.curScene - LEVEL_SCENE_OFFSET);
        Debug.Log(grades.Count);
        grades[sceneChanger.curScene - LEVEL_SCENE_OFFSET]++;

        // This triggers OnEnable() inside DeathPopUp
        levelCanvas.deathPopUp.SetActive(true);
    }

    public void OnPauseKeyInput()
    {
        if (IsPaused) ResumeGame();
        else PauseGame();
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        player?.GetComponent<PlayerSound>()?.Pause();
        levelCanvas.pauseMenu.SetActive(true);
        IsPaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        player?.GetComponent<PlayerSound>()?.Resume();
        levelCanvas.pauseMenu.SetActive(false);
        IsPaused = false;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode = LoadSceneMode.Single)
    {
        Time.timeScale = 1;
        IsPaused = false;

        if (scene.name == "LevelSelect")
        {
            IsInputBlocked = false;
        }
        else if (scene.name == "Title")
        {
            IsInputBlocked = false;
        }
        else
        {
            IsInputBlocked = false;

            player = GameObject.Find("Player");
            levelCanvas = GameObject.Find("LevelCanvas").gameObject.GetComponent<LevelCanvas>();

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
