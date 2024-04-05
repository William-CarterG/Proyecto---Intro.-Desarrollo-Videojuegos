using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    private bool IsRunning = false;

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        GetComponent<Rigidbody2D>().velocity = movement * speed;
        if (!IsRunning && Input.GetKey(KeyCode.Space))
        {
            IsRunning = true;
            speed = 15f;
        }
        else if (IsRunning && !Input.GetKey(KeyCode.Space))
        {

             IsRunning = false;
             speed = 5f;
        
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
    }
}