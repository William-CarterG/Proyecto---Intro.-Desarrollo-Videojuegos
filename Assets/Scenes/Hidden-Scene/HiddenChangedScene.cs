using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HiddenChangedScene : MonoBehaviour
{
    public string sceneName;
    public int sceneIndex;

    public SceneFader sceneFader;

    public bool PassCheckpoint;

    private SaveLoadPlayerState SavingScript;

    void Start()
    {
        SavingScript = GameObject.FindWithTag("Player").GetComponent<SaveLoadPlayerState>();
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
        Debug.Log("Choca con el jugador");
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the trigger");

            if (SavingScript != null)
            {
                Debug.Log("Saving player state");
                SavingScript.SaveAll(PassCheckpoint);

            }
            SceneManager.LoadScene(sceneIndex);
            /*
            if (sceneFader != null)
            {
                Debug.Log("Initiating scene fade to: " + sceneName);
                sceneFader.FadeToScene(sceneName);
            }
            else
            {
                Debug.LogError("SceneFader is not set");
            }
            */
        }
    }
}
