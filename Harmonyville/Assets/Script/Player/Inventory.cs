using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Clue> clues = new List<Clue>();

    public void AddClue(Clue clue)
    {
        if (!clues.Any(c => c.clueID == clue.clueID))
        {
            clues.Add(clue); // Avoid adding duplicates
        }
    }

    public bool CheckMissionCompletion(Mission mission)
    {
        return mission.IsCompleted(clues);
    }

    public Clue GetClueById(string clueID)
    {
        return clues.FirstOrDefault(clue => clue.clueID == clueID);
    }

    public bool HasClue(string clueID)
    {
        return clues.Any(clue => clue.clueID == clueID);
    }

}
