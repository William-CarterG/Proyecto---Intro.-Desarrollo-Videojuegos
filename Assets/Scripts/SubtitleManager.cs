using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Import the TextMeshPro namespace

public class SubtitleManager : MonoBehaviour
{
    public GameObject floatingTextPrefab; // Prefab del texto flotante
    public Canvas canvas; // Canvas UI
    public float despawnDelay = 2f;
    public bool playerInside = false;

    private GameObject floatingTextInstance;
    private TextMeshProUGUI floatingText;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (floatingTextInstance == null)
            {
                // Instancia el texto flotante como hijo del Canvas
                floatingTextInstance = Instantiate(floatingTextPrefab, canvas.transform);

                // Ajusta la posición para que esté en la parte inferior de la pantalla
                RectTransform rectTransform = floatingTextInstance.GetComponent<RectTransform>();
                rectTransform.anchorMin = new Vector2(0.5f, 0); // Ancla en la parte inferior central
                rectTransform.anchorMax = new Vector2(0.5f, 0);
                rectTransform.pivot = new Vector2(0.5f, 0);
                rectTransform.anchoredPosition = new Vector2(0, 20); // Ajusta el valor en Y según sea necesario

                floatingText = floatingTextInstance.GetComponent<TextMeshProUGUI>();

                // Asegurarse de que se encontró el componente de texto
                if (floatingText == null)
                {
                    Debug.LogError("No se encontró el componente TextMeshProUGUI en el prefab del texto flotante.");
                }
                
                StartCoroutine(DespawnText());
            }

            playerInside = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StopCoroutine(DespawnText());

            if (floatingTextInstance != null)
            {
                Destroy(floatingTextInstance);
                floatingText = null; // Resetear el floatingText a null cuando el objeto es destruido
            }

            playerInside = false;
        }
    }

    public void ChangeText(string newMessage)
    {
        floatingText.text = newMessage;
    }

    IEnumerator DespawnText()
    {
        yield return new WaitForSeconds(despawnDelay);

        if (floatingTextInstance != null)
        {
            Destroy(floatingTextInstance);
            floatingText = null; // Resetear el floatingText a null cuando el objeto es destruido
        }
    }

    public void ShowTemporaryMessage(string message, float duration)
    {
        if (floatingTextInstance != null && floatingText != null)
        {
            StopCoroutine(DespawnText());
            StartCoroutine(ShowTemporaryMessageCoroutine(message, duration));
        }
        else
        {
            Debug.LogWarning("No se puede mostrar el mensaje temporal porque el texto flotante no está instanciado o no se encontró el componente TextMeshProUGUI.");
        }
    }

    private IEnumerator ShowTemporaryMessageCoroutine(string message, float duration)
    {
        string originalMessage = floatingText.text;
        floatingText.text = message;
        yield return new WaitForSeconds(duration);
        floatingText.text = originalMessage;
    }
}
