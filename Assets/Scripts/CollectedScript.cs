using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Unity.VisualScripting.Member;

public class CollectedScript : MonoBehaviour
{
    public List<string> collectedItems = new List<string>();
    public List<string> checkpointItems = new List<string>();
    public List<string> powerUps = new List<string>();
    private PowerUpsScript PUScript;
    // Start is called before the first frame update
    void Awake()
    {


        PUScript = GetComponent<PowerUpsScript>();
        LoadInventory();
        setPowerUps();
    }

    void Update()
    {
        CheckForKeyClick();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Collectible"))
        {
            CollectibleScript collectibleScript = collision.GetComponent<CollectibleScript>();

            collectedItems.Add(collectibleScript.Name);
            if (CheckIfPowerUp(collectibleScript.Name))
            {
                powerUps.Add(collectibleScript.Name);
                setPowerUps();
            }
            if (CheckIfSneakers(collectibleScript.Name))
            {
                PUScript.UseSneaker();
            }

            collectibleScript.collected();
        }
    }

    private void CheckForKeyClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null && hit.collider.CompareTag("CollectibleKey"))
            {
                CollectibleScript collectibleScript = hit.collider.GetComponent<CollectibleScript>();
                if (collectibleScript != null)
                {
                    collectedItems.Add(collectibleScript.Name);
                    collectibleScript.collected();
                    SaveInventory(false);
                }
            }
        }
    }
    
    public bool CanAccessPuzzle()
    {
        List<string> puzzlePieces = new List<string>();
        GameObject puzzle = GameObject.Find("collectibles");

        if (puzzle != null)
        {
            // Obtener todos los hijos del objeto padre
            foreach (Transform hijo in puzzle.transform)
            {
                // Agregar el nombre del hijo a la lista
                puzzlePieces.Add(hijo.name);
            }

            // Imprimir los nombres de los hijos en la consola (opcional)
            foreach (string nombre in puzzlePieces)
            {
                Debug.Log("Nombre del hijo: " + nombre);
            }
        }
        else
        {
            return false;
        }
        foreach (string piece in puzzlePieces)
        {
            if (!collectedItems.Contains(piece))
            {
                return false;
            }
        }
        return true;
    }


    public bool IsItemCollected(string itemName)
    {
        return collectedItems.Contains(itemName) || checkpointItems.Contains(itemName);
    }

    public bool IsKeyCollected(string keyName)
    {
        return collectedItems.Contains(keyName);
    }

    public string allItemsCollected()
    {
        string result = "";
        foreach (string element in collectedItems)
        {
            result += element;
            result += ", ";
        }
        return result;
    }

    public void SaveInventory(bool PassCheckpoint)
    {

        if (PassCheckpoint)
        {
            foreach (string item in collectedItems)
            {
                checkpointItems.Add(item);
            }
            string[] array = checkpointItems.ToArray();
            string json = JsonHelper.ToJson(array, true);
            PlayerPrefs.SetString("CheckpointItems", json);
        }
        else
        {
            string[] array = collectedItems.ToArray();
            string json = JsonHelper.ToJson(array, true);
            PlayerPrefs.SetString("LevelInventory", json);
        }
    }

    void LoadInventory()
    {
        string json = PlayerPrefs.GetString("CheckpointItems", "");
        if (!string.IsNullOrEmpty(json))
        {
            string[] array = JsonHelper.FromJson<string>(json);
            if (array != null)
            {
                collectedItems = new List<string>(array);
                Debug.Log("Checkpoint Inventory loaded: " + json);
            }
        }

        json = PlayerPrefs.GetString("LevelInventory", "");
        if (!string.IsNullOrEmpty(json))
        {
            string[] array = JsonHelper.FromJson<string>(json);
            if (array != null)
            {
                collectedItems = new List<string>(array);
                Debug.Log("Level Inventory loaded: " + json);
            }
        }

        foreach(string item in collectedItems)
        {
            if (CheckIfPowerUp(item))
            {
                powerUps.Add(item);
            }
        }

        foreach (string item in checkpointItems)
        {
            if (CheckIfPowerUp(item))
            {
                powerUps.Add(item);
            }
        }

        Debug.Log("Inventory is empty.");
    }

    public bool CheckIfPowerUp(string input)
    {
        if (input.Length >= 2)
        {
            return input.StartsWith("PU", System.StringComparison.Ordinal);
        }
        return false;
    }


    public bool CheckIfSneakers(string input)
    {
        if (input.Length >= 18)
        {
            return input.StartsWith("ConsumableSneakers", System.StringComparison.Ordinal);
        }
        return false;
    }
    public void setPowerUps()
    {
        if (powerUps.Count > 0)
        {
            PUScript.SetPowerUps(powerUps);
        }
    }

    public void DeleteInventory(bool deleteAll)
    {
        if (deleteAll)
        {
            PlayerPrefs.DeleteKey("CheckpointItems");
            collectedItems.Clear();
            PlayerPrefs.DeleteKey("LevelInventory");
            collectedItems.Clear();
        }
        PlayerPrefs.DeleteKey("LevelInventory");
        collectedItems.Clear();
    }
}
