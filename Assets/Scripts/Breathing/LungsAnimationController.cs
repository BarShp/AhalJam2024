using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

public class LungsAnimationController : BaseMonoBehaviour
{
    [SerializeField] private Animator animatorComponent;

    public void SetCurrentLungsUsage(float normalizedValue)
    {
        animatorComponent.SetFloat("Time", Mathf.Clamp(normalizedValue, 0, 0.99f));
    }
}
