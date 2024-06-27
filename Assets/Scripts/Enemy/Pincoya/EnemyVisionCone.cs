using System.Collections;
using UnityEngine;

public class EnemyVisionCone : MonoBehaviour
{
    public float rotationSpeed = 50f;  // Speed of rotation in degrees per second
    public int damage = 1;  // Amount of damage to apply
    public AudioClip detectionSound; // Clip de sonido para la detecci�n
    public float attackCooldown = 3f; // Tiempo de enfriamiento entre ataques
    public float attackDistance = 5f; // Distancia a la que comienza a atacar
    private Transform visionConeTransform;
    private Transform playerTransform; // Referencia al transform del jugador
    private bool canAttack = true; // Indica si el enemigo puede atacar

    void Start()
    {
        // Find the Vision Cone child object
        visionConeTransform = transform.Find("VisionCone");

        // Obtener la referencia al jugador
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        // Set the initial color (optional, if not set in the material)
        if (visionConeTransform != null)
        {
            SpriteRenderer sr = visionConeTransform.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.color = new Color(1f, 1f, 0.7f, 0.5f); // Pale yellow with some transparency
            }
        }
    }

    void Update()
    {
        if (visionConeTransform != null && playerTransform != null)
        {
            // Verificar la distancia al jugador
            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

            // �rbol de decisiones
            {
                // Acci�n: Rotar el cono de visi�n
                RotateVisionCone();

                // Acci�n: Atacar si el jugador est� dentro del rango y puede atacar
                if (distanceToPlayer <= attackDistance && canAttack)
                {
                    Debug.Log("entra");
                    StartCoroutine(AttackPlayer());
                }
            }
        }
    }

    void RotateVisionCone()
    {
        // Rotate the vision cone
        visionConeTransform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }

    IEnumerator AttackPlayer()
    {
        canAttack = false; // Desactivar la habilidad de atacar durante el enfriamiento

        // Acci�n: Realizar el ataque
        Debug.Log("Atacando al jugador!");

        // Simular tiempo de ataque
        yield return new WaitForSeconds(1.5f); // Ajustar seg�n la animaci�n o el comportamiento del ataque

        canAttack = true; // Permitir que el enemigo pueda atacar nuevamente despu�s del enfriamiento
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                // Acci�n: Aplicar da�o al jugador
                playerHealth.TakeDamage(damage);

                // Acci�n: Reproducir sonido de detecci�n
                PlayDetectionSound();

                // Acci�n: Iniciar parpadeo del cono de visi�n
                StartCoroutine(BlinkVisionCone(2f));
            }
        }
    }

    void PlayDetectionSound()
    {
        if (detectionSound != null)
        {
            AudioSource.PlayClipAtPoint(detectionSound, transform.position);
        }
    }

    IEnumerator BlinkVisionCone(float duration)
    {
        if (visionConeTransform != null)
        {
            SpriteRenderer sr = visionConeTransform.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                float elapsedTime = 0f;

                while (elapsedTime < duration)
                {
                    sr.enabled = !sr.enabled; // Toggle visibility
                    elapsedTime += 0.2f; // Adjust the blinking interval as needed
                    yield return new WaitForSeconds(0.2f); // Adjust the blinking interval as needed
                }

                sr.enabled = true; // Ensure the sprite renderer is enabled after blinking
            }
        }
    }
}
