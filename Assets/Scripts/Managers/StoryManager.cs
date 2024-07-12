using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    [SerializeField] StoryStepsSO StoryDataSO;

    private  AnxietyManager anxietyManager;
    private int currentStoryStep = 0;
    void Start()
    {
        anxietyManager = FindFirstObjectByType<AnxietyManager>();
        AddListeners();
    }

    public void ModifyStoryStep(object collisionId)
    {
        var currentCollisionId = (string)collisionId;
        StoryStepData currentStoryData = StoryDataSO.storyStepsData[currentStoryStep];

        if (currentStoryData.collisionId != currentCollisionId) return;

        foreach (BaseStoryStepAction storyActionStep in currentStoryData.storyStepActions)
        {
            ProcessAction(storyActionStep);
        }

        //
        // print(eventStoryStepData.storyStepType);
        // if (eventStoryStepData.CheckStoryStepTypeExists(StoryStepType.AnxietyUp))
        // {
        //     anxietyManager.ModifyAnxietyState(AnxietyState.Medium);
        // }
        //
        // if (eventStoryStepData.CheckStoryStepTypeExists(StoryStepType.AnxietyDown))
        // {
        //     anxietyManager.ModifyAnxietyState(AnxietyState.Calm);
        // }
        //
        // if (eventStoryStepData.CheckStoryStepTypeExists(StoryStepType.AdvanceText))
        // {
        //     print(eventStoryStepData.textToShowIfSelectedWhat);
        // }
    }

    private void ProcessAction(BaseStoryStepAction baseStoryStep)
    {
        switch (baseStoryStep.actionType)
        {
            //Prints are for testings and will get removed later
            case StoryStepActionType.AdvanceText:
            //Call Dialogue system to pop out and show people
                ProcessInitiateNewDialogue(baseStoryStep.DialogueSO);
                print("Starting New Dialogue");
                break;

            case StoryStepActionType.ChangeAnxietyLevel:
                print("Modifying Anxiety Level");
                ProcessChangeAnxietyLevel(baseStoryStep.AnxietyLevel);
                break;
            
            case StoryStepActionType.ChangeInteractableState:
                print("Changing Lock Interactable");
                ProcessChangeInteractableLockState(baseStoryStep.InteractableLockState);
                break;
        }
    }

    void AddListeners()
    {
        EventsManager.Instance.AddListener(EventType.OnStoryColliderHit, ModifyStoryStep);
    }

    private void ProcessInitiateNewDialogue(string dialogueSO)
    {
        //Temporary print to until merged with dialogue system
        print(dialogueSO);
    }

    private void ProcessChangeAnxietyLevel(AnxietyState state)
    {
        anxietyManager.ModifyAnxietyState(state);
    }

    private void ProcessChangeInteractableLockState(InteractableLockStateChangeRequest actionData)
    {
        EventsManager.Instance.InvokeEvent(EventType.OnInteractableChangeLockStateRequest, actionData);
    }
    
}