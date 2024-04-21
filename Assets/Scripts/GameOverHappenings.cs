using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverHappenings : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.DeleteKey("Inventory");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
