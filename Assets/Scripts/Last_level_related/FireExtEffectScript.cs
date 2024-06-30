using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExtEffectScript : MonoBehaviour
{
    public GameObject effectPrefab;
    public string playerName = "Player";
    public float circleRadius = 1f;
    private Transform player;
    private Vector2 directionOffset;

    void Start()
    {
        player = GameObject.Find(playerName)?.transform;

        if (player == null)
        {
            Debug.LogError("Player GameObject not found!");
        }
    }

    void Update()
    {
        if (player == null)
            return;

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector2 inputVector = new Vector2(horizontalInput, verticalInput);

        if (inputVector.magnitude > 0)
        {
            directionOffset = inputVector.normalized;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector2 spawnPosition = (Vector2)player.position + directionOffset * circleRadius;
            SpawnEffect(spawnPosition);
        }
    }

    void SpawnEffect(Vector2 position)
    {
        Instantiate(effectPrefab, position, Quaternion.identity);
    }
}