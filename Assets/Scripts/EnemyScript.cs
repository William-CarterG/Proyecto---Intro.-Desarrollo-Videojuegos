using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float Velocity = 5f;
    public bool horizontal = false;
    public Rigidbody2D EnemyBody;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (horizontal)
        {
            transform.position += Vector3.left * Velocity * Time.deltaTime;
        }
        else
        {
            transform.position += Vector3.down * Velocity * Time.deltaTime;
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Velocity = 0 - Velocity;
    }
 }