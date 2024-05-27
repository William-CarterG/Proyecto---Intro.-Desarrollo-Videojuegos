using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InputFieldManager : MonoBehaviour
{
    public InputField inputField;  // Referencia al Input Field
    public Text errorMessage;  // Referencia al Text para mensajes de error
    public string correctCode = "123";  // El código correcto
    public string previousSceneName;  // El nombre de la escena anterior

    void Start()
    {
        // Asegúrate de que el Input Field esté enfocado al iniciar la escena
        inputField.ActivateInputField();
        inputField.onEndEdit.AddListener(CheckInput);

        // Inicialmente ocultar el mensaje de error
        errorMessage.gameObject.SetActive(false);
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
