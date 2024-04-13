using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowText : MonoBehaviour
{
    public GameObject floatingTextPrefab;
    public Canvas canvas; 
    public float despawnDelay = 2f; 

    private GameObject floatingTextInstance;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            Vector3 playerPosition = other.transform.position;
            Vector3 textPosition = new Vector3(playerPosition.x, playerPosition.y + 1f, playerPosition.z); 

            Vector2 screenPosition = Camera.main.WorldToScreenPoint(textPosition);

            floatingTextInstance = Instantiate(floatingTextPrefab, screenPosition, Quaternion.identity, canvas.transform);

            StartCoroutine(DespawnText());
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
