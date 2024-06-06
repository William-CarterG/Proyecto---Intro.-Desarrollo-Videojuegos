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

    private AudioSource audioSource; // Componente de AudioSource
    private float nextFireTime;

    void Start()
    {
        // Inicializar el temporizador de disparo
        nextFireTime = Time.time;

        // Obtener el componente AudioSource
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Verificar si es hora de disparar
        if (Time.time > nextFireTime)
        {
            // Disparar un proyectil
            if (horizontal)
            {
                FireProjectileHorizontal();
            }
            if (vertical)
            {
                FireProjectileVertical();
            }

            // Reproducir el sonido de disparo
            PlayFireSound();

            // Establecer el próximo tiempo de disparo
            nextFireTime = Time.time + 1f / fireRate;
        }
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
    }

    void PlayFireSound()
    {
        if (audioSource != null && fireSound != null)
        {
            audioSource.PlayOneShot(fireSound);
        }
    }
}
