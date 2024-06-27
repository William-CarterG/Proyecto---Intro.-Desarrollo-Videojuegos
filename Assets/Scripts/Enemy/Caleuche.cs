using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caleuche : MonoBehaviour
{
    public GameObject projectilePrefabVertical;
    public GameObject projectilePrefabHorizontal;
    public float fireRate = 2f;
    public float projectileSpeed = 5f;
    public bool vertical;
    public bool horizontal;
    public AudioClip fireSound; // Clip de sonido para el disparo
    public float detectionDistanceY = 11f; // Distancia de detección en el eje y
    public float detectionDistanceX = 23f;
    public float detectionDistanceXHorizontal = 2.5f;
    public float detectionDistanceYVertical = 2.5f;

    private Transform playerTransform; // Referencia al transform del jugador
    private float nextFireTime;

    void Start()
    {
        // Inicializar el temporizador de disparo
        nextFireTime = Time.time;
        // Obtener la referencia al jugador
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (playerTransform == null)
            return;

        // Verificar la posición del jugador
        float distanceToPlayerY = playerTransform.position.y - transform.position.y;
        float distanceToPlayerX = playerTransform.position.x - transform.position.x;
        float distanceToPlayerXHorizontal = Mathf.Abs(playerTransform.position.x - transform.position.x);
        float distanceToPlayerYVertical = Mathf.Abs(playerTransform.position.y - transform.position.y);

        // Árbol de decisiones
        if (IsTimeToFire())
        {
            if (horizontal && IsPlayerInHorizontalRange(distanceToPlayerX, distanceToPlayerYVertical))
            {
                FireProjectileHorizontal();
            }
            else if (vertical && IsPlayerInVerticalRange(distanceToPlayerY, distanceToPlayerXHorizontal))
            {
                FireProjectileVertical();
            }

            // Establecer el próximo tiempo de disparo
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    bool IsTimeToFire()
    {
        return Time.time > nextFireTime;
    }

    bool IsPlayerInHorizontalRange(float distanceToPlayerX, float distanceToPlayerYVertical)
    {
        return distanceToPlayerX > 0 && distanceToPlayerX <= detectionDistanceX && distanceToPlayerYVertical <= detectionDistanceYVertical;
    }

    bool IsPlayerInVerticalRange(float distanceToPlayerY, float distanceToPlayerXHorizontal)
    {
        return distanceToPlayerY > 0 && distanceToPlayerY <= detectionDistanceY && distanceToPlayerXHorizontal <= detectionDistanceXHorizontal;
    }

    void FireProjectileHorizontal()
    {
        // Instanciar tres proyectiles
        GameObject projectile1 = Instantiate(projectilePrefabHorizontal, transform.position + Vector3.left * 2.5f, Quaternion.identity);
        GameObject projectile2 = Instantiate(projectilePrefabHorizontal, transform.position, Quaternion.identity);
        GameObject projectile3 = Instantiate(projectilePrefabHorizontal, transform.position + Vector3.right * 2.5f, Quaternion.identity);

        // Obtener los componentes Rigidbody2D de los proyectiles
        Rigidbody2D rb1 = projectile1.GetComponent<Rigidbody2D>();
        Rigidbody2D rb2 = projectile2.GetComponent<Rigidbody2D>();
        Rigidbody2D rb3 = projectile3.GetComponent<Rigidbody2D>();

        // Aplicar impulso a los proyectiles en una dirección fija (horizontal)
        rb1.AddForce(Vector2.left * projectileSpeed, ForceMode2D.Impulse);
        rb2.AddForce(Vector2.left * projectileSpeed, ForceMode2D.Impulse);
        rb3.AddForce(Vector2.left * projectileSpeed, ForceMode2D.Impulse);

        // Reproducir el sonido de disparo
        PlayFireSound();
    }

    void FireProjectileVertical()
    {
        // Instanciar tres proyectiles
        GameObject projectile1 = Instantiate(projectilePrefabVertical, transform.position + Vector3.up * 2.5f, Quaternion.identity);
        GameObject projectile2 = Instantiate(projectilePrefabVertical, transform.position, Quaternion.identity);
        GameObject projectile3 = Instantiate(projectilePrefabVertical, transform.position + Vector3.down * 2.5f, Quaternion.identity);

        // Obtener los componentes Rigidbody2D de los proyectiles
        Rigidbody2D rb1 = projectile1.GetComponent<Rigidbody2D>();
        Rigidbody2D rb2 = projectile2.GetComponent<Rigidbody2D>();
        Rigidbody2D rb3 = projectile3.GetComponent<Rigidbody2D>();

        // Aplicar impulso a los proyectiles en una dirección fija (vertical)
        rb1.AddForce(Vector2.up * projectileSpeed, ForceMode2D.Impulse);
        rb2.AddForce(Vector2.up * projectileSpeed, ForceMode2D.Impulse);
        rb3.AddForce(Vector2.up * projectileSpeed, ForceMode2D.Impulse);

        // Reproducir el sonido de disparo
        PlayFireSound();
    }

    void PlayFireSound()
    {
        if (fireSound != null)
        {
            AudioSource.PlayClipAtPoint(fireSound, transform.position);
        }
    }
}
