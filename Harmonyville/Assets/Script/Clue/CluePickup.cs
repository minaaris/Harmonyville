using UnityEngine;

public class ClueObject : MonoBehaviour, IInteractable, IClueProvider
{
    [SerializeField] private Clue clueDetails; // Assign this in the inspector
    public string InteractionPrompt => $"Press E to interact with {clueDetails.clueID}";


    // Interact method from IInteractable
    public bool Interact(Interactor interactor)
    {
        // Add the clue to the player's inventory and disable the object
        if (interactor != null)
        {
            Inventory playerInventory = interactor.GetComponent<Inventory>();
            if (playerInventory != null)
            {
                playerInventory.AddClue(GetClue());
                Debug.Log($" {clueDetails.GetCurrentContent()}"); // Log message to the console
                clueDetails.NextContent();// Move to the next content


                return true;
            }
        }
        return false;
    }

    // GetClue method from IClueProvider
    public Clue GetClue()
    {
        return clueDetails;
    }
}
