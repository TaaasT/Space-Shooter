using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5;
    private float _speedMultiplier = 2;
    [SerializeField]
    private Laser _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private float _fireRate = 0.15f;
    private float _canFire = -1f;
    Vector3 offset = new Vector3(0, 1.05f, 0);
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;
    
    private bool _isTripleShotActive = false;
    private bool _isSpeedBoostActive = false;
    private bool _isShieldActive = false;
    public bool isPlayerOne = false;
    public bool isPlayerTwo = false;

    [SerializeField]
    private GameObject _shieldVisualizer;

    [SerializeField]
    private int _score;

    private UIManager _uiManager;
    private GameManager _gameManager;

    [SerializeField]
    private GameObject _leftEngine, _rightEngine;
    [SerializeField]
    private AudioClip _laserSoundClip;
    private AudioSource _audioSource;

    void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _audioSource = GetComponent<AudioSource>();

        _shieldVisualizer.SetActive(false);
        _leftEngine.SetActive(false);
        _rightEngine.SetActive(false);

        if (_gameManager.isCoopMode == false)
        {
            transform.position = new Vector3(0, 0, 0);
        }

        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn manager is NULL.");
        }

        if(_uiManager == null)
        {
            Debug.LogError("The UI Manager is NULL");
        }

        if(_audioSource == null)
        {
            Debug.LogError("AudioSource on the player is NULL");
        }
        else
        {
            _audioSource.clip = _laserSoundClip;
        }

    }

    void Update()
    {

        if(isPlayerOne == true)
        {
            CalculateMovement();
            FireLaser();
        }
        
        if(isPlayerTwo == true)
        {
            PlayerTwo();
            ShootPlayerTwo();
        }

    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        transform.Translate(direction * _speed * Time.deltaTime);
       
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 3.8f), 0);

        if (transform.position.x > 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x < -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }

    void PlayerTwo()
    {


        if(Input.GetKey(KeyCode.Keypad8))
        {
            transform.Translate(Vector3.up * _speed * Time.deltaTime);
        }

        if(Input.GetKey(KeyCode.Keypad6))
        {
            transform.Translate(Vector3.right * _speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.Keypad2))
        {
            transform.Translate(Vector3.down * _speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.Keypad4))
        {
            transform.Translate(Vector3.left * _speed * Time.deltaTime);
        }

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 3.8f), 0);

        if (transform.position.x > 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x < -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }

    void FireLaser()
    {
        if ((Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire) && isPlayerOne == true)
        {
            _canFire = Time.time + _fireRate;

            if (_isTripleShotActive == true)
            {

                GameObject tripleShot = Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
                foreach (Laser laser in tripleShot.GetComponentsInChildren<Laser>()) 
                {
                    laser.SetPlayer(this);
                }
            }
            else
            {
                Instantiate(_laserPrefab, transform.position + offset, Quaternion.identity).SetPlayer(this);
            }

            _audioSource.Play();

        }
    }

    void ShootPlayerTwo()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            _canFire = Time.time + _fireRate;

            if (_isTripleShotActive == true)
            {

                GameObject tripleShot = Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
                foreach (Laser laser in tripleShot.GetComponentsInChildren<Laser>())
                {
                    laser.SetPlayer(this);
                }
            }
            else
            {
                Instantiate(_laserPrefab, transform.position + offset, Quaternion.identity).SetPlayer(this);
            }

            _audioSource.Play();

        }
    }
        public void Damage()
    {
        if(_isShieldActive == true)
        {
            _shieldVisualizer.SetActive(false);
            _isShieldActive = false;
            return;
        }

        _lives --;

        if(_lives == 2)
        {
            _leftEngine.SetActive(true);
        }
        else if(_lives == 1)
        {
            _rightEngine.SetActive(true);
        }

        _uiManager.UpdateLives(_lives);

        if(_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    public void TrippleShotActive()
    {
        _isTripleShotActive = true;

        StartCoroutine(TrippleShotPowerDownRoutine());
    }

    IEnumerator TrippleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5);
        _isTripleShotActive = false;
    }

    public void SpeedBoostActive()
    {
        _isSpeedBoostActive = true;
        _speed *= _speedMultiplier;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5);
        _isSpeedBoostActive = false;
        _speed /= _speedMultiplier;
    }

    public void ShieldActive()
    {
        _isShieldActive = true;
        _shieldVisualizer.SetActive(true);  
    }


    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }

}
