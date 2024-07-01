using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class PlayerPosition : MonoBehaviour
{
    private Vector2 lastPosition;
    private List<string> scenesToLoadPositionFrom; // Hacer la lista pública

    void Start()
    {
        scenesToLoadPositionFrom = new List<string> { "Ascensor", "Grate", "CableLockedPuzzle", "CablePuzzleCompleted", "CableUnlockedPuzzle", "PuzzleScene", "Vias Tren", "Cables", "Baño", "Poem", "PoemDoor", "SwitchScene", "WireScene" };

        // Obtener la escena anterior
        string previousScene = PlayerPrefs.GetString("PreviousScene", "");

        // Cargar la posición guardada al iniciar la escena si la escena anterior es válida
        if (scenesToLoadPositionFrom.Contains(previousScene) && !(SceneManager.GetActiveScene().name == "BajoHernandoDeMagallanes"))
        {
            Debug.Log("Llega aqui");
            if (PlayerPrefs.HasKey("PlayerX") && PlayerPrefs.HasKey("PlayerY"))
            {
                float x = PlayerPrefs.GetFloat("PlayerX");
                float y = PlayerPrefs.GetFloat("PlayerY");
                transform.position = new Vector2(x, y);
            }
        }

        // Registrar el evento Application.quitting para ejecutar algo antes de salir del Play Mode
        Application.quitting += OnApplicationQuitting;
    }

    void Update()
    {
        // Actualizar la última posición
        lastPosition = transform.position;

        // Opcional: Guardar posición cuando se presiona una tecla o se detecta un cambio de escena
        if (Input.GetKeyDown(KeyCode.E))
        {
            SavePosition();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);
            PlayerPrefs.Save();
            Debug.Log(SceneManager.GetActiveScene().name);
        }
    }

    private void OnApplicationQuitting()
    {
        // Aquí puedes poner el código que deseas ejecutar justo antes de salir del Play Mode
        Debug.Log("Ejecutando algo antes de salir del Play Mode");

        PlayerPrefs.DeleteKey("PreviousScene");
    }

    public void SavePosition()
    {
        // Guardar la posición actual
        PlayerPrefs.SetFloat("PlayerX", lastPosition.x);
        PlayerPrefs.SetFloat("PlayerY", lastPosition.y);
        PlayerPrefs.Save();
    }

    private void OnDestroy()
    {
        // Asegúrate de quitar el evento al destruir el objeto
        Application.quitting -= OnApplicationQuitting;
    }
}
