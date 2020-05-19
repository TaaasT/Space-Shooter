using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;
    [SerializeField]// 0 = triple Shot, 1 = Speedboost , 2 = Shield
    private int powerupID;
    [SerializeField]
    private AudioClip _clip;

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

            AudioSource.PlayClipAtPoint(_clip, transform.position);

            if(player != null)
            {
                switch(powerupID)
                {
                    case 0:
                        player.TrippleShotActive();
                    break;
                case 1:
                        player.SpeedBoostActive();
                    break;
                case 2:
                        player.ShieldActive();
                    break;
                }
            }

            Destroy(this.gameObject);
        }
    }

}
