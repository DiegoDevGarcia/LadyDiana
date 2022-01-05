using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFlow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed;
    
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

   
    void FixedUpdate()
    {
        Vector3 StartPosition = new Vector3(target.position.x, target.position.y + 0.43f, -1f);
        Vector3 smoothPosition = Vector3.Lerp(transform.position, StartPosition, smoothSpeed);
        transform.position = smoothPosition;
    }
}
