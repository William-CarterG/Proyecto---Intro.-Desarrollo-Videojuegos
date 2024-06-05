using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balaVertical : MonoBehaviour
{
    // Start is called before the first frame update
    public float Velocity = 5f;
    public Rigidbody2D EnemyBody;
    private PowerUpsScript PUScript;

    // Start is called before the first frame update
    void Start()
    {
        PUScript = GameObject.FindWithTag("Player").GetComponent<PowerUpsScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PUScript.checkWatch())
        {


            transform.position += Vector3.up * Velocity * Time.deltaTime;

        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Walls"))
        {
            Destroy(gameObject);
        }
    }
}
