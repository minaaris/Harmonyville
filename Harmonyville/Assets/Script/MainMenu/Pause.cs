using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] GameObject pausemenu;
    public bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    private void TogglePause()
    {
        isPaused = !isPaused;  // Toggle the pause state

        if (isPaused)
        {
            // Pause the game
            Time.timeScale = 0f;
            pausemenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            // Resume the game
            Resume();
        }
    }

    public void Resume()
    {
        isPaused = false;
        pausemenu.SetActive(false);
        Time.timeScale = 1f;
        
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
