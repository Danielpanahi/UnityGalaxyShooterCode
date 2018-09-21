using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private GameObject _enemyExplosionPrefab;
    
    private UI_Manager _uI_Manager;
    [SerializeField]
    private AudioClip _clip;

    // Use this for initialization
    void Start () {
        _uI_Manager = GameObject.Find("Canvas").GetComponent<UI_Manager>();
       
    }
	
	// Update is called once per frame
	void Update () {

        //move down
        MoveDown();

        // when off screen, respawn at top with random x in bounds of the screen
        CheckBorder();

	}

    private void MoveDown()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed );
    }

    private void CheckBorder()
    {
        if (transform.position.y < -6.5f)
        {
          
            transform.position = new Vector3(Random.Range(-7.9f, 7.9f), 7f, 0);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1f);
                player.GetDamage(1);
                Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }

           
        }

        if (other.tag == "Laser")
        {
            Laser laser = other.GetComponent<Laser>();

            if (laser != null)
            {
               
                _uI_Manager.UpdateScore(10);
                AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1f);
                Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
        }

      
        Debug.Log("Collided with :" + other.name);
    }

}
