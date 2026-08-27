[System.Serializable]
public class Clue
{
    public string clueID; // Unique identifier for the clue
    public string description;
    public string[] content; // Array of text content for the clue
    private int currentContentIndex = 0; // Index to track the current content

    // Method to get the current content
    public string GetCurrentContent()
    {
        if (content.Length == 0)
        {
            return "No content available.";
        }
        return content[currentContentIndex];
    }

    // Method to move to the next content
    public void NextContent()
    {
        if (content.Length > 0)
        {
            currentContentIndex = (currentContentIndex + 1) % content.Length;
        }
    }
}

