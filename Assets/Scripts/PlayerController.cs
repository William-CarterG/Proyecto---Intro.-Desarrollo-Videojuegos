using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float runningSpeed = 15f;

    private bool isMoving;
    private bool isRunning = false;
    private Animator animator;
    private Rigidbody2D rb;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        MovePlayer(moveHorizontal, moveVertical);
        UpdateAnimator(moveHorizontal, moveVertical);
        UpdateRunningState();
    }

  void MovePlayer(float horizontal, float vertical)
{
    Vector2 movement = new Vector2(horizontal, vertical);
    if (movement.magnitude > 0)
    {
        rb.velocity = movement * (isRunning ? runningSpeed : speed);
    }
    else
    {
        rb.velocity = Vector2.zero; // Detener el movimiento cuando no hay entrada de movimiento
    }
}

    void UpdateAnimator(float horizontal, float vertical)
    {
        if (horizontal != 0 || vertical != 0)
        {
            animator.SetFloat("moveX", horizontal);
            animator.SetFloat("moveY", vertical);
            if (!isMoving)
            {
                isMoving = true;
                animator.SetBool("isMoving", true);
            }
        }
        else
        {
            if (isMoving)
            {
                isMoving = false;
                animator.SetBool("isMoving", false);
            }
        }
    }

    void UpdateRunningState()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isRunning = true;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            isRunning = false;
        }
    }
}
