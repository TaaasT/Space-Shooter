using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _speed = 4f;

    void Start()
    {
        
    }

    
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if(transform.position.y < -5.35f)
        {
            transform.position = new Vector3(Random.Range(-11.62f, 11.65f), 6.92f, 0);
        }
    }
}
