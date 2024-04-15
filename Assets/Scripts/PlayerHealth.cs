using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 5; // Vidas iniciales
    public int currentHealth; // Vidas actuales
    public GameObject[] hearts;

    void Start()
    {
        // Configurar currentHealth con startingHealth
        currentHealth = startingHealth;

        // Sobrescribir currentHealth con el valor guardado en PlayerPrefs, si existe
        if (PlayerPrefs.HasKey("PlayerHealthActual"))
        {
            currentHealth = PlayerPrefs.GetInt("PlayerHealthActual");
        }

        // Actualizar el UI de la salud
        UpdateHealthUI();
    }

    // Método para restar vidas
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        UpdateHealthUI();
        PlayerPrefs.SetInt("PlayerHealthActual", currentHealth);
        // Comprobar si el jugador ha perdido todas las vidas
        if (currentHealth <= 0)
        {
            PlayerPrefs.DeleteKey("PlayerHealthActual");
            Debug.Log("Game Over");
            SceneManager.LoadScene("GameOver");
        }
    }

    // Método para actualizar el UI de los corazones
    void UpdateHealthUI()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].SetActive(i < currentHealth);
        }
    }

    // Ajuste para colisiones en un contexto 2D
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Collision with enemy!");

            int damageAmount = 1;
            TakeDamage(damageAmount);
        }
    }
}
