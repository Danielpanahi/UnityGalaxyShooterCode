using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour {


    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private int powerupID; //0 = triple shot, 1 = 
    [SerializeField]
    private AudioClip _clip;

    // Update is called once per frame
    void Update () {
        CheckBorder();
        Move();
        
	}

    private void CheckBorder()
    {
        if (transform.position.y < -6.5f)
        {
            Destroy(this.gameObject);
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

                if (powerupID == 0)
                {
                    player.TripleShotPowerupOn();
                }
                else if (powerupID == 1)
                {
                    player.SpeedBoostPowerUpOn();
                }
                else if (powerupID == 2)
                {
                    player.ShieldPowerUpOn();
                }
            }

            Destroy(this.gameObject);
        }
        Debug.Log("Collided with :" + other.name);
    }


    private void Move()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
    }
}
