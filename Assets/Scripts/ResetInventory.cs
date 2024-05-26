using UnityEngine;

public class InventoryResetter : MonoBehaviour
{
    void Start()
    {
        CollectedScript collectedScript = GameObject.FindWithTag("Player").GetComponent<CollectedScript>();
        if (collectedScript != null)
        {
            collectedScript.DeleteInventory();
        }

        Destroy(gameObject);
    }
}
