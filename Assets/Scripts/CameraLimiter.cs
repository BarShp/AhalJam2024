using Core;
using UnityEngine;

public class CameraLimiter : MonoBehaviour
{
    [SerializeField] private float leftLimitX;
    [SerializeField] private float rightLimitX;
    void Update()
    {
        transform.SetX(Mathf.Clamp(transform.position.x, leftLimitX, rightLimitX));
    }
}
