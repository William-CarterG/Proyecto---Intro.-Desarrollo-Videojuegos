

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterCablePanel: MonoBehaviour
{
    public string sceneToLoadLocked;
    public string sceneToLoadUnlocked;
    public string sceneToLoadCompleted;
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
                if (Inventory.IsKeyCollected("Key")) 
                {
                    SceneManager.LoadScene(sceneToLoadUnlocked);
                }
                else 
                {
                    SceneManager.LoadScene(sceneToLoadLocked);
                }
                string completedPuzzle = PlayerPrefs.GetString("CablePuzzleComplete", "");
                if (!string.IsNullOrEmpty(completedPuzzle))
                {
                    if (completedPuzzle == "true")
                    {
                        SceneManager.LoadScene(sceneToLoadCompleted);
                    }
                }
                PlayerPrefs.SetInt("SaveScene", SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}
