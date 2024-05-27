using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadPlayerState : MonoBehaviour
{
    private CollectedScript Inventory;
    // Start is called before the first frame update
    void Start()
    {
        Inventory = GetComponent<CollectedScript>();
    }

    public void SaveAll(bool passCheckpoint)
    {
        Inventory.SaveInventory(passCheckpoint);
    }
}
