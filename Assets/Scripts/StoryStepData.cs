using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StoryStepType
{
    AdvanceText,
    AnxietyUp,
    AnxietyDown,
}

[Serializable]
public class StoryStepData
{
    public StoryStepType storyStepType;
}
