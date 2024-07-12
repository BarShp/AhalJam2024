using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvokeDeathTest : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        EventsManager.Instance.InvokeEvent(EventType.OnPlayerLoss);
    }
}
