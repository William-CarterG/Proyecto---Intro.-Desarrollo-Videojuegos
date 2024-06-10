using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBarrierWires : MonoBehaviour
{
    void Start()
    {
        string completedPuzzle = PlayerPrefs.GetString("WiresPuzzleComplete", "");
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
