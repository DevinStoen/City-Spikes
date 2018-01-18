using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Death : MonoBehaviour
{

    public Vector3 velocity = Vector3.zero;
    public Vector3 gravity;
    public float maxSpeed = 100f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        velocity += 2.4f * gravity * Time.deltaTime;

        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        transform.position += velocity * Time.deltaTime;

        transform.Rotate(Vector3.right * Time.deltaTime);

    }

    void OnCollisionEnter(Collision col)
    {

        if(col.gameObject.tag == "Floor")
        {
            Destroy(this.gameObject);
        }


    }
}
