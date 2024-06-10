using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangerWires : MonoBehaviour
{
    public string escapeSceneName = "EntradaBaquedano";

    void Update()
    {
        // Verifica si se presion� la tecla Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Establecer las coordenadas para la posición (-14, -3.8)
            PlayerPrefs.SetFloat("PlayerPositionX", -20f);
            PlayerPrefs.SetFloat("PlayerPositionY", -14f);
            PlayerPrefs.Save();

            // Cargar la escena especificada para la tecla P
            SceneManager.LoadScene(escapeSceneName);
        }
    }
}
