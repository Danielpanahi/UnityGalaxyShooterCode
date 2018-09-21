using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

    [SerializeField]
    private float _speed = 10.0f;




    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


        Move();
        DestroyCheck();
	}

    private void Move()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime); 
    }

    

    private void DestroyCheck()
    {
        if(transform.position.y >= 6)
        {
            Destroy(this.gameObject);
        }
    }
}
