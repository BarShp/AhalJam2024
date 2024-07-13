using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractableTextController : MonoBehaviour
{
    [Tooltip("Text to render. Write \"{key}\" where the action key should be placed.")]
    [SerializeField] private string text;
    
    [SerializeField] private string interactableInputChar = "E";
    [SerializeField] private TextMeshProUGUI interactableTextMeshPro;
    
    protected void Start()
    {
        interactableTextMeshPro.text = text.Replace("{key}", $"'{interactableInputChar}'");
    }
}
