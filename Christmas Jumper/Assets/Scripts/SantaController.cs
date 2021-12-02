using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SantaController : KinematicObject
{
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    public float maxSpeed = 7f;
    public float jumpTakeOffSpeed = 7f;

    public Vector2 move;
    private bool jump;
    
    private static readonly int WALK_TRIGGER = Animator.StringToHash("Walk");
    private static readonly int RUN_TRIGGER = Animator.StringToHash("Run");
    private static readonly int IDLE_TRIGGER = Animator.StringToHash("Idle");
    private static readonly int JUMP_TRIGGER = Animator.StringToHash("Jump");
    private static readonly int SLIDE_TRIGGER = Animator.StringToHash("Slide");
    private static readonly int DEAD_TRIGGER = Animator.StringToHash("Dead");

    private void ResetAllTriggers()
    {
        animator.ResetTrigger(WALK_TRIGGER);
        animator.ResetTrigger(RUN_TRIGGER);
        animator.ResetTrigger(IDLE_TRIGGER);
        animator.ResetTrigger(JUMP_TRIGGER);
        animator.ResetTrigger(SLIDE_TRIGGER);
        animator.ResetTrigger(DEAD_TRIGGER);
    }

    protected override void ComputeVelocity()
    {
        if (jump && IsGrounded)
        {
            velocity.y = jumpTakeOffSpeed;
            jump = false;
        }

        targetVelocity = move * maxSpeed;
    }
    
    // Update is called once per frame
    protected override void Update()
    {
        move.x = 0;
        if (Input.GetKeyDown(KeyCode.D))
        {
            ResetAllTriggers();
            animator.SetTrigger(DEAD_TRIGGER);
            return;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            spriteRenderer.flipX = true;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            spriteRenderer.flipX = false;
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.speed = 1f;
            ResetAllTriggers();
            animator.SetTrigger(JUMP_TRIGGER);
            if (IsGrounded)
                jump = true;
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                move.x = 1;
                if (IsGrounded)
                    animator.speed = 2.5f;
            }
            else
            {
                move.x = -1;
                if (IsGrounded)
                    animator.speed = 2.5f;
            }
            
            ResetAllTriggers();
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                animator.SetTrigger(SLIDE_TRIGGER);
            }
            else if (Input.GetKey(KeyCode.LeftShift))
            {
                animator.SetTrigger(RUN_TRIGGER);
                move.x *= 1.5f;
            }
            else
            {
                animator.SetTrigger(WALK_TRIGGER);
            }
        }
        else
        {
            ResetAllTriggers();
            animator.SetTrigger(IDLE_TRIGGER);
        }
        
        base.Update();
    }
}
