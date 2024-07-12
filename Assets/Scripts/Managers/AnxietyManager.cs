using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class AnxietyManager : MonoBehaviour
{
    [SerializeField] List<AnxietyStateData> anxietyStatesList;
    AnxietyState currentState = AnxietyState.Calm;
    SpriteRenderer objectSpriteRenderer;

    private void Start() {
        objectSpriteRenderer = GetComponent<SpriteRenderer>();
        AnxietyStateData stateData = anxietyStatesList.Find(state => state.state == currentState);
        objectSpriteRenderer.sprite = stateData.sprite;
    }

    public void ModifyAnxietyState(AnxietyState newState)
    {
        if (currentState != newState)
        {
            AnxietyStateData stateData = anxietyStatesList.Find(state => state.state == newState);
            print(stateData.state);
            objectSpriteRenderer.sprite = stateData.sprite;
            currentState = newState;
        }
    }
}

public enum AnxietyState
{
    Calm,
    Low,
    Medium,
    High
}

[Serializable]
public class AnxietyStateData
{
    public AnxietyState state;
    public Sprite sprite;
}
