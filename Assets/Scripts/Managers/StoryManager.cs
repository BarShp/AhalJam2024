using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    EventsManager eventManager;
    AnxietyManager anxietyManager;
    int currentStoryStep = 0;

    void Start()
    {
        eventManager = FindFirstObjectByType<EventsManager>();
        anxietyManager = FindFirstObjectByType<AnxietyManager>();
        AddListeners();
    }

    public void ModifyStoryStep(object storyStepData)
    {
        print("I was Invoked");
        var eventStoryStepData = (StoryStepData) storyStepData;
        print(eventStoryStepData.storyStepType);
        switch(eventStoryStepData.storyStepType)
        {
            case StoryStepType.AnxietyUp:
            anxietyManager.ModifyAnxietyState(AnxietyState.Medium);
            break;

            case StoryStepType.AnxietyDown:
            anxietyManager.ModifyAnxietyState(AnxietyState.Calm);
            break;

            case StoryStepType.AdvanceText:
            break;
        }
    }

    void AddListeners()
    {
        eventManager.AddListener(EventType.OnStoryColliderHit, ModifyStoryStep);
    }
}
