using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spiner : MonoBehaviour
{
    public float rotX;
    public float rotY;
    public float rotZ;
    public float rotSpeedX;
    public float rotSpeedY;
    public float rotSpeedZ;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(rotX, rotY, rotZ);
        transform.Rotate(rotSpeedX, rotSpeedY,rotSpeedZ);
    }
}
