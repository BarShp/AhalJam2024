using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

public class PlayerController : BaseMonoBehaviour
{
    private IInteractable currentInteractable;

    private void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            if (currentInteractable == null) return;
            currentInteractable.Interact();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<IInteractable>(out var interactable))
        {
            currentInteractable = interactable;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent<IInteractable>(out _))
        {
            currentInteractable = null;
        }
    }
}
