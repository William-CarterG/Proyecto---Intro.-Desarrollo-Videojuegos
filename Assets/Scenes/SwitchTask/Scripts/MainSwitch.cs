using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSwitch : MonoBehaviour
{
    static public MainSwitch Instance;
    public int switchCount;
    public GameObject winText;
    private int onCount = 0;
    private void Awake()
    {
        Instance = this;
    }
    public void SwitchChange(int points) {
        onCount = onCount + points;
        if (onCount == switchCount) 
        {
            PlayerPrefs.SetString("SwitchPuzzleComplete", "true");
            winText.SetActive(true);
        }
    }
}
