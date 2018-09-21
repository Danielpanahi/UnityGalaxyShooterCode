using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public bool canTripleShot = false;
    public bool hasSpeedBoost = false;
    public bool alreadyHit = false;
    public bool hasShield = false;


    [SerializeField]
    private GameObject _shieldGameObject;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _playerExplosionPrefab;
    [SerializeField]
    private float _fireRate = 0.25f;
    private float _canFire = 0.0f;

    [SerializeField]
    private GameObject[] _engines;


    [SerializeField]
    private float _speed = 5.5f;

    [SerializeField]
    private int _health = 3;

    private UI_Manager _uI_Manager;
    private GameManager _gameManager;
    private Spawn_Manager _spawnManager;
    private AudioSource _audioSource;

    private int _hitCount = 0;

    // Use this for initialization
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);

        _uI_Manager = GameObject.Find("Canvas").GetComponent<UI_Manager>();
        if(_uI_Manager != null)
        {
            _uI_Manager.UpdateLives(_health);
        }

        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<Spawn_Manager>();
        if (_spawnManager != null)
        {
            _spawnManager.StartSpawning();
        }

        _audioSource = GetComponent<AudioSource>();

        _hitCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        IsAlive();
        Movement();

        //if space pressed
        //spawn laser at player position.

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }
    }

    private void Fire()
    {
        if (Time.time > _canFire)
        {
            _audioSource.Play(); 
            if (!canTripleShot)
            {
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.88f, 0), Quaternion.identity);
            }
            if (canTripleShot)
            {
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.88f, 0), Quaternion.identity);
                Instantiate(_laserPrefab, transform.position + new Vector3(0.54f, -0.04f, 0), Quaternion.identity);
                Instantiate(_laserPrefab, transform.position + new Vector3(-0.54f, -0.04f, 0), Quaternion.identity);
            
            }
            _canFire = Time.time + _fireRate;
        }

    }

    private void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        //if speedboost true // transform speed * 1.5
        if (hasSpeedBoost)
        {
            transform.Translate(Vector3.right * Time.deltaTime * _speed * 1.5f * horizontalInput);
            transform.Translate(Vector3.up * Time.deltaTime * _speed * 1.5f * verticalInput);
        }
        else
        {
            transform.Translate(Vector3.right * Time.deltaTime * _speed * horizontalInput);
            transform.Translate(Vector3.up * Time.deltaTime * _speed * verticalInput);
        }
        CheckBorderY();
        CheckBorderX();
    }


    private void CheckBorderY()
    {
        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y < -4.2f)
        {
            transform.position = new Vector3(transform.position.x, -4.2f, 0);
        }

    }

    private void CheckBorderX()
    {
        if (transform.position.x > 9.4f)
        {
            transform.position = new Vector3(-9.4f, transform.position.y, 0);
        }
        else if (transform.position.x < -9.4f)
        {
            transform.position = new Vector3(9.4f, transform.position.y, 0);
        }
    }

    public void TripleShotPowerupOn()
    {
        canTripleShot = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    public IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        canTripleShot = false;
    }

    public void ShieldPowerUpOn()
    {
        hasShield = true;
       _shieldGameObject.SetActive(true);
    }


    public void SpeedBoostPowerUpOn()
    {
        hasSpeedBoost = true;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    public IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        hasSpeedBoost = false;
    }

    public void GetDamage(int amount)
    {
        if (hasShield)
        {
            hasShield = false;
            _shieldGameObject.SetActive(false);
            return;
        }
            if (!alreadyHit)
            {

            _hitCount++;
            CheckEngineFailure();


                this._health -= amount;
                _uI_Manager.UpdateLives(_health);
                alreadyHit = true;
                StartCoroutine(HasBeenHitRoutine());
            }
        
        }

    private void CheckEngineFailure()
    {
        if (_hitCount == 1)
        {
            int r = Random.Range(0, 2);
            _engines[r].SetActive(true);
        }
        if (_hitCount == 2)
        {
            _engines[0].SetActive(true);
            _engines[1].SetActive(true);
        }
    }

    public IEnumerator HasBeenHitRoutine()
    {
        yield return new WaitForSeconds(1.0f);
        alreadyHit = false;
    }

    private void IsAlive()
    {
        if(_health < 1)
        {
            Instantiate(_playerExplosionPrefab, transform.position, Quaternion.identity);
            _gameManager.gameOver = true;
            _uI_Manager.ShowTitleScreen();
            Destroy(this.gameObject);
        }
    }


}
