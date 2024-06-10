using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBarrierPoem : MonoBehaviour
{
    void Start()
    {
        string completedPuzzle = PlayerPrefs.GetString("PoemPuzzleComplete", "");
        if (!string.IsNullOrEmpty(completedPuzzle))
        {
            if (completedPuzzle == "true")
            {
                Destroy(gameObject);
            }
        }
    }

    void Update()
    {
        
    }
}
