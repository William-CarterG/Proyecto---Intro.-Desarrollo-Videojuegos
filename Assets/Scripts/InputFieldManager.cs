using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InputFieldManager : MonoBehaviour
{
    public InputField inputField;  // Referencia al Input Field
    public string correctCode = "123";  // El código correcto
    public string previousSceneName;  // El nombre de la escena anterior

    void Start()
    {
        // Asegúrate de que el Input Field esté enfocado al iniciar la escena
        inputField.ActivateInputField();
        inputField.onEndEdit.AddListener(CheckInput);
    }

    void CheckInput(string userInput)
    {
        if (userInput == correctCode)
        {
            // Cargar la escena anterior
            SceneManager.LoadScene(previousSceneName);
        }
    }
}
