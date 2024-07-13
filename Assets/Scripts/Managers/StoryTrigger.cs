using UnityEngine;

public class StoryTrigger : MonoBehaviour
{
    [SerializeField] private string colliderId;
    
    private void OnTriggerEnter2D(Collider2D other) 
    {   
        EventsManager.Instance.InvokeEvent(EventType.OnStoryTriggerHit, colliderId);
    }
}
