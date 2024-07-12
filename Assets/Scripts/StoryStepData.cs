using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Flags]
public enum StoryStepType
{
    AdvanceText = 1,
    AnxietyUp = 2,
    AnxietyDown = 4,
}

[Serializable]
public class StoryStepData
{
    public StoryStepType storyStepType;
    public int storyStepId;
    public string textToShowIfSelectedWhat;

    public bool CheckStoryStepTypeExists(StoryStepType storyStepToCheck)
    {
            return (storyStepType & storyStepToCheck) != 0;
    }
}
