using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changedScene : MonoBehaviour
{
    public string sceneName;
    public int sceneIndex;

    public SceneFader sceneFader;

    public bool PassCheckpoint;

    private SaveLoadPlayerState SavingScript;
    private PlayerPosition playerPositionScript;

    void Start()
    {
        SavingScript = GameObject.FindWithTag("Player").GetComponent<SaveLoadPlayerState>();
        playerPositionScript = GameObject.FindWithTag("Player").GetComponent<PlayerPosition>();

        /*
        if (SavingScript == null)
        {
            Debug.LogError("SaveLoadPlayerState script not found on Player");
        }

        if (sceneFader == null)
        {
            Debug.LogError("SceneFader is not assigned");
        }
        */
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Verify if the object that entered the trigger has the tag "Player"
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the trigger");

            if (SavingScript != null)
            {
                Debug.Log("Saving player state");
                SavingScript.SaveAll(PassCheckpoint);
            }

            if (playerPositionScript != null)
            {
                Debug.Log("Saving player position");
                playerPositionScript.SavePosition();
            }

            // Guardar el nombre de la escena actual
            PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);
            PlayerPrefs.Save();

            if (sceneFader != null)
            {
                Debug.Log("Initiating scene fade to: " + sceneName);
                sceneFader.FadeToScene(sceneName);
            }
            else
            {
                SceneManager.LoadSceneAsync(sceneName);
            }
        }
    }
}
