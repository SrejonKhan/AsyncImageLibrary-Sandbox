using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public Vector3 rotateAngle = new Vector3(45, 45, 45);

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(-rotateAngle * Time.deltaTime);    
    }
}
