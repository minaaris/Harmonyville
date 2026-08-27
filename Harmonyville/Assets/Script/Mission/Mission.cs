using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class Mission
{
    public string missionName;
    public string missionDescription;
    public List<string> requiredClues; // List of clueIDs required to complete the mission

    public bool IsCompleted(List<Clue> playerClues)
    {
        foreach (string requiredClue in requiredClues)
        {
            if (!playerClues.Any(clue => clue.clueID == requiredClue))
            {
                return false; // Required clue is missing
            }
        }
        return true; // All required clues are present
    }
}
