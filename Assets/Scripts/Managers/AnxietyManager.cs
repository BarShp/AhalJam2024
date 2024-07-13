using System.Collections.Generic;
using Core;
using UnityEngine;
using UnityEngine.UI;

public class AnxietyManager : BaseMonoBehaviour
{
    [SerializeField] private Dictionary<AnxietyState, Sprite> anxietyStateToSprite;

    public AnxietyState currentState = AnxietyState.Calm;
    
    private void Start() 
    {
        SetAnxietyImageOrFrenchNameThatDaniCalledItVANDATASOMETHING(currentState);
    }

    public void ModifyAnxietyState(AnxietyState newState)
    {
        if (currentState == newState) return;
        currentState = newState;
        print(currentState);

        SetAnxietyImageOrFrenchNameThatDaniCalledItVANDATASOMETHING(currentState);
    }

    private void SetAnxietyImageOrFrenchNameThatDaniCalledItVANDATASOMETHING(AnxietyState state)
    {
        // For some reason it didn't allow me to serialize field the image.

        var frenchThingy = GameObject.FindWithTag("FrenchThingyImage");
        if (frenchThingy == null) return;
        var frenchThingyImage = frenchThingy.GetComponent<Image>();
        
        if (frenchThingyImage == null) return;
        
        var sprite = anxietyStateToSprite[state];
        frenchThingyImage.color = new Color(0, 0, 0, sprite == null ? 0 : 255);
        frenchThingyImage.sprite = sprite;
    }
}

public enum AnxietyState
{
    Calm,
    Low,
    Medium,
    High
}