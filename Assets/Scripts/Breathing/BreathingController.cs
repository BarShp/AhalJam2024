using PathCreation;
using UnityEngine;

public class BreathingController : MonoBehaviour
{
    [SerializeField] private PathCreator breathingPatternPathCreator;
    [SerializeField] private Transform breathingPoint;
    [SerializeField] private float thresholdBreathingOffset = 0.25f;

    private VertexPath breathingPatternVertexPath;

    private void Awake()
    {
        breathingPatternVertexPath = breathingPatternPathCreator.path;
    }

    protected void Update()
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
}
