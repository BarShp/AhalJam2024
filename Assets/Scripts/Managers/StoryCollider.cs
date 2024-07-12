using UnityEngine;

public class StoryCollider : MonoBehaviour
{
    [SerializeField] private string colliderId;
    
    private bool hasTriggered = false;

    private void OnCollisionEnter2D(Collision2D other) 
    {
        print("ME COLLIDED AHAL");
        
        if (hasTriggered) return;
        
        hasTriggered = true;
        EventsManager.Instance.InvokeEvent(EventType.OnStoryColliderHit, colliderId);
    }
}
