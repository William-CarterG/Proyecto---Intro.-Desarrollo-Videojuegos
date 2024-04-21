using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changedScene : MonoBehaviour
{
    public string sceneName;
    private SaveLoadPlayerState SavingScript;

    void Start()
    {
        SavingScript = GameObject.FindWithTag("Player").GetComponent<SaveLoadPlayerState>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        // Verificar si el objeto que entró en el trigger tiene el tag "Player"
        if (other.CompareTag("Player"))
        {
            // Cambiar a la siguiente escena
            Debug.Log("CAMBIO DE ESCENA");
            SavingScript.SaveAll();
            SceneManager.LoadScene(sceneName);
        }
    }
}
