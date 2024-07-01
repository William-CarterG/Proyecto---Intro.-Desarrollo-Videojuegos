using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterGrieta : MonoBehaviour
{
    public string sceneToLoad;
    public int completed;
    private ShowText ZoneScript;
    private CollectedScript Inventory;
    private SaveLoadPlayerState SavingScript;

    private void Start()
    {
        ZoneScript = GetComponent<ShowText>();
        GameObject PlayerObject = GameObject.Find("Player");
        Inventory = PlayerObject.GetComponent<CollectedScript>();
        SavingScript = GameObject.FindWithTag("Player").GetComponent<SaveLoadPlayerState>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {

            if (ZoneScript.playerInside)
            {
                SavingScript.SaveAll(false);
                // Guardar el nombre de la escena actual
                PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);
                PlayerPrefs.Save();
                SceneManager.LoadScene(sceneToLoad);
                PlayerPrefs.SetInt("SaveScene", SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}