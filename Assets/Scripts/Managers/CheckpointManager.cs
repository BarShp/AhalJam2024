using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointManager : BaseSingletonMonoBehaviour<CheckpointManager>
{
    private Checkpoint currentCheckpoint;
    private Rigidbody2D playerRigidbody;

    private void Start() 
    {
        DontDestroyOnLoad(gameObject);
        playerRigidbody = FindAnyObjectByType<PlayerMovment>().GetComponent<Rigidbody2D>();
        EventsManager.Instance.AddListener(EventType.OnPlayerLoss, ResetBackToCheckpoint);
    }
    
    private void ProcessSetCheckpoint(int currentStoryStep, AnxietyState anxietyState)
    {
        print("Set Checkpoint!");
        currentCheckpoint = new Checkpoint 
        {
            StoryStepCheckpoint = currentStoryStep,
            AnxietyState = anxietyState,
            CheckpointPosition = playerRigidbody.position,
            SceneId = SceneManager.GetActiveScene().buildIndex
        };
    }

    private void ResetBackToCheckpoint(object obj)
    {
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
}
