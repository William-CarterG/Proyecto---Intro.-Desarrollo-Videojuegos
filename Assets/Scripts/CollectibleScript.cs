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
            collectedScript.IsItemCollected(Name, (isCollected) =>
            {
                if (isCollected)
                {
                    Destroy(gameObject);
                }
                else
                {
                    Debug.Log("Item " + Name + " is not collected.");
                }
            });
        }
        else
        {
            Debug.Log("CollectedScript not found.");
        }
    }
}
