using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverFunctionScript : MonoBehaviour
{
    public GameObject player;
    public string playerTag = "Player"; 
    public KeyCode interactKey = KeyCode.E;
    public GameObject speedManager; 
    public List<float> speeds = new List<float>();
    public int currentSpeedIdx = 2;
    public GameObject ui;

    private GameObject Triangle1, Triangle2, Triangle3;
    private bool isPlayerInZone = false;
    private TrainSpeedScript trainSpeedScript;

    void Start()
    {
        Triangle1 = ui.transform.Find("Image_1").gameObject;
        Triangle2 = ui.transform.Find("Image_2").gameObject;
        Triangle3 = ui.transform.Find("Image_3").gameObject;
        setSpeedUI();
        speeds.Add(2.5f);
        speeds.Add(4.0f);
        speeds.Add(6.0f);
        if (speedManager != null)
        {
            trainSpeedScript = speedManager.GetComponent<TrainSpeedScript>();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player || other.CompareTag(playerTag))
        {
            isPlayerInZone = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == player || other.CompareTag(playerTag))
        {
            isPlayerInZone = false;
        }
    }

    void Update()
    {
        if (isPlayerInZone && Input.GetKeyDown(interactKey) && trainSpeedScript != null)
        {
            currentSpeedIdx = (currentSpeedIdx + 1) % 3;
            trainSpeedScript.UpdateSpeed(speeds[currentSpeedIdx]);
            setSpeedUI();
        }
    }

    void setSpeedUI()
    {
        Triangle2.SetActive(false);
        Triangle3.SetActive(false);

        if(currentSpeedIdx > 0)
        {
            Triangle2.SetActive(true);
        }
        if (currentSpeedIdx > 1)
        {
            Triangle3.SetActive(true);
        }
    }
}
