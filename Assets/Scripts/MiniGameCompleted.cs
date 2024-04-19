using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameCompleted : MonoBehaviour
{
    // Start is called before the first frame update
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("MiniGameCompleted1") == 1)
        {
            spriteRenderer.color = Color.green;
        }
        else
        {
            spriteRenderer.color = Color.blue;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Si el objeto con el que colisiona no es el jugador, cambiar de dirección
        Debug.Log("in MiniGameCompleted ->" + PlayerPrefs.GetInt("MiniGameCompleted1"));
        if (PlayerPrefs.GetInt("MiniGameCompleted1") == 1)
        {
            Debug.Log("Es un True");
            GetComponent<BoxCollider2D>().isTrigger = true;
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Es un false");
            GetComponent<BoxCollider2D>().isTrigger = false;
            gameObject.SetActive(true);
        }
    }
}
