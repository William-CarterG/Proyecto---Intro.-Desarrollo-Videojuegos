using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastLevelUpdaterScript : MonoBehaviour
{
    public GameObject player;
    private bool first = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (first)
        {
            first = false;
            PowerUpsScript pu = player.GetComponent<PowerUpsScript>();
            List<string> emptyPu = new List<string>();
            pu.SetPowerUps(emptyPu);
        }
    }
}
