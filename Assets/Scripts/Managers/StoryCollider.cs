using UnityEngine;

public class StoryCollider : MonoBehaviour
{
    [SerializeField] private string colliderId;
    
    private EventsManager eventsManager;
    private bool hasTriggered = false;
    
    void Start()
    {
        eventsManager = FindObjectOfType<EventsManager>();
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        print("ME COLLIDED AHAL");
        
        if (hasTriggered) return;
        
        hasTriggered = true;
        eventsManager.InvokeEvent(EventType.OnStoryColliderHit, colliderId);
    }
}
