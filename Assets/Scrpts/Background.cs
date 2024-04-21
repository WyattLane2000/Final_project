using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    private float speed = 16f;
    private Vector3 startPos;
    
    private void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        Vector3 movement = Vector3.up * speed * Time.deltaTime;
        transform.Translate(movement);
        
        if (transform.position.y > 12 )
        {
            transform.position += new Vector3(0, -22, 0);
        }
    }
}
