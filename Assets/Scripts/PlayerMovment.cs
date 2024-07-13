using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    private float horizontal;
    public float speed = 6f;
    private bool isFacingRight = true;
    private bool canMove = true;

    private Animator animator;

    [SerializeField] private Rigidbody2D rb;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    void OnEnable() 
    {
        EventsManager.Instance.AddListener(EventType.OnDialogueChange, toggleDialogueActive);
    }

    void OnDisable()
    {
        EventsManager.Instance.RemoveListener(EventType.OnDialogueChange, toggleDialogueActive);
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        Flip();
    }

    private void FixedUpdate()
    {
        if (!canMove)
        {
            rb.velocity = Vector2.zero;
        }
        else
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
            animator.SetFloat("xVelocity", Math.Abs(rb.velocity.x));
        }
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void toggleDialogueActive(object isDialogueActive)
    {
        canMove = !(bool)isDialogueActive;
        print(canMove);
    }
}
