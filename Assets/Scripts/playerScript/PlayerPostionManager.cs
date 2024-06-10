using UnityEngine;

public class PlayerPositionManager : MonoBehaviour
{
    public GameObject player;  // Referencia al jugador

    void Start()
    {
        // Verificar si el jugador ha sido asignado en el Inspector
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");  // Asume que el jugador tiene la etiqueta "Player"
        }

        // Verificar y obtener las coordenadas del jugador en PlayerPrefs
        if (PlayerPrefs.HasKey("PlayerPositionX") && PlayerPrefs.HasKey("PlayerPositionY"))
        {
            float x = PlayerPrefs.GetFloat("PlayerPositionX");
            float y = PlayerPrefs.GetFloat("PlayerPositionY");

            // Mover el jugador a la posición almacenada
            player.transform.position = new Vector3(x, y, 0);

            // Limpiar las coordenadas en PlayerPrefs
            PlayerPrefs.DeleteKey("PlayerPositionX");
            PlayerPrefs.DeleteKey("PlayerPositionY");
            PlayerPrefs.Save();
        }
    }
}
