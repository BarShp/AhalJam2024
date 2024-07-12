using System;
using Core;
using UnityEngine;

public abstract class BaseInteractableController : BaseMonoBehaviour, IInteractable
{
    [SerializeField] private string interactableID;
    [SerializeField] protected GameObject interactionTextObject;
    [SerializeField] private bool isInteractable = true;
    
    protected abstract void Interaction();

    protected void Awake()
    {
        EventsManager.Instance.AddListener(EventType.OnInteractableChangeLockStateRequest, OnInteractableLockStateChangeRequest);
    }

    protected void OnDestroy()
    {
        EventsManager.Instance.RemoveListener(EventType.OnInteractableChangeLockStateRequest, OnInteractableLockStateChangeRequest);
    }

    public void Interact()
    {
        if (!isInteractable) return;
        
        Interaction();
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (!isInteractable) return;

        EnableInteractionDisplay();
    }

    protected void OnTriggerExit2D(Collider2D other)
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
    
    private void OnInteractableLockStateChangeRequest(object eventData)
    {
        var interactableChangeLockStateRequest = (InteractableLockStateChangeRequest)eventData;

        if (interactableChangeLockStateRequest.InteractableID != interactableID) return;

        isInteractable = interactableChangeLockStateRequest.IsInteractable;
        
        Debug.Log($"Interactable id {interactableID} changed lock state to {isInteractable}");
    }
}