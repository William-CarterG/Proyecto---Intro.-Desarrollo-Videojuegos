using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubtitleManager : MonoBehaviour
{
    public GameObject floatingTextPrefab; // Prefab del texto flotante
    public Canvas canvas; // Canvas UI
    public float despawnDelay = 2f;
    public bool playerInside = false;

    private GameObject floatingTextInstance;

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
            }

            playerInside = false;
        }
    }

    IEnumerator DespawnText()
    {
        yield return new WaitForSeconds(despawnDelay);

        if (floatingTextInstance != null)
        {
            Destroy(floatingTextInstance);
        }
    }
}
