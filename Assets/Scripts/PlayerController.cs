using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float runningSpeed = 15f;
    private bool isRunning = false;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        GetComponent<Rigidbody2D>().velocity = movement * speed;

        if (movement.magnitude > 0)
        {
            animator.SetFloat("MoveX", movement.x);
            animator.SetFloat("MoveY", movement.y);
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
        
        if (!isRunning && Input.GetKey(KeyCode.Space))
        {
            isRunning = true;
            speed = runningSpeed;
        }
        else if (isRunning && !Input.GetKey(KeyCode.Space))
        {
            isRunning = false;
            speed = 5f;
        }
    }
}