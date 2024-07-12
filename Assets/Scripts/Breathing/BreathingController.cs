using System;
using Core;
using PathCreation;
using UnityEngine;

public class BreathingController : BaseMonoBehaviour
{
    [SerializeField] private PathCreator breathingPatternPathCreator;
    [SerializeField] private Transform breathingPoint;
    [SerializeField] private float thresholdBreathingOffset = 0.25f;
    [SerializeField] private float breathingPatternSpeed = 1f;
    [SerializeField] private float breathInOutSpeed = 0.25f;

    [Range(0.5f, 5f)]
    [SerializeField] private float breathingPointMaxLimitY;
    [Range(0.5f, 5f)]
    [SerializeField] private float breathingPointMinLimitY;
    
    private VertexPath breathingPatternVertexPath;
    
    private float currentBreathingPointNormalized;
    private float targetBreathingPointNormalized;
    private float currentVelocity;

    private float GetWorldLimitMinY() => transform.position.y - breathingPointMinLimitY;
    private float GetWorldLimitMaxY() => transform.position.y + breathingPointMaxLimitY;
    
    private void Awake()
    {
        breathingPatternVertexPath = breathingPatternPathCreator.path;
        currentBreathingPointNormalized = 0.5f;
    }

    protected void Update()
    {
        UpdateBreathingPatternPosition();
        UpdateBreathingPoint();
        UpdateBreathingQuality();
    }

    private void UpdateBreathingPatternPosition()
    {
        breathingPatternPathCreator.transform.position += breathingPatternSpeed * Time.deltaTime * Vector3.left;
    }

    private void UpdateBreathingPoint()
    {
        var breathInOutSign = 1;
        if (!Input.GetKey(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.E))
            {
                currentVelocity = 0;
                return;
            }

            breathInOutSign = -1;
        }
        
        
        // Calculate the target position based on input
        targetBreathingPointNormalized = Mathf.Clamp(currentBreathingPointNormalized + breathInOutSign, 0, 1);

        // Smoothly interpolate towards the target using SmoothDamp
        currentBreathingPointNormalized = Mathf.SmoothDamp(currentBreathingPointNormalized, targetBreathingPointNormalized, ref currentVelocity, breathInOutSpeed);
        
        SetBreathingPointYPos(currentBreathingPointNormalized);
    }

    private void UpdateBreathingQuality()
    {
        var closestPointOnPath = breathingPatternVertexPath.GetClosestPointOnPath(breathingPoint.position);
        var distanceFromPath = Vector3.Distance(breathingPoint.position, closestPointOnPath);

        if (distanceFromPath > thresholdBreathingOffset)
        {
            Debug.Log($"Too Far - ");
        }
        else
        {
            Debug.Log($"Close enough - ");
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
