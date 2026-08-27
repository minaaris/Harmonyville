using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] private Dropdown[] questions; // Assign these in the inspector
    [SerializeField] private Button finishButton;
    [SerializeField] private Button returnButton; // New button to return to another scene
    [SerializeField] private string winningSceneName; // The name of the winning scene
    [SerializeField] private string losingSceneName; // The name of the losing scene
    [SerializeField] private string returnSceneName; // The name of the return scene

    [SerializeField] private int[] correctAnswers; // Correct answers for each question

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        finishButton.onClick.AddListener(CheckAnswers);
        returnButton.onClick.AddListener(ReturnToScene); // Add listener for return button
    }

    private void CheckAnswers()
    {
        bool allCorrect = true;
        for (int i = 0; i < questions.Length; i++)
        {
            if (questions[i].value != correctAnswers[i])
            {
                allCorrect = false;
                break; // Stop checking further if any answer is wrong
            }
        }

        if (allCorrect)
        {
            Debug.Log("Correct");
            SceneManager.LoadScene(winningSceneName); // Load winning scene
        }
        else
        {
            Debug.Log("Incorrect. Try again.");
            SceneManager.LoadScene(losingSceneName); // Load losing scene
        }
    }

    private void ReturnToScene()
    {
        SceneManager.LoadScene(returnSceneName); // Load the return scene
    }
}
