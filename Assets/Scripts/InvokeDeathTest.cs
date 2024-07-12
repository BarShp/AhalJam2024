using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvokeDeathTest : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other) {
        EventsManager.Instance.InvokeEvent(EventType.OnPlayerLoss);
    }
}
