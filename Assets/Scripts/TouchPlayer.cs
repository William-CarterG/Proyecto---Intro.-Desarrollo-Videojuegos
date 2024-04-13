using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public string tagPlayer = "Player"; // Etiqueta del jugador
    public Vector2 posicionTeletransporte = new Vector2(6.5f, 1f); // Posición de teletransporte

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(tagPlayer))
        {
            // Si el objeto chocado es el jugador, teletransportarlo a la posición especificada
            collision.transform.position = posicionTeletransporte;
        }
    }
}
