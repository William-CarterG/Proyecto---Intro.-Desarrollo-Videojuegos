using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathManager : MonoBehaviour
{
    private SubtitleManager ZoneScript;
    private CollectedScript Inventory;

    void Start()
    {
        ZoneScript = GetComponent<SubtitleManager>();
        GameObject PlayerObject = GameObject.Find("Player");
        Inventory = PlayerObject.GetComponent<CollectedScript>();  
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (ZoneScript.playerInside)
            {
                if (Inventory.HasAllCoins())
                {
                    Debug.Log("Te doy la pieza de puzzle");
                }
                else
                {
                    ZoneScript.ShowTemporaryMessage("La muerte: No tienes 3 monedas", 3f);
                }
            }
        }
    }
}
