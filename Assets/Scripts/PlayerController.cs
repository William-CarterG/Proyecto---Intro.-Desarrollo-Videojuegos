using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 1f;
    public float maxSpeed = 15f;
    public float stunDuration = 0.5f;
    private float cameraShakeIntensity = 0.25f;
    public float acceleration = 0.75f;
    private float currentSpeed;
    public float zoomAmount = 4f;
    private Vector2 lastDirection;

    private bool isMoving;
    private bool isStunned = false;
    private Animator animator;
    private Rigidbody2D rb;

    public AudioClip runningSound; // Clip de sonido para la carrera
    public AudioClip crashSound; // Clip de sonido para el choque
    private bool isPlayingRunningSound = false;

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
        }
    }

    public void MovePlayer(float horizontal, float vertical)
    {
        Vector2 movement = new Vector2(horizontal, vertical).normalized;

        if (movement.magnitude > 0)
        {
            // Verifica si el jugador ha cambiado de direcci�n
            if (movement != lastDirection)
            {
                currentSpeed = speed; // Reinicia la velocidad actual
                lastDirection = movement; // Actualiza la �ltima direcci�n
            }

            // Incrementa la velocidad actual
            currentSpeed += acceleration * (Time.deltaTime / 3);
            currentSpeed = Mathf.Min(currentSpeed, maxSpeed);

            rb.velocity = movement * currentSpeed;

            // Reproducir el sonido de carrera si no se est� reproduciendo
            if (!isPlayingRunningSound)
            {
                isPlayingRunningSound = true;
                AudioSource.PlayClipAtPoint(runningSound, transform.position);
                StartCoroutine(ResetRunningSound());
            }
        }
        else
        {
            rb.velocity = Vector2.zero; // Detener el movimiento cuando no hay entrada de movimiento
            currentSpeed = speed; // Reinicia la velocidad actual
            lastDirection = Vector2.zero; // Reinicia la �ltima direcci�n
        }
    }

    IEnumerator ResetRunningSound()
    {
        yield return new WaitForSeconds(runningSound.length);
        isPlayingRunningSound = false;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((currentSpeed >= (maxSpeed - 0.5f)) && collision.gameObject.CompareTag("Walls"))
        {
            AudioSource.PlayClipAtPoint(crashSound, transform.position); // Reproducir sonido de choque
            Debug.Log("ATURDIDO");
            StartCoroutine(StunPlayer());
        }
    }

    IEnumerator StunPlayer()
    {
        // Guardar la posici�n inicial del jugador
        Vector2 initialPosition = transform.position;

        // Activar aturdimiento
        isStunned = true;

        // Detener el movimiento del jugador
        rb.velocity = Vector2.zero;

        // Activar vibraci�n de la c�mara
        StartCoroutine(CameraShake());

        // Esperar el tiempo de aturdimiento
        yield return new WaitForSeconds(stunDuration);

        // Restablecer el estado del jugador despu�s del aturdimiento
        isStunned = false;

        // Restaurar la posici�n inicial del jugador
        transform.position = initialPosition;
    }

    IEnumerator CameraShake()
    {
        Vector3 originalPosition = Camera.main.transform.localPosition;
        float originalSize = Camera.main.orthographicSize;

        float elapsedTime = 0f;

        while (elapsedTime < stunDuration)
        {
            float x = UnityEngine.Random.Range(-1f, 1f) * cameraShakeIntensity;
            float y = UnityEngine.Random.Range(-1f, 1f) * cameraShakeIntensity;

            Camera.main.transform.localPosition = originalPosition + new Vector3(x, y, 0);

            // Aplicar el zoom suave
            Camera.main.orthographicSize = Mathf.Lerp(originalSize, originalSize - zoomAmount, elapsedTime / stunDuration);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // Restablecer la posici�n y el tama�o de la c�mara al final de la vibraci�n
        Camera.main.transform.localPosition = originalPosition;
        Camera.main.orthographicSize = originalSize;
    }

    public void setStunned(bool newValue)
    {
        isStunned = newValue;
    }

    public void multiplySpeed(float rate)
    {
        speed = speed * rate;
    }
}
