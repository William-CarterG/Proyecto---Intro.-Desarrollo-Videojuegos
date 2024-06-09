using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathManager : MonoBehaviour
{
    private SubtitleManager ZoneScript;
    private CollectedScript Inventory;
    public GameObject PuzzlePiece;

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
                    Debug.Log("Dando piezza al usuario");
                    Inventory.AddItem("Photo piece (7)");
                    ZoneScript.ShowTemporaryMessage("La muerte: Te doy una pieza de puzzle, gracias mortal.", 3f);
                    Destroy(PuzzlePiece);
                }
                else
                {
                    ZoneScript.ShowTemporaryMessage("La muerte: No tienes 3 monedas", 3f);
                }
            }
        }
        if (Inventory.IsItemCollected("Photo piece (7)"))
        {
            Destroy(PuzzlePiece);
        }
    }
}
