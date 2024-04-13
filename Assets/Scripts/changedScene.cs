using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changedScene : MonoBehaviour
{
    public string sceneName;
    void OnTriggerEnter2D(Collider2D other)
    {
        // Verificar si el objeto que entró en el trigger tiene el tag "Player"
        if (other.CompareTag("Player"))
        {
            // Cambiar a la siguiente escena
            Debug.Log("CAMBIO DE ESCENA");
            SceneManager.LoadScene(sceneName);
        }
    }
}
