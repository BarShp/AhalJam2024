using System.Collections.Generic;
using Core;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AnxietyManager : BaseMonoBehaviour
{
    [SerializeField] private Dictionary<AnxietyState, Sprite> anxietyStateToSprite;

    private SpriteRenderer frenchThingySprite;
    public AnxietyState currentState = AnxietyState.Calm;

    private void Awake()
    {
        frenchThingySprite = GetComponent<SpriteRenderer>();
    }

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
        var sprite = anxietyStateToSprite[state];
        frenchThingySprite.sprite = sprite;
    }
}

public enum AnxietyState
{
    Calm,
    Low,
    Medium,
    High
}