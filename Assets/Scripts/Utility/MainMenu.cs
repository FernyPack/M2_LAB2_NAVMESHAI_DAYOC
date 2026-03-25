using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject controlsPanel;

    public string level1Scene = "Level1";

    void Start()
    {
        ShowMainMenu();
    }

    public void ShowMainMenu()
    {
        mainMenuPanel.SetActive(true);
        controlsPanel.SetActive(false);
    }

    public void ShowControls()
    {
        mainMenuPanel.SetActive(false);
        controlsPanel.SetActive(true);
    }

    public void ShowLevelSelect()
    {
        mainMenuPanel.SetActive(false);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(level1Scene);
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene(level1Scene);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game!");
        Application.Quit();
    }
}