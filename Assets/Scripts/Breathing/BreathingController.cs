using System;
using Core;
using PathCreation;
using Sirenix.OdinInspector;
using UnityEngine;
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
    
    private float anxietyGainSpeed;
    
    private VertexPath breathingPatternVertexPath;

    private float currentBreathingPointNormalized;

    [ReadOnly] [SerializeField] private float currentAnxiety = 0;

    private bool isGameStarted = false;
    private bool firstPointReached = false;
    
    private float GetWorldLimitMinY() => transform.position.y - breathingPointMinLimitY;
    private float GetWorldLimitMaxY() => transform.position.y + breathingPointMaxLimitY;
    
    private void Awake()
    {
        breathingPatternVertexPath = breathingPatternPathCreator.path;
        currentBreathingPointNormalized = 0.5f;
        anxietyGainSpeed = 1 / timeToMaxAnxietyInSeconds;
    }

    [ContextMenu("Start Game")]
    public void StartGame()
    {
        isGameStarted = true;
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
        
        if (firstPointReached) return;
        firstPointReached = breathingPatternVertexPath.GetClosestTimeOnPath(breathingPoint.position) > 0;
        Debug.Log("Starting to calculate anxiety level");
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
        if (!firstPointReached) return;
        
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
        
        anxietyBar.fillAmount = currentAnxiety;

        // Don't ask, I'm tired
        heartAnimationController.SetAnimationSpeed(currentAnxiety + 0.5f);

        if (Mathf.Approximately(currentAnxiety, 1))
        {
            EventsManager.Instance.InvokeEvent(EventType.OnPlayerLoss);
        }
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
