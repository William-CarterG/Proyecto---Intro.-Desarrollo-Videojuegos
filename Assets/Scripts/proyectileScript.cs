using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class proyectileScript : MonoBehaviour
{
    public float speed = 5.0f;
    public float stunAmount = 5.0f;
    private Vector3 direction = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       transform.position += direction * speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyScript script = collision.gameObject.GetComponent<EnemyScript>();
            if(script != null)
            {
                script.stun(stunAmount);
            }

        }
        Destroy(gameObject);
    }
    public void setDirection(Vector3 newDirection)
    {
        direction = newDirection;
    }
}
