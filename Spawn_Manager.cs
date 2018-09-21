using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Manager : MonoBehaviour {

    [SerializeField]
    private GameObject _enemyShipPrefab;
    [SerializeField]
    private GameObject[] _powerups;
    [SerializeField]
    private GameObject _playerPrefab;
    
    private GameManager _gameManager;

    // Use this for initialization
    void Start () {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
     
        }


	



    public void StartSpawning()
    {
        StartCoroutine(EnemySpawnRoutine());
        StartCoroutine(PowerupSpawnRoutine());
    }


    public IEnumerator EnemySpawnRoutine()
    {

    while (_gameManager.gameOver == false) {
            
            
            Instantiate(_enemyShipPrefab, new Vector3(Random.Range(-7f, 7f), 7, 0), Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
        }
        
    }


    public IEnumerator PowerupSpawnRoutine()
    {
        
        while (_gameManager.gameOver == false)
        {

            int randomPowerup = Random.Range(0, 3);
            Instantiate(_powerups[randomPowerup], new Vector3(Random.Range(-7f, 7f), 7, 0), Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
        }

    }


}
