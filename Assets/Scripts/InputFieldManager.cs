using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InputFieldManager : MonoBehaviour
{
    public InputField inputField;  // Referencia al Input Field
    public Text errorMessage;  // Referencia al Text para mensajes de error
    public string correctCode = "632514";  // El código correcto
    public string previousSceneName;  // El nombre de la escena anterior
    public string escapeSceneName = "SampleScene";  // El nombre de la escena a cargar al presionar Esc

    void Start()
    {
        // Asegúrate de que el Input Field esté enfocado al iniciar la escena
        inputField.ActivateInputField();
        inputField.onEndEdit.AddListener(CheckInput);

        // Inicialmente ocultar el mensaje de error
        errorMessage.gameObject.SetActive(false);
    }

    void Update()
    {
        // Verificar si se ha presionado la tecla Esc
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Establecer las coordenadas para la posición (0, 0)
            PlayerPrefs.SetFloat("PlayerPositionX", 0f);
            PlayerPrefs.SetFloat("PlayerPositionY", 0f);
            PlayerPrefs.Save();

            // Cargar la escena especificada para la tecla Esc
            SceneManager.LoadScene(escapeSceneName);
        }
    }

    void CheckInput(string userInput)
    {
        if (userInput == correctCode)
        {
            // Cargar la escena anterior
            SceneManager.LoadScene(previousSceneName);
        }
        else
        {
            // Mostrar el mensaje de error
            errorMessage.text = "Código incorrecto.";
            errorMessage.gameObject.SetActive(true);

            // Reactivar el InputField para que el usuario pueda intentar nuevamente
            inputField.text = "";  // Limpiar el campo de entrada
            inputField.ActivateInputField();  // Reactivar el campo de entrada
        }
    }
}
