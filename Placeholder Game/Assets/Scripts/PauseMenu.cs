using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool paused = false;
    public GameObject pauseMenu;
    private LevelManager levelManager;

    public GameObject pauseButton;

    private void Start()
    {
        levelManager = GetComponent<LevelManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            switch (paused)
            {
                case true:
                    ResumeGame();
                    break;

                case false:
                    PauseGame();
                    break;
            }
        }
    }

    public void PauseGame()
    {
        pauseButton.SetActive(false);
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        paused = true;
    }

    public void ResumeGame()
    {
        pauseButton.SetActive(true);
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        paused = false;
    }

    public void LoadMenu()
    {
        ResumeGame();
        levelManager.LoadStartMenu();

    }
}
