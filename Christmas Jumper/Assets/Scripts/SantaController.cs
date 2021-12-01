using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SantaController : MonoBehaviour
{
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rigidBody;
    public float speed = 10f;
    public float force = 50f;

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
    
    // Update is called once per frame
    void Update()
    {
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
            ResetAllTriggers();
            animator.SetTrigger(JUMP_TRIGGER);
            rigidBody.AddForce(Vector2.up * force, ForceMode2D.Impulse);
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
               rigidBody.velocity = Vector2.right * speed * Time.deltaTime; 
            }
            else
            {
                rigidBody.velocity = Vector2.left * speed * Time.deltaTime; 
            }
            
            ResetAllTriggers();
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                animator.SetTrigger(SLIDE_TRIGGER);
            }
            else if (Input.GetKey(KeyCode.LeftShift))
            {
                animator.SetTrigger(RUN_TRIGGER);
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
    }
}
