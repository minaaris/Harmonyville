using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Required for changing scenes

public class TextComparisonPuzzle : MonoBehaviour
{
    public InputField[] playerInputFields;  // Array of player's input fields
    public string[] preparedAnswers;        // Array of prepared answers
    public float similarityThreshold = 0.5f; // Threshold for text similarity
    public string nextSceneName;            // Name of the next scene to load

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CheckTextSimilarity()
    {
        int numFields = playerInputFields.Length;
        int numCorrect = 0;

        for (int i = 0; i < numFields; i++)
        {
            string playerInput = playerInputFields[i].text;
            string preparedAnswer = preparedAnswers[i];

            float similarity = CalculateTextSimilarity(playerInput, preparedAnswer);

            if (similarity >= similarityThreshold)
            {
                numCorrect++;
            }
        }

        if (numCorrect == numFields)
        {
            // All answers are correct
            PuzzleCompleted();
        }
        else
        {
            // At least one answer is incorrect
            PuzzleFailed();
        }
    }

    private float CalculateTextSimilarity(string text1, string text2)
    {
        int maxLength = Mathf.Max(text1.Length, text2.Length);
        int distance = LevenshteinDistance(text1, text2);

        float similarity = 1f - ((float)distance / maxLength);
        return similarity;
    }

    private int LevenshteinDistance(string s, string t)
    {
        int[,] dp = new int[s.Length + 1, t.Length + 1];

        for (int i = 0; i <= s.Length; i++)
        {
            dp[i, 0] = i;
        }

        for (int j = 0; j <= t.Length; j++)
        {
            dp[0, j] = j;
        }

        for (int i = 1; i <= s.Length; i++)
        {
            for (int j = 1; j <= t.Length; j++)
            {
                int cost = (s[i - 1] == t[j - 1]) ? 0 : 1;

                dp[i, j] = Mathf.Min(
                    dp[i - 1, j] + 1,
                    dp[i, j - 1] + 1,
                    dp[i - 1, j - 1] + cost
                );
            }
        }

        return dp[s.Length, t.Length];
    }

    private void PuzzleCompleted()
    {
        Debug.Log("Complete");
        LoadNextScene(); // Call the function to load the next scene
    }

    private void PuzzleFailed()
    {
        Debug.Log("Fail");
        // Perform any other actions when the puzzle is not completed successfully
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName); // Load the next scene
    }
}
