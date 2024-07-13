using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointManager : BaseSingletonMonoBehaviour<CheckpointManager>
{
    private Transform playerTransform;
    private Checkpoint currentCheckpoint;

    private AnxietyManager _anxietyManager;
    private DialogueManager _dialogueManager;
    private StoryManager _storyManager;

    private StoryManager StoryManagerInstance
    {
        get
        {
            if (_storyManager != null) return _storyManager;
            
            _storyManager = FindAnyObjectByType<StoryManager>();
            return _storyManager;
        }
    }
    
    private void Start()
    {
        _anxietyManager = FindAnyObjectByType<AnxietyManager>();
        _storyManager = FindAnyObjectByType<StoryManager>();
        playerTransform = FindAnyObjectByType<PlayerMovment>().transform;
        EventsManager.Instance.AddListener(EventType.OnPlayerLoss, ResetBackToCheckpoint);
    }
    
    public void SetCheckpoint(Checkpoint checkpointData)
    {
        currentCheckpoint = checkpointData;
    } 

    private void ResetBackToCheckpoint(object data)
    {
        if (currentCheckpoint == null)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            return;
        }

        print(currentCheckpoint.StoryStepCheckpoint);

        SceneManager.sceneLoaded += ResetCheckpoint;
        SceneManager.LoadScene(currentCheckpoint.SceneId);
    }

    private void ResetCheckpoint(Scene arg0, LoadSceneMode arg1)
    {
        SceneManager.sceneLoaded -= ResetCheckpoint;
        _dialogueManager = FindAnyObjectByType<DialogueManager>();
        _dialogueManager.StopAndClearDialogue();

        _anxietyManager = FindAnyObjectByType<AnxietyManager>();
        _anxietyManager.ModifyAnxietyState(currentCheckpoint.AnxietyState);
        
        StoryManagerInstance.GoToStoryStepIndex(currentCheckpoint.StoryStepCheckpoint);
        
        playerTransform = FindAnyObjectByType<PlayerMovment>().transform;
        playerTransform.position = currentCheckpoint.CheckpointPosition;
    }
}
