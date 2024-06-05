using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    public float speed = 2f;
    public float neighborDistance = 2f;
    public float separationDistance = 1f;
    public float followDistance = 0.5f; // Distancia m�nima para seguir al objetivo
    public float randomness = 1f; // Factor de aleatoriedad

    private Vector2 velocity;
    public Transform target; // Objetivo a seguir
    private Vector2 lastTargetPosition; // �ltima posici�n del objetivo

    void Start()
    {
        // Inicializar la velocidad aleatoriamente
        velocity = Random.insideUnitCircle.normalized * speed;

        // Ignorar colisiones con el objetivo
        if (target != null)
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), target.GetComponent<Collider2D>());
            lastTargetPosition = target.position;
        }
    }

    void Update()
    {
        logicBoid();
        FollowTarget(); // L�gica para seguir al objetivo
    }

    void logicBoid()
    {
        Vector2 acceleration = Vector2.zero;

        // Obtener todos los boids
        Boid[] boids = FindObjectsOfType<Boid>();

        // Variables para calcular las fuerzas
        Vector2 alignment = Vector2.zero;
        Vector2 cohesion = Vector2.zero;
        Vector2 separation = Vector2.zero;
        int count = 0;

        foreach (Boid boid in boids)
        {
            if (boid == this) continue;

            float distance = Vector2.Distance(transform.position, boid.transform.position);

            if (distance < neighborDistance)
            {
                // Alineaci�n: hacer que los boids se alineen con sus vecinos
                alignment += boid.velocity;

                // Cohesi�n: hacer que los boids se acerquen al centro de masa de sus vecinos
                cohesion += (Vector2)boid.transform.position;

                // Separaci�n: hacer que los boids se alejen de sus vecinos si est�n demasiado cerca
                if (distance < separationDistance)
                {
                    separation += (Vector2)(transform.position - boid.transform.position) / distance;
                }

                count++;
            }
        }

        if (count > 0)
        {
            alignment /= count;
            alignment = alignment.normalized * speed;

            cohesion /= count;
            cohesion = ((Vector2)cohesion - (Vector2)transform.position).normalized * speed;

            separation /= count;
            separation = separation.normalized * speed;

            // Sumar las fuerzas al vector de aceleraci�n
            acceleration = alignment + cohesion + separation;
        }

        // A�adir aleatoriedad a la aceleraci�n
        acceleration += Random.insideUnitCircle * randomness;

        // Actualizar la velocidad y la posici�n del boid
        velocity += acceleration * Time.deltaTime;
        velocity = velocity.normalized * speed;
        transform.position += (Vector3)velocity * Time.deltaTime;

        // Ajustar la rotaci�n del boid en la direcci�n de la velocidad
        float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    void FollowTarget()
    {
        if (target != null)
        {
            // Comprobar si el objetivo ha cambiado de direcci�n
            if ((Vector2)target.position != lastTargetPosition)
            {
                // Obtener la direcci�n hacia el objetivo
                Vector2 directionToTarget = (Vector2)target.position - (Vector2)transform.position;
                directionToTarget = directionToTarget.normalized * speed;

                // Ajustar la velocidad del boid para seguir al objetivo
                velocity = directionToTarget;

                // Ajustar la rotaci�n del boid en la direcci�n del objetivo
                float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

                // Actualizar la �ltima posici�n del objetivo
                lastTargetPosition = target.position;
            }

            // Seguir al objetivo manteniendo una distancia m�nima
            if (Vector2.Distance(transform.position, target.position) > followDistance)
            {
                // A�adir aleatoriedad a la direcci�n
                Vector2 randomOffset = Random.insideUnitCircle * randomness;

                // Ajustar la posici�n del boid
                transform.position += (Vector3)(velocity + randomOffset) * Time.deltaTime;
            }
        }
    }
}
