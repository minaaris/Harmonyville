using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement; // Required for changing scenes

public class MissionManager : MonoBehaviour, IInteractable
{
    public Mission mission; // Define this in the inspector or through code
    private bool missionCompleted = false; // To track if the mission is already completed
    public string nextSceneName; // Name of the next scene to load

    public string InteractionPrompt
    {
        get
        {
            return missionCompleted ? "Press E to Write Report" : "Press E to check Mission Status";
        }
    }

    public bool Interact(Interactor interactor)
    {
        if (!missionCompleted)
        {
            Inventory playerInventory = FindObjectOfType<Inventory>();
            if (playerInventory != null)
            {
                if (playerInventory.CheckMissionCompletion(mission))
                {
                    missionCompleted = true;
                    Debug.Log($"Clues Collected: {mission.missionName}. {mission.missionDescription}");
                    // Additional code here if needed for when the mission is just completed
                }
                else
                {
                    DisplayMissingClues(playerInventory);
                }
            }
        }
        else
        {
            Debug.Log("Writing Report...");
            LoadNextScene();
        }

        return true;
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }

    private void DisplayMissingClues(Inventory playerInventory)
    {
        string missingClues = string.Join(", ", mission.requiredClues.Where(clueID => !playerInventory.HasClue(clueID)));
        Debug.Log($"Mission: {mission.missionName}. Missing Clues: {missingClues}. The clues are hidden around the town, when you find all the clues required, you can comback here to write the paper");
    }
}
