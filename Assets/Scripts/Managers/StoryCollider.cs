using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Sirenix.OdinInspector;
using UnityEngine;

public class StoryCollider : MonoBehaviour
{
    EventsManager eventsManager;
    [SerializeField] StoryStepData storyStepData;
    // Start is called before the first frame update
    void Start()
    {
        eventsManager = FindObjectOfType<EventsManager>();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        print("ME COLLIDED AHAL");
        eventsManager.InvokeEvent(EventType.OnStoryColliderHit, storyStepData);
    }
}
