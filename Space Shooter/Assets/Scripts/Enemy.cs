using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;

    private Player _player;
    private Animator _anim;

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();

        if(_player == null)
        {
            Debug.LogError("Player is NULL!");
        }
        _anim = GetComponent<Animator>();
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if(player != null)
            {
                player.Damage();
            }
            _anim.SetTrigger("OnEnemyDeath");
            Destroy(this.gameObject);
        }
        else if(other.gameObject.tag == "Laser")
        {
            Destroy(other.gameObject);
            
            if(_player != null)
            {
                _player.AddScore(10);
            }
            _anim.SetTrigger("OnEnemyDeath");
            Destroy(this.gameObject);
        }
    }

}
