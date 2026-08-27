using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Image img;
    public GameObject fadeBG;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void PlayGame()
    {
        fadeBG.SetActive(true);
        Debug.Log("fade works");
        StartCoroutine(FadeIn());

    }

    public void GoToSettingsMenu()
    {
        SceneManager.LoadScene("SettingsMenu");
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Player has quit the game");
    }

    IEnumerator FadeIn()
    {

        // loop over 1 second
        for (float i = 0; i <= 1; i += Time.deltaTime)
        {
            // set color with i as alpha
            img.color = new Color(1, 1, 1, i);
            Debug.Log("fade works 2");
            StartCoroutine(StartGame());
            yield return null;
        }

    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(3);
        Debug.Log("fade works3");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
