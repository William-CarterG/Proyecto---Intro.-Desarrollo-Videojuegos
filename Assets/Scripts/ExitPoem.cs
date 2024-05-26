using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string sceneToLoad;  // El nombre de la escena a la que quieres cambiar

    void Update()
    {
        // Verifica si se presion� la tecla Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Carga la escena especificada
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
