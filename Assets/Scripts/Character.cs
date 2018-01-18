using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {


    public Rigidbody rb;
    public float thrust = 100;
    public float thrustUp = 200;

    public float bounce = 8;
    public bool jumpSwitch = false;
    float maxSpeed = 10;
    bool turnGravityOn = false;
    public float gtimer = 0;
    public float timeThresh = 0.1f;

    public Vector2 jumpForce = new Vector2(0, 800);
    public Vector2 rightForce = new Vector2(400, 0);
    public Vector2 leftForce = new Vector2(-400, 0);
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {

        controls();

        gtimer += Time.deltaTime;
        if(gtimer > timeThresh)
        {
            rb.AddForce(new Vector3(0, -10 * gtimer * 2, 0));
        }

    }

    private void controls()
    {
        if (!jumpSwitch)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //Debug.Log("left"); 
                rb.velocity = new Vector3(0, 0, 0);
                rb.velocity += thrustUp * Vector3.up;
                rb.velocity += thrust * Vector3.right;
                turnGravityOn = false;
                gtimer = 0;
                //rb.useGravity = false;
            }
            
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //Debug.Log("left"); 
                rb.velocity = new Vector3(0, 0, 0);
                //rb.AddForce(jumpForce);
                //rb.AddForce(leftForce);
                rb.velocity += thrustUp * Vector3.up;
                rb.velocity += thrust * Vector3.left;
                turnGravityOn = false;
                gtimer = 0;
                //rb.useGravity = false;
            }
            else
            {
                
            }
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Building")
        {
            if (!jumpSwitch)
            {
                rb.velocity += bounce * Vector3.up;
                rb.velocity += bounce * Vector3.left;

            }
            else
            {
                rb.velocity += bounce * Vector3.up;
                rb.velocity += bounce * Vector3.right;
            }
            jumpSwitch = !jumpSwitch;
        }
    }
}
