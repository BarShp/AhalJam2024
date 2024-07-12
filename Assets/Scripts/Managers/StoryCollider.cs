using UnityEngine;

public class StoryCollider : MonoBehaviour
{
    [SerializeField] private string colliderId;
    
    private void OnCollisionEnter2D(Collision2D other) 
    {   
        EventsManager.Instance.InvokeEvent(EventType.OnStoryColliderHit, colliderId);
    }
}
