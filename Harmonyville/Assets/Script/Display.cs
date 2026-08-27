using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DebugDisplay : MonoBehaviour
{
    public GameObject textDisplay; // Reference to the GameObject containing the Text and Image components
    public Text legacyText; // Reference to the Legacy Text component
    public float displayDuration = 2f; // Duration in seconds to display the text
    private bool canClearText = false; // Flag to check if text can be cleared

    void Start()
    {
        if (legacyText == null || textDisplay == null)
        {
            Debug.LogError("Legacy Text component or Text Display GameObject is not assigned.");
            return;
        }

        legacyText.text = "";
        textDisplay.SetActive(false); // Initially disable the GameObject
        Application.logMessageReceived += HandleLog;
    }

    void OnDestroy()
    {
        Application.logMessageReceived -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        legacyText.text = ""; // Clear existing text
        canClearText = false;

        legacyText.text = logString + "\n"; // Display new text
        textDisplay.SetActive(true); // Enable the GameObject

        StopAllCoroutines();
        StartCoroutine(HideTextAfterDelay());
    }

    IEnumerator HideTextAfterDelay()
    {
        yield return new WaitForSeconds(displayDuration);
        canClearText = true; // Allow text to be cleared after duration
    }

    void Update()
    {
        if (canClearText && Input.GetKeyDown(KeyCode.E))
        {
            legacyText.text = ""; // Clear the text if 'E' is pressed
            textDisplay.SetActive(false); // Disable the GameObject
            canClearText = false;
        }
    }
}
