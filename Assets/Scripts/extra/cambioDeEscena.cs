using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para la gestión de escenas

public class cambioDeEscena : MonoBehaviour
{
    void Update()
    {
        // Detectar la entrada del teclado numérico
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            CambiarEscena(11); // Cambiar a la escena 11
        }
        else if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            CambiarEscena(12); // Cambiar a la escena 12
        }
        else if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            CambiarEscena(13); // Cambiar a la escena 13
        }
        else if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            CambiarEscena(22); // Cambiar a la escena 0
        }

    }

    void CambiarEscena(int sceneNumber)
    {
        // Cambiar a la escena correspondiente
        SceneManager.LoadScene(sceneNumber);
    }
}
