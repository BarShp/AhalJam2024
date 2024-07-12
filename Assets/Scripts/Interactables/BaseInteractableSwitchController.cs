using UnityEngine;
using UnityEngine.Events;

public class BaseInteractableSwitchController : BaseInteractableController
{
    [SerializeField] private UnityEvent onInteracted;

    protected override void Interaction()
    {
        DisableInteraction();
        onInteracted.Invoke();
    }
}