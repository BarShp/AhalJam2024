using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Sirenix.OdinInspector;
using UnityEngine;

public class StoryCollider : MonoBehaviour
{
    EventsManager eventsManager;
    bool hasTriggered = false;
    [SerializeField] StoryStepData storyStepData;
    // Start is called before the first frame update
    void Start()
    {
        eventsManager = FindObjectOfType<EventsManager>();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        print("ME COLLIDED AHAL");
        if (hasTriggered == false)
        {
            eventsManager.InvokeEvent(EventType.OnStoryColliderHit, storyStepData);
            hasTriggered = true;
        }
    }
}
