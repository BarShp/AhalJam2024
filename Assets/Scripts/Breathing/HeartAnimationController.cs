using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

public class HeartAnimationController : BaseMonoBehaviour
{
    [SerializeField] private Animator animatorComponent;
    [SerializeField] private float maxAnimationSpeed;
    
    public void SetAnimationSpeed(float animationSpeedMultiplier)
    {
        animatorComponent.speed = animationSpeedMultiplier * maxAnimationSpeed;
    }
}
