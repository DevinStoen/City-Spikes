using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShardScript : MonoBehaviour {

    public float thrust;
    public float h_thrust;
    public Rigidbody rb;

    void Start()
    {
        thrust = Random.Range(46, 64);
        h_thrust = Random.Range(30, 60);
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rb.AddForce(Vector3.down * thrust);


      
        //if (this.transform.rotation.y == 180)
        //{
        //    rb.AddForce(Vector3.right* h_thrust);
        //}
        //else
        //{
        //    rb.AddForce(Vector3.left * h_thrust);
        //}

    }

   

}
