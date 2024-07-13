using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : BaseMonoBehaviour
{
    [SerializeField] private GameObject CanInteractOutline;
    private IInteractable currentInteractable;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentInteractable == null) return;
            currentInteractable.Interact();
            SoundManager.Instance.PLaySound(SoundManager.Sound.Interact);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<IInteractable>(out var interactable))
        {
            currentInteractable = interactable;
            CanInteractOutline.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<IInteractable>(out _))
        {
            currentInteractable = null;
            CanInteractOutline.SetActive(false);
        }
    }
}
