using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFinder : MonoBehaviour
{
    public float velocidad = 3f; // Velocidad de persecución
    public string tagPlayer = "Player"; // Etiqueta del jugador a seguir

    private Transform player; // Referencia al transform del jugador

    void Start()
    {
        // Buscar el jugador al inicio del juego
        player = GameObject.FindGameObjectWithTag(tagPlayer).transform;
    }

    void Update()
    {
        if (player != null)
        {
            // Calcular la dirección hacia el jugador
            Vector2 direccion = (player.position - transform.position).normalized;

            // Mover al enemigo en la dirección del jugador con una velocidad variable
            transform.position += (Vector3)direccion * velocidad * Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Si el objeto con el que colisiona no es el jugador, cambiar de dirección
        if (!collision.gameObject.CompareTag(tagPlayer))
        {
            velocidad = -velocidad;
        }
    }
}
