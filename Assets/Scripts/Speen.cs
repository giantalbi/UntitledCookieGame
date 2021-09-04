using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speen : MonoBehaviour
{
    public int speed = 1;

    void Start()
    {
        
    }

    void Update()
    {
        transform.rotation = transform.rotation * Quaternion.AngleAxis(speed, Vector3.forward);
    }
}
