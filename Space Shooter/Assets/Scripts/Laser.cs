using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private int _speed = 8;

    void Start()
    {
        
    }

    
    void Update()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
    }
}
