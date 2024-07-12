using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    EventsManager eventManager;
    AnxietyManager anxietyManager;

    void Start()
    {
        AddListeners();
        eventManager = FindFirstObjectByType<EventsManager>();
        anxietyManager = FindFirstObjectByType<AnxietyManager>();
    }

    public void ModifyStoryStep(object storyStepData)
    {
        var eventStoryStepData = (StoryStepData) storyStepData;
        switch(eventStoryStepData.storyStepType)
        {
            case StoryStepType.AnxietyUp:
            anxietyManager.ModifyAnxietyState(AnxietyState.Medium);
            break;

            case StoryStepType.AnxietyDown:
            anxietyManager.ModifyAnxietyState(AnxietyState.Medium);
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
