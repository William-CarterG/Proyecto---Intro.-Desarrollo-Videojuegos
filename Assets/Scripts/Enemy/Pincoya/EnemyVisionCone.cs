using UnityEngine;

public class EnemyVisionCone : MonoBehaviour
{
    public float rotationSpeed = 30f;  // Speed of rotation in degrees per second
    public int damage = 1;  // Amount of damage to apply
    public AudioClip detectionSound; // Clip de sonido para la detección
    private Transform visionConeTransform;

    void Start()
    {
        // Find the Vision Cone child object
        visionConeTransform = transform.Find("VisionCone");

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
        if (visionConeTransform != null)
        {
            // Rotate the vision cone
            visionConeTransform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                PlayDetectionSound(); // Reproducir el sonido de detección
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
}
