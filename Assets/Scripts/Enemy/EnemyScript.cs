using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float Velocity = 5f;
    public bool horizontal = false;
    public Rigidbody2D EnemyBody;
    private PowerUpsScript PUScript;
    private float timer = 0.0f, stun_time;
    private bool isStunned = false;

    // Start is called before the first frame update
    void Start()
    {
        PUScript = GameObject.FindWithTag("Player").GetComponent<PowerUpsScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PUScript.checkWatch() && !isStunned)
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
        else if (isStunned)
        {
            timer += Time.deltaTime;

            if (timer > stun_time)
            {
                timer = 0;
                isStunned = false;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Velocity = 0 - Velocity;
    }

    public void stun(float stunTime)
    {
        isStunned = true;
        stun_time = stunTime;
        timer = 0;
    }
}
