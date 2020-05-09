using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;
    [SerializeField]// 0 = triple Shot, 1 = Speedboost , 2 = Shield
    private int powerupID;


    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -6f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if(player != null)
            {
                if (powerupID == 0)
                {
                    player.TrippleShotActive();
                }
                else if(powerupID == 1)
                {
                    Debug.Log("Speed");
                }
                else if(powerupID == 2)
                {
                    Debug.Log("Shield");
                }
            }

            Destroy(this.gameObject);
        }
    }

}
