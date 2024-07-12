using UnityEngine;
using UnityEngine.Events;

public class BaseInteractableButtonController : BaseInteractableController
{
    [SerializeField] private UnityEvent onInteracted;

    protected override void Interaction()
    {
        onInteracted.Invoke();
    }
}