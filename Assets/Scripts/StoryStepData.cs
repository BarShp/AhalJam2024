using System;
using System.Diagnostics;
using Sirenix.OdinInspector;

public enum StoryStepActionType
{
    AdvanceText,
    ChangeAnxietyLevel,
    ChangeInteractableState,
    GoToBattle,
    GoToScene
}

[Serializable]
public struct StoryStepData
{
    public string collisionId;
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

    [ShowIf("actionType", StoryStepActionType.GoToBattle)]
    public BattleData BattleData;
    
    [ShowIf("actionType", StoryStepActionType.GoToScene)]
    public int SceneId;
}

[Serializable]
public struct BattleData
{
    public int battleSceneId;
}

[Serializable]
public struct InteractableLockStateChangeRequest
{
    public string InteractableID;
    public bool IsInteractable;
}
