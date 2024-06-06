using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 5; // Vidas iniciales
    public int currentHealth; // Vidas actuales
    private GameObject[] hearts;
    public GameObject HealthMeter;
    public AudioClip hitSound; // Clip de sonido para el golpe

    private AudioSource audioSource; // Componente de AudioSource

    void Start()
    {
        HealthMeter = GameObject.Find("HealthMeter");
        hearts = new GameObject[5];

        if (HealthMeter != null)
        {
            for (int i = 0; i < 5; i++)
            {
                hearts[i] = HealthMeter.transform.Find("h" + (i + 1)).gameObject;
            }
        }

        // Configurar currentHealth con startingHealth
        currentHealth = startingHealth;

        // Sobrescribir currentHealth con el valor guardado en PlayerPrefs, si existe
        if (PlayerPrefs.HasKey("PlayerHealthActual"))
        {
            currentHealth = PlayerPrefs.GetInt("PlayerHealthActual");
        }

        // Obtener el componente AudioSource
        audioSource = GetComponent<AudioSource>();

        // Actualizar el UI de la salud
        UpdateHealthUI();
    }

    // Método para restar vidas
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        UpdateHealthUI();
        PlayerPrefs.SetInt("PlayerHealthActual", currentHealth);
        PlayerPrefs.SetInt("MiniGameCompleted1", 0);

        // Reproducir el sonido de golpe
        PlayHitSound();

        // Comprobar si el jugador ha perdido todas las vidas
        if (currentHealth <= 0)
        {
            PlayerPrefs.DeleteKey("PlayerHealthActual");
            PlayerPrefs.DeleteKey("MiniGameCompleted1");
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

            Debug.Log("Enemy  --> [ANTES]: " + PlayerPrefs.GetInt("MiniGameCompleted1"));

            int damageAmount = 1;
            TakeDamage(damageAmount);
            Debug.Log("Enemy  --> [DESPUES]: " + PlayerPrefs.GetInt("MiniGameCompleted1"));
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            TakeDamage(1);
        }
    }

    void OnApplicationQuit()
    {
        PlayerPrefs.DeleteKey("PlayerHealth");
    }

    void PlayHitSound()
    {
        if (audioSource != null && hitSound != null)
        {
            audioSource.PlayOneShot(hitSound);
        }
    }
}
