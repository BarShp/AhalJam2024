using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu]
public class DialogueSO : ScriptableObject
{
    public DialogueLineobject[] lines;
}

public enum DialogueSpeaker
{
    Self,
    Therapist,
    VoicesInMyHead
}
[Serializable]
public class DialogueLineobject
{
    public string text;
    public float textSpeed = 0.125f;    
    public DialogueSpeaker speaker;
    public bool AutoSkip = false;
}