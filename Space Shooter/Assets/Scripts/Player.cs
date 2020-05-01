using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed = 3.5f;

    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
    }

    
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

}
