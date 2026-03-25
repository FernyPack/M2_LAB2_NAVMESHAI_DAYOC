using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public TMP_Text killText;
    public GameObject LevelCompleteUI;
    public GameObject FailedUI;
    public GameObject PauseUI;
    public Button retryButton;
    public Button mainMenuButton;
    public Button resumeButton;

    public int requiredKills = 3;
    private int currentKills = 0;
    private bool isPaused = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SetupUI();
        UpdateKillUI();
        UpdateCursorState(scene.name);
    }

    void SetupUI()
    {
        GameObject go = GameObject.Find("Kills");
        if (go != null) killText = go.GetComponent<TMP_Text>();

        LevelCompleteUI = GameObject.Find("LevelCompleteUI");
        FailedUI = GameObject.Find("Failed");
        PauseUI = GameObject.Find("Pause");

        retryButton = GameObject.Find("RetryButton")?.GetComponent<Button>();
        mainMenuButton = GameObject.Find("MainMenuButton")?.GetComponent<Button>();
        resumeButton = GameObject.Find("ResumeButton")?.GetComponent<Button>();

        if (LevelCompleteUI != null) LevelCompleteUI.SetActive(false);
        if (FailedUI != null) FailedUI.SetActive(false);
        if (PauseUI != null) PauseUI.SetActive(false);

        if (retryButton != null)
        {
            retryButton.onClick.RemoveAllListeners();
            retryButton.onClick.AddListener(RetryLevel);
        }

        if (mainMenuButton != null)
        {
            mainMenuButton.onClick.RemoveAllListeners();
            mainMenuButton.onClick.AddListener(GoToMainMenu);
        }

        if (resumeButton != null)
        {
            resumeButton.onClick.RemoveAllListeners();
            resumeButton.onClick.AddListener(ResumeGame);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            TogglePause();
    }

    public void AddKill()
    {
        currentKills++;
        UpdateKillUI();
        if (currentKills >= requiredKills) WinLevel();
    }

    void UpdateKillUI()
    {
        if (killText != null)
            killText.text = $"Kills: {currentKills}/{requiredKills}";
    }

    public void WinLevel()
    {
        if (LevelCompleteUI != null) LevelCompleteUI.SetActive(true);
        if (FailedUI != null) FailedUI.SetActive(false);
        PauseGame();
    }

    public void LoseLevel()
    {
        if (FailedUI != null) FailedUI.SetActive(true);
        if (LevelCompleteUI != null) LevelCompleteUI.SetActive(false);
        PauseGame();
    }

    void TogglePause()
    {
        isPaused = !isPaused;
        if (PauseUI != null) PauseUI.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;
        UpdateCursorState(SceneManager.GetActiveScene().name);
    }

    void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        UpdateCursorState(SceneManager.GetActiveScene().name);
    }

    void ResumeGame()
    {
        isPaused = false;
        if (PauseUI != null) PauseUI.SetActive(false);
        Time.timeScale = 1f;
        UpdateCursorState(SceneManager.GetActiveScene().name);
    }

    public void RetryLevel()
    {
        Time.timeScale = 1f;
        currentKills = 0;
        SceneManager.LoadScene("Level1");
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        currentKills = 0;
        SceneManager.LoadScene("MainMenu");
    }

    void UpdateCursorState(string sceneName)
    {
        if (sceneName == "MainMenu" || sceneName == "WinScene" || sceneName == "LoseScene" || isPaused)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}