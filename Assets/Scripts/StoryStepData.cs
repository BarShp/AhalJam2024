using System;
using System.Diagnostics;
using Sirenix.OdinInspector;

public enum StoryStepActionType
{
    AdvanceText,
    ChangeAnxietyLevel,
    ChangeInteractableState,
}

[Serializable]
public struct StoryStepData
{
    public string collisionId;
    public bool setCheckpoint;    
    public BaseStoryStepAction[] storyStepActions;
}

[Serializable]
public struct BaseStoryStepAction
{
    public StoryStepActionType actionType;

    [ShowIf("actionType", StoryStepActionType.ChangeAnxietyLevel)]
    public AnxietyState AnxietyLevel;

    [ShowIf("actionType", StoryStepActionType.AdvanceText)]
    public DialogueSO DialogueText;

    [ShowIf("actionType", StoryStepActionType.ChangeInteractableState)]
    public InteractableLockStateChangeRequest InteractableLockState;
}

[Serializable]
public struct InteractableLockStateChangeRequest
{
    public string InteractableID;
    public bool IsInteractable;
}
