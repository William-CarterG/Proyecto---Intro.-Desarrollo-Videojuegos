using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterMiniGame : MonoBehaviour
{
    public string sceneToLoad;
    public int completed;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {

            // Load the specified scene
            /*
             * GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                // Obtener la posición del objeto "Player"
                Vector3 playerPosition = player.transform.position;
                Debug.Log("Player position: " + playerPosition);
            }
                        completed = PlayerPrefs.GetInt("MiniGameCompleted1");
            Debug.Log("In EnterMiniGame [ANTES]: "+completed);
            PlayerPrefs.SetInt("MiniGameCompleted1", 1);
            completed = PlayerPrefs.GetInt("MiniGameCompleted1");
            Debug.Log("In EnterMiniGame [DESPUES]: " + completed);
            */
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
