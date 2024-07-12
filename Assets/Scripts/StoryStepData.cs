using System;
using System.Diagnostics;
using Sirenix.OdinInspector;

public enum StoryStepActionType
{
    AdvanceText,
    ChangeAnxietyLevel,
    ChangeInteractableState
}

[Serializable]
public class StoryStepData
{
    public string collisionId;
    public BaseStoryStepAction[] storyStepActions;
}

[Serializable]
public class BaseStoryStepAction
{
    public StoryStepActionType actionType;

    [ShowIf("actionType", StoryStepActionType.ChangeAnxietyLevel)]
    public AnxietyState AnxietyLevel;

    [ShowIf("actionType", StoryStepActionType.AdvanceText)]
    public string DialogueSO;

    [ShowIf("actionType", StoryStepActionType.ChangeInteractableState)]
    public InteractableLockStateChangeRequest InteractableLockState;
}

[Serializable]
public struct InteractableLockStateChangeRequest
{
    public string InteractableID;
    public bool IsInteractable;
}
