using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastLevelUpdaterScript : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        PowerUpsScript pu = player.GetComponent<PowerUpsScript>();
        List<string> emptyPu = new List<string>();
        pu.SetPowerUps(emptyPu);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
