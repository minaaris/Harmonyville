using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;

    private bool isOpen = false;
    private Vector3 originalPosition;

    private void Start()
    {
        // Save the original position when the script starts
        originalPosition = transform.position;
    }

    public bool Interact(Interactor interactor)
    {
        Debug.Log("Interacting with door");

        if (isOpen)
        {
            // If the door is open, move it back to the original position
            transform.position = originalPosition;
        }
        else
        {
            // If the door is closed, move it horizontally by 1 unit (adjust as needed)
            transform.Translate(Vector3.right * 1.5f);
        }

        // Toggle the state of the door (open/closed)
        isOpen = !isOpen;

        return true;
    }
}

