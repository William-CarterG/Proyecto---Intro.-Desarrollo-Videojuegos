using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverHappenings : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.DeleteKey("CheckpointItems");
        PlayerPrefs.DeleteKey("LevelInventory");
        PlayerPrefs.DeleteKey("CablePuzzleComplete");
        PlayerPrefs.DeleteKey("SwitchPuzzleComplete");
        PlayerPrefs.DeleteKey("WiresPuzzleComplete");
        PlayerPrefs.DeleteKey("PlayerPositionX");
        PlayerPrefs.DeleteKey("PlayerPositionY");
        PlayerPrefs.DeleteKey("PoemPuzzleComplete");
        PlayerPrefs.DeleteKey("MiniGameCompleted1");
        PlayerPrefs.DeleteKey("PlayerHealthActual");
        PlayerPrefs.DeleteKey("PreviousScene");
        PlayerPrefs.DeleteKey("FirstTimetext");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
