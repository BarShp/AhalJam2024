using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryManager : BaseSingletonMonoBehaviour<StoryManager>
{
    [SerializeField] StoryStepsSO StoryDataSO;

    private AnxietyManager anxietyManager;
    private DialogueManager _dialogueManagerManager;
    private int currentStoryStep = 0;
    private int currentStoryStepIndex = 0;
    public int CurrentStoryStep => currentStoryStep;
    
    void Start()
    {
        anxietyManager = FindFirstObjectByType<AnxietyManager>();
        _dialogueManagerManager = FindFirstObjectByType<DialogueManager>();
        AddListeners();
    }

    public void GoToStoryStepIndex(int index)
    {
        currentStoryStep = index;
        print(currentStoryStep);
        ProcessAndContinueCurrentStep();
    }

    public void ModifyStoryStep(object collisionId)
    {
        var currentCollisionId = (string)collisionId;
        print(currentStoryStep);
        if (currentStoryStep > StoryDataSO.storyStepsData.Length - 1) throw new Exception("Trying to get a new story step but StoryData doesn't have any left, dudu plz");

        StoryStepData currentStoryData = StoryDataSO.storyStepsData[currentStoryStep];

        if (currentStoryData.collisionId != currentCollisionId) return;

        ProcessAndContinueCurrentStep();
    }

    private void ProcessAndContinueCurrentStep()
    {
        foreach (BaseStoryStepAction storyActionStep in StoryDataSO.storyStepsData[currentStoryStep].storyStepActions)
        {
            ProcessAction(storyActionStep);
        }

        currentStoryStep += 1;        
    }

    private void ProcessAction(BaseStoryStepAction baseStoryStep)
    {
        switch (baseStoryStep.actionType)
        {
            //Prints are for testings and will get removed later
            case StoryStepActionType.AdvanceText:
            //Call Dialogue system to pop out and show people
                ProcessInitiateNewDialogue(baseStoryStep.DialogueText);
                print("Starting New Dialogue");
                break;

            case StoryStepActionType.ChangeAnxietyLevel:
                print("Modifying Anxiety Level");
                ProcessChangeAnxietyLevel(baseStoryStep.AnxietyLevel);
                break;
            
            case StoryStepActionType.ChangeInteractableState:
                print("Changing Lock Interactable");
                ProcessChangeInteractableLockState(baseStoryStep.InteractableLockState);
                break;
            
            case StoryStepActionType.GoToBattle:
                SceneManager.LoadScene(baseStoryStep.BattleData.battleSceneId);
                break;
        }
    }


    void AddListeners()
    {
        EventsManager.Instance.AddListener(EventType.OnStoryTriggerHit, ModifyStoryStep);
    }


    private void ProcessInitiateNewDialogue(DialogueSO dialogueSO)
    {
        //Temporary print to until merged with dialogue system
        _dialogueManagerManager = FindFirstObjectByType<DialogueManager>();
        _dialogueManagerManager.StartDialogue(dialogueSO);
    }

    private void ProcessChangeAnxietyLevel(AnxietyState state)
    {
        anxietyManager = FindAnyObjectByType<AnxietyManager>();
        anxietyManager.ModifyAnxietyState(state);
    }

    private void ProcessChangeInteractableLockState(InteractableLockStateChangeRequest actionData)
    {
        EventsManager.Instance.InvokeEvent(EventType.OnInteractableChangeLockStateRequest, actionData);
    }
    
}