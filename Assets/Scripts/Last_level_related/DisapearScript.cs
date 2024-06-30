using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisapearScript : MonoBehaviour
{
    public float time = 0.15f;
    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > time)
        {
            Destroy(gameObject);
        }
    }
}
