using UnityEngine;

[System.Serializable]
public class NPC : MonoBehaviour, IInteractable
{
    public string npcName;
    public string[] conversations;
    private int currentConversationIndex = 0;

    public string InteractionPrompt => "Press E to Talk to " + npcName;

    public bool Interact(Interactor interactor)
    {
        if (conversations.Length == 0)
        {
            Debug.Log($"{npcName}: No conversation available.");
            return true;
        }

        // Display the current line of conversation
        Debug.Log($" {conversations[currentConversationIndex]}");

        // Move to the next line of conversation, looping back if at the end
        currentConversationIndex = (currentConversationIndex + 1) % conversations.Length;

        return true;
    }

    // Method to get a conversation line by index
    public string GetConversationLine(int index)
    {
        if (index >= 0 && index < conversations.Length)
        {
            return conversations[index];
        }
        return "No conversation available.";
    }
}
