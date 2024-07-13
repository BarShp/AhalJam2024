using System;
using System.Collections;
using Core;
using TMPro;
using UnityEngine;

public abstract class BaseInteractableController : BaseMonoBehaviour, IInteractable
{
    [SerializeField] private string interactableID;
    [SerializeField] protected GameObject interactionTextObject;
    [SerializeField] private bool isInteractable = true;
    [SerializeField] private TMP_Text notInteractableTextObject;
    [SerializeField] private string notInteractableText = "I can't do that yet.";
    [SerializeField] private bool hasExtraSound;
    [SerializeField] private SoundManager.Sound extraSound;
    
    protected abstract void Interaction();

    protected void Awake()
    {
        EventsManager.Instance.AddListener(EventType.OnInteractableChangeLockStateRequest, OnInteractableLockStateChangeRequest);
    }

    protected void OnDestroy()
    {
        EventsManager.Instance.RemoveListener(EventType.OnInteractableChangeLockStateRequest, OnInteractableLockStateChangeRequest);
        StopAllCoroutines();
    }

    public void TriggerStoryWithInteractableID()
    {
        EventsManager.Instance.InvokeEvent(EventType.OnStoryTriggerHit, interactableID);
    }

    public void Interact()
    {
        if (!isInteractable)
        {
            if (notInteractableTextObject == null) return;

            notInteractableTextObject.gameObject.SetActive(true);
            
            StopAllCoroutines();
            StartCoroutine(DisableNotInteractableText());
            
            notInteractableTextObject.text = notInteractableText;
            return;
        }
        
        Interaction();
        if (hasExtraSound)
        { 
            SoundManager.Instance.PLaySound(extraSound);
        }
    }

    public IEnumerator DisableNotInteractableText()
    {
        yield return new WaitForSeconds(2.5f);
        notInteractableTextObject.gameObject.SetActive(false);
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