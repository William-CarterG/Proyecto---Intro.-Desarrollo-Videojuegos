using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBarrier : MonoBehaviour
{
    void Start()
    {
        string completedPuzzle = PlayerPrefs.GetString("CablePuzzleComplete", "");
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
