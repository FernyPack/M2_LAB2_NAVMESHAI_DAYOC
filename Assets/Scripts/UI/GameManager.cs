using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public TMP_Text killText;
    public GameObject LevelCompleteUI;
    public GameObject Failed;
    public GameObject pauseUI;
    public Button retryButton;
    public Button mainMenuButton;

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
    }

    void Start()
    {
        SetupUI();
        UpdateKillUI();
        HandleCursorState();
    }

    void SetupUI()
    {
        if (killText == null)
        {
            GameObject go = GameObject.Find("KillText");
            if (go != null) killText = go.GetComponent<TMP_Text>();
        }

        if (LevelCompleteUI != null) LevelCompleteUI.SetActive(false);
        if (Failed != null) Failed.SetActive(false);
        if (pauseUI != null) pauseUI.SetActive(false);

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
    }

    public void AddKill()
    {
        currentKills++;
        UpdateKillUI();
        if (currentKills >= requiredKills)
            WinLevel();
    }

    void UpdateKillUI()
    {
        if (killText != null)
            killText.text = $"Kills: {currentKills}/{requiredKills}";
    }

    public void WinLevel()
    {
        if (LevelCompleteUI != null)
            LevelCompleteUI.SetActive(true);

        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void LoseLevel()
    {
        if (Failed != null)
            Failed.SetActive(true);

        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            TogglePause();
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (pauseUI != null)
            pauseUI.SetActive(isPaused);

        Time.timeScale = isPaused ? 0f : 1f;
        Cursor.visible = isPaused;
        Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
    }

    public void RetryLevel()
    {
        Time.timeScale = 1f;
        currentKills = 0;
        if (LevelCompleteUI != null) LevelCompleteUI.SetActive(false);
        if (Failed != null) Failed.SetActive(false);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.transform.position = Vector3.zero;
            player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        currentKills = 0;
        if (LevelCompleteUI != null) LevelCompleteUI.SetActive(false);
        if (Failed != null) Failed.SetActive(false);
    }

    void HandleCursorState()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}