using System;

public enum StoryStepActionType
{
    AdvanceText,
    AnxietyUp,
    AnxietyDown,
}

[Serializable]
public class StoryStepData
{
    public string collisionId;
    public int storyStepId;
    public BaseStoryStepAction[] storyStepActions;
}

public abstract class BaseStoryStepAction
{
    public StoryStepActionType actionType;
}

public class AnxietyStoryStepAction : BaseStoryStepAction
{
    public AnxietyState ChangeToState;
}