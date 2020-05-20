using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;
    [SerializeField]
    private GameObject _laserPrefab;

    private Player _player;
    private Animator _anim;
    private BoxCollider2D _enemyCollider;
    private AudioSource _audioSource;
    private float _fireRate = 3f;
    private float _canFire = -1;

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _audioSource = GetComponent<AudioSource>();

        if(_player == null)
        {
            Debug.LogError("Player is NULL!");
        }

        _anim = GetComponent<Animator>();

        if(_anim == null)
        {
            Debug.LogError("Animator is NULL");
        }

        _enemyCollider = GetComponent<BoxCollider2D>();
    }

    
    void Update()
    {
        CalculateMovement();
        
        if(Time.time > _canFire)
        {
            _fireRate = Random.Range(3f, 7f);
            _canFire = Time.time + _fireRate;
            GameObject enemyLaser = Instantiate(_laserPrefab, transform.position, Quaternion.identity);
            Laser[] lasers = enemyLaser.GetComponentInChildren<Laser[]>();
            
            for(int i = 0; i < lasers.Length; i++)
            {
                lasers[i].AssignEnemyLaser();
            }

        }
    }

    void CalculateMovement()
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
            _speed = 0;
            _audioSource.Play();
            Destroy(this.gameObject, 2.8f);
            _enemyCollider.enabled = false;
            
        }

        else if(other.gameObject.tag == "Laser")
        {
            Destroy(other.gameObject);
            
            if(_player != null)
            {
                _player.AddScore(10);
            }

           _anim.SetTrigger("OnEnemyDeath");
           _speed = 0;
            _audioSource.Play();
            Destroy(this.gameObject, 2.8f);
            _enemyCollider.enabled = false;
        }
    }

}
