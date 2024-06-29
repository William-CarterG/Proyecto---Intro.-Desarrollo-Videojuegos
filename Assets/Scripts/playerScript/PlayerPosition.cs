using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPosition : MonoBehaviour
{
    private Vector2 lastPosition;
    public bool Used;

    void Start()
    {
        // Cargar la posición guardada al iniciar la escena
        if (Used)
        {
            Debug.Log("Llega aqui");
            if (PlayerPrefs.HasKey("PlayerX") && PlayerPrefs.HasKey("PlayerY"))
            {
                
                float x = PlayerPrefs.GetFloat("PlayerX");
                float y = PlayerPrefs.GetFloat("PlayerY");
                transform.position = new Vector2(x, y);
            }
        }

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
    }

    public void SavePosition()
    {
        // Guardar la posición actual
        PlayerPrefs.SetFloat("PlayerX", lastPosition.x);
        PlayerPrefs.SetFloat("PlayerY", lastPosition.y);
        PlayerPrefs.Save();
    }
}
