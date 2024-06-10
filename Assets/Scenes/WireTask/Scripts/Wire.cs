using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour
{
    public SpriteRenderer wireEnd;
    public GameObject lightOn;
    Vector3 startPoint;
    Vector3 startPosition;
    // Start is called before the first frame update
    void Start()
    {
        startPoint = transform.parent.position;
        startPosition = transform.position;
    }

    // Update is called once per frame
    private void OnMouseDrag()
    {
        // mouse position to world point
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0;

        //check for nearby connection points
        Collider2D[] colliders = Physics2D.OverlapCircleAll(newPosition, .2f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject != gameObject)
            {
                UpdateWire(collider.transform.position);

                //check if the wires are same color
                if (transform.parent.name.Equals(collider.transform.parent.name))
                {
                    //count connections
                    MainWires.Instance.SwitchChange(1);


                    //finish step
                    collider.GetComponent<Wire>()?.Done();
                    Done();
                }

                return;
            }
        }

        //update wire
        //update position
        transform.position = newPosition;

        //update direction
        Vector3 direction = newPosition - startPoint;
        transform.right = direction * transform.lossyScale.x;

        //update scale
        float dist = Vector2.Distance(startPoint, newPosition);
        wireEnd.size = new Vector2(dist, wireEnd.size.y);
    }

    void Done()
    {
        lightOn.SetActive(true);
        
        //destroy the script
        Destroy(this);
    }

    private void OnMouseUp()
    {
       UpdateWire(startPosition);
    }

    void UpdateWire(Vector3 newPosition)
    {
        //update wire
        //update position
        transform.position = newPosition;

        //update direction
        Vector3 direction = newPosition - startPoint;
        transform.right = direction * transform.lossyScale.x;

        //update scale
        float dist = Vector2.Distance(startPoint, newPosition);
        wireEnd.size = new Vector2(dist, wireEnd.size.y);
    }
}
