using Core;
using UnityEngine;

public abstract class BaseInteractableController : BaseMonoBehaviour, IInteractable
{
    [SerializeField] protected GameObject interactionTextObject;

    protected bool isInteractable = true;
    
    protected abstract void Interaction();

    public void Interact()
    {
        if (!isInteractable) return;
        
        Interaction();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!isInteractable) return;

        EnableInteractionDisplay();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        DisableInteractionDisplay();
    }

    protected void EnableInteractionDisplay()
    {
        if (interactionTextObject == null) return;
        
        interactionTextObject.SetActive(true);
    }

    protected void DisableInteractionDisplay()
    {
        if (interactionTextObject == null) return;
        
        interactionTextObject.SetActive(false);
    }

    protected void DisableInteraction()
    {
        isInteractable = false;
        DisableInteractionDisplay();
    }
}