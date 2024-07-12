using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    [SerializeField] StoryStepsSO StoryDataSO;

    private  AnxietyManager anxietyManager;
    private Dialogue dialogueManager;
    private Rigidbody2D playerRigidbody;
    private int currentStoryStep = 0;
    private Checkpoint currentCheckpoint;
    void Start()
    {
        anxietyManager = FindFirstObjectByType<AnxietyManager>();
        dialogueManager = FindFirstObjectByType<Dialogue>();
        playerRigidbody = FindAnyObjectByType<PlayerMovment>().GetComponent<Rigidbody2D>();
        AddListeners();
    }

    public void ModifyStoryStep(object collisionId)
    {
        var currentCollisionId = (string)collisionId;

        if (currentStoryStep > StoryDataSO.storyStepsData.Length - 1) throw new Exception("Trying to get a new story step but StoryData doesn't have any left, dudu plz");

        StoryStepData currentStoryData = StoryDataSO.storyStepsData[currentStoryStep];

        if (currentStoryData.collisionId != currentCollisionId) return;

        foreach (BaseStoryStepAction storyActionStep in currentStoryData.storyStepActions)
        {
            ProcessAction(storyActionStep);
        }

        if(currentStoryData.setCheckpoint)
        {
            ProcessSetCheckpoint();
        };

        currentStoryStep += 1;        
    }

    private void ProcessAction(BaseStoryStepAction baseStoryStep)
    {
        switch (baseStoryStep.actionType)
        {
            //Prints are for testings and will get removed later
            case StoryStepActionType.AdvanceText:
            //Call Dialogue system to pop out and show people
                ProcessInitiateNewDialogue(baseStoryStep.DialogueText);
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

    private void ProcessSetCheckpoint()
    {
        print("Set Checkpoint!");
        currentCheckpoint = new Checkpoint 
        {
            StoryStepCheckpoint = currentStoryStep,
            AnxietyState = anxietyManager.currentState,
            CheckpointPosition = playerRigidbody.position
        };
    }

    void AddListeners()
    {
        EventsManager.Instance.AddListener(EventType.OnStoryTriggerHit, ModifyStoryStep);
        EventsManager.Instance.AddListener(EventType.OnPlayerLoss, ResetBackToCheckpoint);
    }

    private void ResetBackToCheckpoint(object obj)
    {
        //FadeToBlack
        playerRigidbody.position = currentCheckpoint.CheckpointPosition;
        dialogueManager.StopAndClearDialogue();
        print(currentCheckpoint.StoryStepCheckpoint);
        StoryStepData currentStoryData = StoryDataSO.storyStepsData[currentCheckpoint.StoryStepCheckpoint];
        foreach (BaseStoryStepAction storyActionStep in currentStoryData.storyStepActions)
        {
            ProcessAction(storyActionStep);
        }
        anxietyManager.ModifyAnxietyState(currentCheckpoint.AnxietyState);
    }

    private void ProcessInitiateNewDialogue(DialogueSO dialogueSO)
    {
        //Temporary print to until merged with dialogue system
        dialogueManager.StartDialogue(dialogueSO);
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