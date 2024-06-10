using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBarrierSwitch : MonoBehaviour
{
    void Start()
    {
        string completedPuzzle = PlayerPrefs.GetString("SwitchPuzzleComplete", "");
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
