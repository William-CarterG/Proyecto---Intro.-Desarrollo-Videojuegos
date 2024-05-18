using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float runningSpeed = 15f;
    public float stunDuration = 2.5f;
    private float cameraShakeIntensity = 0.25f;

    private bool isMoving;
    private bool isRunning = false;
    private bool isStunned = false;
    private Animator animator;
    private Rigidbody2D rb;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!isStunned)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            MovePlayer(moveHorizontal, moveVertical);
            UpdateAnimator(moveHorizontal, moveVertical);
            UpdateRunningState();
        }
    }

  void MovePlayer(float horizontal, float vertical)
{
        Vector2 movement = new Vector2(horizontal, vertical).normalized;
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(isRunning & collision.gameObject.tag == "Walls")
        {
            EditorApplication.Beep();
            Debug.Log("ATURDIDO");
            StartCoroutine(StunPlayer());
        }
        else
        {
            Debug.Log("CHOCA");
        }
        
    }
    IEnumerator StunPlayer()
    {
        // Guardar la posición inicial del jugador
        Vector2 initialPosition = transform.position;

        // Activar aturdimiento
        isStunned = true;

        // Detener el movimiento del jugador
        rb.velocity = Vector2.zero;

        // Activar vibración de la cámara
        StartCoroutine(CameraShake());

        // Esperar el tiempo de aturdimiento
        yield return new WaitForSeconds(stunDuration);

        // Restablecer el estado del jugador después del aturdimiento
        isStunned = false;
        isRunning = false;

        // Restaurar la posición inicial del jugador
        transform.position = initialPosition;
    }

    IEnumerator CameraShake()
    {
        Vector3 originalPosition = Camera.main.transform.localPosition;

        float elapsedTime = 0f;

        while (elapsedTime < stunDuration)
        {
            float x = UnityEngine.Random.Range(-1f, 1f) * cameraShakeIntensity;
            float y = UnityEngine.Random.Range(-1f, 1f) * cameraShakeIntensity;

            Camera.main.transform.localPosition = originalPosition + new Vector3(x, y, 0);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // Restablecer la posición de la cámara al final de la vibración
        Camera.main.transform.localPosition = originalPosition;
    }
}
