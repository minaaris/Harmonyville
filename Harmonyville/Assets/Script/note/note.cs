using UnityEngine;
using UnityEngine.UI;

public class InputFieldController : MonoBehaviour
{
    public InputField inputField;  // Assign this in the inspector
    private bool isInputFieldActive = false;

    void Start()
    {
        // Hide the input field at the start
        inputField.gameObject.SetActive(false);

        // Load the persisted data (if any) when the scene starts
        if (DataManager.Instance != null && inputField != null)
        {
            inputField.text = DataManager.Instance.InputFieldData;
        }
    }

    void Update()
    {
        // Check for the Tab key press
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInputField();
        }
    }

    private void ToggleInputField()
    {
        isInputFieldActive = !isInputFieldActive;  // Toggle the state
        inputField.gameObject.SetActive(isInputFieldActive);

        if (isInputFieldActive)
        {
            // Pause the game
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;  
            Cursor.visible = true;
        }
        else
        {
            // Resume the game and save the input field data
            Time.timeScale = 1;
            SaveInputFieldData();
            
        }
    }

    private void SaveInputFieldData()
    {
        if (DataManager.Instance != null)
        {
            DataManager.Instance.InputFieldData = inputField.text;
        }
    }
}
