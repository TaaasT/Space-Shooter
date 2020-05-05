using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;

    void Start()
    {
        
    }

    
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        float randomX = Random.Range(-11.62f, 11.65f);
        if (transform.position.y < -5.35f)
        {
            transform.position = new Vector3(randomX, 6.92f, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }
        else if(other.gameObject.tag == "Laser")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }

}
