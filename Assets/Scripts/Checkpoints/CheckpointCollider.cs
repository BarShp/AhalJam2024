using System;
using Core;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider2D))]
public class CheckpointCollider : BaseMonoBehaviour
{
    // [SerializeField] private AnxietyManager _anxietyManager;
    // [SerializeField] private Transform spawnTransform;
    
    // // StoryStepCheckpoint = currentStoryStep,
    // // CheckpointPosition = playerRigidbody.position,
    // // SceneId = SceneManager.GetActiveScene().buildIndex
    // private Checkpoint checkpointData;

    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     checkpointData = new()
    //     {
    //         StoryStepCheckpoint = StoryManager.Instance.CurrentStoryStep - 1,
    //         SceneId = SceneManager.GetActiveScene().buildIndex,
    //         CheckpointPosition = spawnTransform.position,
    //         AnxietyState = _anxietyManager.currentState
    //     };
    //     CheckpointManager.Instance.SetCheckpoint(checkpointData);
    // }
}
