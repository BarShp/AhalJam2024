using System;
using System.Collections;
using Core;
using PathCreation;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BreathingController : BaseMonoBehaviour
{
    [SerializeField] private PathCreator breathingPatternPathCreator;
    [SerializeField] private Transform breathingPoint;
    [SerializeField] private float thresholdBreathingOffset = 0.25f;
    [SerializeField] private float breathingPatternSpeed = 1f;
    [SerializeField] private float breathInOutSpeed = 0.25f;
    [SerializeField] private float timeToMaxAnxietyInSeconds = 6;
    [SerializeField] private float lowerAnxietySpeed = 0.125f;

    [SerializeField] private HeartAnimationController heartAnimationController;
    [SerializeField] private LungsAnimationController lungsAnimationController;

    [Range(0.1f, 5f)]
    [SerializeField] private float breathingPointMaxLimitY;
    [Range(0.1f, 5f)]
    [SerializeField] private float breathingPointMinLimitY;

    [SerializeField] private Image anxietyBar;
    
    [SerializeField] private CombatDialogue combatDialogue;
    [SerializeField] private GameObject startGameTooltip;
    
    private float anxietyGainSpeed;
    
    private VertexPath breathingPatternVertexPath;

    private float currentBreathingPointNormalized;

    [ReadOnly] [SerializeField] private float currentAnxiety = 0;

    [SerializeField] private float loadNextSceneTimer;
    [SerializeField] private int nextSceneId;
    
    private bool isGameStarted = false;
    private bool firstPointReached = false;
    private bool lastPointReached = false;

    private AudioSource beatingHeartAudioSource;
    
    private float GetWorldLimitMinY() => transform.position.y - breathingPointMinLimitY;
    private float GetWorldLimitMaxY() => transform.position.y + breathingPointMaxLimitY;
    
    private void Awake()
    {
        breathingPatternVertexPath = breathingPatternPathCreator.path;
        currentBreathingPointNormalized = 0.5f;
        anxietyGainSpeed = 1 / timeToMaxAnxietyInSeconds;
    }

    private void Start()
    {
        beatingHeartAudioSource = SoundManager.Instance.PlayContinuous(SoundManager.Sound.HeartBeat);
        currentAnxiety = 0.5f;
        SyncAnxietyView();
    }

    [ContextMenu("Start Game")]
    public void StartGame()
    {
        isGameStarted = true;
        startGameTooltip.SetActive(false);
        combatDialogue.StartDialogues();
    }

    private void OnDestroy()
    {
        SoundManager.Instance.StopContinuous(SoundManager.Sound.HeartBeat);
    }

    protected void Update()
    {
        if (!isGameStarted) return;
        
        UpdateBreathingPatternPosition();
        UpdateBreathingPoint();
        UpdateBreathingQuality();
    }

    private void UpdateBreathingPatternPosition()
    {
        breathingPatternPathCreator.transform.position += breathingPatternSpeed * Time.deltaTime * Vector3.left;

        if (!firstPointReached)
        {
            firstPointReached = breathingPatternVertexPath.GetClosestTimeOnPath(breathingPoint.position) > 0;
            Debug.Log("Starting to calculate anxiety level");
            return;
        }

        if (!lastPointReached)
        {
            lastPointReached = breathingPatternVertexPath.GetClosestTimeOnPath(breathingPoint.position) >= 1;
            if (lastPointReached)
            {
                SoundManager.Instance.StopContinuous(SoundManager.Sound.HeartBeat);
                GameWon();
            }
        }
    }

    private void UpdateBreathingPoint()
    {
        var verticalInput = Input.GetAxis("Vertical");

        // Smoothly interpolate towards the target using SmoothDamp
        currentBreathingPointNormalized += breathInOutSpeed * verticalInput * Time.deltaTime;
        currentBreathingPointNormalized = Mathf.Clamp(currentBreathingPointNormalized, 0, 1);
        
        SetBreathingPointYPos(currentBreathingPointNormalized);
        
        lungsAnimationController.SetCurrentLungsUsage(currentBreathingPointNormalized);
    }

    private void UpdateBreathingQuality()
    {
        if (!firstPointReached || lastPointReached) return;
        
        var closestPointOnPath = breathingPatternVertexPath.GetClosestPointOnPath(breathingPoint.position);
        var distanceFromPath = Vector3.Distance(breathingPoint.position, closestPointOnPath);

        if (distanceFromPath > thresholdBreathingOffset)
        {
            currentAnxiety += anxietyGainSpeed * Time.deltaTime;
        }
        else
        {
            currentAnxiety -= lowerAnxietySpeed * Time.deltaTime;
        }

        currentAnxiety = Mathf.Clamp(currentAnxiety, 0, 1);

        SyncAnxietyView();

        if (Mathf.Approximately(currentAnxiety, 1))
        {
            combatDialogue.EndDialogues();
            SoundManager.Instance.StopContinuous(SoundManager.Sound.HeartBeat);
            EventsManager.Instance.InvokeEvent(EventType.OnPlayerLoss);
            breathingPatternSpeed = 0;
        }
    }

    private void SyncAnxietyView()
    {
        // Don't ask, I'm tired
        
        anxietyBar.fillAmount = currentAnxiety;
        heartAnimationController.SetAnimationSpeed(currentAnxiety + 0.5f);
        SoundManager.Instance.SetAudioSourceSpeed(beatingHeartAudioSource, currentAnxiety + 0.5f);
    }

    private void GameWon()
    {
        StartCoroutine(LoadNextSceneCoroutine());
    }

    private IEnumerator LoadNextSceneCoroutine()
    {
        combatDialogue.EndDialogues();
        
        yield return new WaitForSeconds(loadNextSceneTimer);

        SceneManager.LoadScene(nextSceneId);
    }

    private void SetBreathingPointYPos(float normalizedValue)
    {
        var normalizedToWorldY = Mathf.Lerp(GetWorldLimitMinY(), GetWorldLimitMaxY(), normalizedValue);
            
        breathingPoint.SetY(normalizedToWorldY);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawLine(transform.position - breathingPointMinLimitY * Vector3.up, transform.position + breathingPointMaxLimitY * Vector3.up);
    }
}
