using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform interactionPoint;
    [SerializeField] private float interactionRange = 1f;
    [SerializeField] private LayerMask interactableMask = default;
    [SerializeField] private InteractionPromptUI interactionPromptUI;

    private readonly Collider[] _colliders = new Collider[3];
    [SerializeField] private int numFound;

    private IInteractable _interactable;

    private Inventory inventory;

    private void Start()
    {
        inventory = GetComponent<Inventory>();
    }
    private void Update()
    {
        numFound = Physics.OverlapSphereNonAlloc(interactionPoint.position, interactionRange, _colliders, interactableMask);
        if (numFound > 0 )
        {
            var interactable = _colliders[0].GetComponent<IInteractable>();
            if (interactable != null)
            {
                if (!interactionPromptUI.IsDisplayed)
                    interactionPromptUI.SetUPrompt(interactable.InteractionPrompt);
                if (Input.GetKeyDown(KeyCode.E)) interactable.Interact(this);
             
            }
        }
        else
        {
            if (_interactable != null) _interactable = null;
            if (interactionPromptUI.IsDisplayed)
                interactionPromptUI.Close();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(interactionPoint.position, interactionRange);
    }
    public void InteractWith(IInteractable interactable)
    {
        if (interactable.Interact(this))
        {
            // Check if the interactable is also a clue provider
            if (interactable is IClueProvider clueProvider)
            {
                Clue clue = clueProvider.GetClue();
                inventory.AddClue(clue);
            }
        }
    }


}
