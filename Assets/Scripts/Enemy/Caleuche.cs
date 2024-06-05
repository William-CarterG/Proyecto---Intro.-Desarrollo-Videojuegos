using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caleuche : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float fireRate = 2f;
    public float projectileSpeed = 5f;
    public bool vertical;
    public bool horizontal;

    private float nextFireTime;

    void Start()
    {
        // Inicializar el temporizador de disparo
        nextFireTime = Time.time;
    }

    void Update()
    {
        // Verificar si es hora de disparar
        if (Time.time > nextFireTime)
        {
            // Disparar un proyectil
            if (horizontal)
            {
                FireProjectileVertical();
            }
            if (vertical)
            {
                
                FireProjectileHorizontal();
            }

            // Establecer el pr�ximo tiempo de disparo
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    void FireProjectileHorizontal()
    {
        // Instanciar tres proyectiles
        GameObject projectile1 = Instantiate(projectilePrefab, transform.position + Vector3.left * 2.5f, Quaternion.identity);
        GameObject projectile2 = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        GameObject projectile3 = Instantiate(projectilePrefab, transform.position + Vector3.right * 2.5f, Quaternion.identity);

        // Obtener los componentes Rigidbody2D de los proyectiles
        Rigidbody2D rb1 = projectile1.GetComponent<Rigidbody2D>();
        Rigidbody2D rb2 = projectile2.GetComponent<Rigidbody2D>();
        Rigidbody2D rb3 = projectile3.GetComponent<Rigidbody2D>();

        // Aplicar impulso a los proyectiles en una direcci�n fija (horizontal)
        rb1.AddForce(Vector2.left * projectileSpeed, ForceMode2D.Impulse);
        rb2.AddForce(Vector2.left * projectileSpeed, ForceMode2D.Impulse);
        rb3.AddForce(Vector2.left * projectileSpeed, ForceMode2D.Impulse);
    }

    void FireProjectileVertical()
    {
        // Instanciar tres proyectiles
        GameObject projectile1 = Instantiate(projectilePrefab, transform.position + Vector3.up * 2.5f, Quaternion.identity);
        GameObject projectile2 = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        GameObject projectile3 = Instantiate(projectilePrefab, transform.position + Vector3.down * 2.5f, Quaternion.identity);

        // Obtener los componentes Rigidbody2D de los proyectiles
        Rigidbody2D rb1 = projectile1.GetComponent<Rigidbody2D>();
        Rigidbody2D rb2 = projectile2.GetComponent<Rigidbody2D>();
        Rigidbody2D rb3 = projectile3.GetComponent<Rigidbody2D>();

        // Aplicar impulso a los proyectiles en una direcci�n fija (vertical)
        rb1.AddForce(Vector2.up * projectileSpeed, ForceMode2D.Impulse);
        rb2.AddForce(Vector2.up * projectileSpeed, ForceMode2D.Impulse);
        rb3.AddForce(Vector2.up * projectileSpeed, ForceMode2D.Impulse);
    }
}
