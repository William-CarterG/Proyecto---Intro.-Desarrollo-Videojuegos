using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateScript : MonoBehaviour
{
    public Vector3 rotationAngles = new Vector3(0, 0, 90);

    void Start()
    {
        transform.Rotate(rotationAngles);
    }
}