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
        if (eventStoryStepData.CheckStoryStepTypeExists(StoryStepType.AnxietyUp))
        {
            anxietyManager.ModifyAnxietyState(AnxietyState.Medium);
        }

        if (eventStoryStepData.CheckStoryStepTypeExists(StoryStepType.AnxietyDown))
        {
            anxietyManager.ModifyAnxietyState(AnxietyState.Calm);
        }

        if (eventStoryStepData.CheckStoryStepTypeExists(StoryStepType.AdvanceText))
        {
            print(eventStoryStepData.textToShowIfSelectedWhat);
        }
    }

    void AddListeners()
    {
        eventManager.AddListener(EventType.OnStoryColliderHit, ModifyStoryStep);
    }
}
