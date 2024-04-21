using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectedScript : MonoBehaviour
{
    public List<string> collectedItems = new List<string>();
    private Action<bool> inventoryLoadedCallback;
    // Start is called before the first frame update
    void Start()
    {
        LoadInventory();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Collectible"))
        {
            CollectibleScript collectibleScript = collision.GetComponent<CollectibleScript>();
            collectedItems.Add(collectibleScript.Name);
            collectibleScript.collected();
        }
    }

    public bool CanAccessPuzzle()
    {
        List<string> puzzlePieces = new List<string> { "PuzzlePiece1", "PuzzlePiece2", "PuzzlePiece3",
                                                        "PuzzlePiece4", "PuzzlePiece5", "PuzzlePiece6",
                                                        "PuzzlePiece7", "PuzzlePiece8", "PuzzlePiece9" };
        foreach (string piece in puzzlePieces)
        {
            if (!collectedItems.Contains(piece))
            {
                return false;
            }
        }
        return true;
    }

    public void IsItemCollected(string itemName, Action<bool> callback)
    {
        if (collectedItems.Count == 0)
        {
            inventoryLoadedCallback = callback;
        }
        else
        {
            callback(collectedItems.Contains(itemName));
        }
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

    public void SaveInventory()
    {

        string[] array = collectedItems.ToArray();
        string json = JsonHelper.ToJson(array, true);
        PlayerPrefs.SetString("Inventory", json);
    }

    void LoadInventory()
    {
        string json = PlayerPrefs.GetString("Inventory", "");
        if (!string.IsNullOrEmpty(json))
        {
            string[] array = JsonHelper.FromJson<string>(json);
            if (array != null)
            {
                collectedItems = new List<string>(array);
                Debug.Log("Inventory loaded: " + json);
                if (inventoryLoadedCallback != null)
                {
                    inventoryLoadedCallback.Invoke(true);
                    inventoryLoadedCallback = null;
                }
                return;
            }
        }

        Debug.Log("Inventory is empty.");
        if (inventoryLoadedCallback != null)
        {
            inventoryLoadedCallback.Invoke(false);
            inventoryLoadedCallback = null;
        }
    }
    public void DeleteInventory()
    {
        PlayerPrefs.DeleteKey("Inventory");
        collectedItems.Clear();
    }
}
public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array, bool prettyPrint = false)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}
