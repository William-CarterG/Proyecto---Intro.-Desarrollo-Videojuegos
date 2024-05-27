using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleScript : MonoBehaviour
{
    public string Name;

    void Start()
    {
        CheckIfCollected();
    }

    public void collected()
    {
        Destroy(gameObject);
    }

    void CheckIfCollected()
    {
        CollectedScript collectedScript = GameObject.FindWithTag("Player").GetComponent<CollectedScript>();

        if (collectedScript != null)
        {
            if (collectedScript.IsItemCollected(Name))
            {
                Destroy(gameObject);
            };
        }
        else
        {
            Debug.Log("CollectedScript not found.");
        }
    }
}
