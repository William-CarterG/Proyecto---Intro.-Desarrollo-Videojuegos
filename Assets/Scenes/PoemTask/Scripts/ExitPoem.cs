using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string escapeSceneName = "nivelHernando";

    void Update()
    {
        // Verifica si se presion� la tecla Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Establecer las coordenadas para la posición (-14, -3.8)
            PlayerPrefs.SetFloat("PlayerPositionX", -13f);
            PlayerPrefs.SetFloat("PlayerPositionY", -3.8f);
            PlayerPrefs.Save();

            // Cargar la escena especificada para la tecla P
            SceneManager.LoadScene(escapeSceneName);
        }
    }
}
