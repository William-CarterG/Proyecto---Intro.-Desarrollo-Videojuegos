using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireScript : MonoBehaviour
{
    public float health = 100;
    private FireManagerScript Manager;
    public string fireManagerName = "FireManager";
    public string playerName = "Player";
    public float circleRadius = 1f;
    private Vector2 directionOffset;

    private Transform player;
    private GameObject fireManager;

    void Start()
    {
        // Find the Player and FireManager GameObjects by name
        player = GameObject.Find(playerName)?.transform;
        fireManager = GameObject.Find(fireManagerName);

        // Check if the references are found
        if (player == null)
        {
            Debug.LogError("Player GameObject not found!");
        }

        if (fireManager == null)
        {
            Debug.LogError("FireManager GameObject not found!");
        }
        else
        {
            Manager = fireManager.GetComponent<FireManagerScript>();
        }
    }

    void Update()
    {
        if (player == null)
            return;

        // Handle player input for direction
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector2 inputVector = new Vector2(horizontalInput, verticalInput);

        if (inputVector.magnitude > 0)
        {
            directionOffset = inputVector.normalized;
        }

        Vector2 circleCenter = (Vector2)player.position + directionOffset;

        // Check for space key press
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (IsWithinCircle(transform.position, circleCenter, circleRadius))
            {
                MakeDamage();
            }
        }
    }

    bool IsWithinCircle(Vector2 objectPosition, Vector2 circleCenter, float radius)
    {
        float distance = Vector2.Distance(objectPosition, circleCenter);
        return distance <= radius;
    }

    void MakeDamage()
    {
        health -= 20;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Manager != null)
        {
            Manager.FireDestroyed();
        }
    }
}
