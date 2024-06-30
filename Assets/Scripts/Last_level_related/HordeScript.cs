using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HordeScript : MonoBehaviour
{
    public float minusSpeed = 4f;
    public float speed = 0f;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(!(transform.position.y < -65))
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
        }
        else
        {
            if(speed > 0)
            {
                transform.position += Vector3.up * speed * Time.deltaTime;
            }
        }
    }

    public void setSpeed(float newTrainSpeed)
    {
        speed = minusSpeed - newTrainSpeed;
        Debug.Log("Set new speed:");
        Debug.Log(speed);
    }
}
