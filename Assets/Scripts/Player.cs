using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public Vector3 velocity = Vector3.zero;
    public Vector3 gravity;
    public int jumpSwitchCount = 0;
    public Vector3 jumpVelocity;
    public float maxSpeed = 5f;
    bool didJump = false;
    public float gtimer = 0;
    public float timeThresh = 0.6f;
    public int tapCount = 0;
    Rigidbody rb;
    string collidedTag = "";
    public Collider leftCollide;
    public Collider rightCollide;
    int jumpCounter = 0;
    int localScore;
    public GameObject shards;

    public SoundScript soundRef;
    public GameManager gameMangerRef;
    bool addLeftOnce;
    bool addRightOnce;
    bool dieOnce = false;
    public bool death;
    Vector3 deathTransform;
   // Vector3 viewPos = new Vector3();
    CameraScript myCamera;
    

    public GameObject leaf;

    int rotate = 0;
    //Quaternion turnPlayer = new Quaternion(1,0,0);
    // Use this for initialization


    public Animator anim;

    void Start () {
        //viewPos = new Vector3();
        rb = GetComponent<Rigidbody>();
        death = false;
        soundRef = GameObject.Find("Sound").GetComponent<SoundScript>();
        myCamera = Camera.main.GetComponent<CameraScript>();
        //gameMangerRef = GameObject.Find("GameManger").GetComponent<GameManager>();
        //faceReplace = GameObject.FindWithTag("CamDisplay").GetComponent<DeviceCamera>();
        anim = GetComponent<Animator>();
    }
	
	// Do graphics and input updates
	void Update () {

        controls();

        if(tapCount > 0)
        {
            //faceReplace.setImage();
            anim.SetBool("flying", true);
        }


       

        //viewPos = myCamera.WorldToViewportPoint(this.transform.position);
    }

    //Do physics engine updates 
    private void FixedUpdate()
    {
        gtimer += Time.deltaTime;
        if (gtimer > timeThresh)
        {
            velocity += 2.4f * gravity * Time.deltaTime;
        }
        else
        {
           velocity += 2 * gravity * Time.deltaTime;
        }

        if (didJump == true)
        {
            

            //if(velocity.y < 0)
            //{ 
                velocity = new Vector3(0, 0, 0);
            //gtimer = 0;
            //}

            if (jumpSwitchCount % 2 == 0)
            {
                velocity.y += jumpVelocity.y;
                velocity.x += jumpVelocity.x;
            }
            else
            {
                velocity.y += jumpVelocity.y;
                velocity.x += -jumpVelocity.x;
            }

            didJump = false;
        }

        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        transform.position += velocity * Time.deltaTime;

        //float angle = 0;
        //if (velocity.y < 0)
        //{
        //    angle = Mathf.Lerp(0, -90, -velocity.y / maxSpeed);
        //}
        // transform.rotation = Quaternion.Euler(0, 0, angle);

        if (leaf != null)
        {
            leaf.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -48);
        }
    }

    private void controls()
    {
            
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) && myCamera.transform.position.y > -30)
            {
                didJump = true;
                tapCount++;
                gtimer = 0;
                soundRef.PlayJumpSound();
                anim.Play("fly", - 1, 0f);
            }
    }

    void OnCollisionEnter(Collision col)
    {
        //this.gameObject.transform.position.x > 20
       // if (col.gameObject.tag == "RBuilding")
        //{
            Debug.Log("HIT! Collider: " + col.collider.gameObject.tag);
            jumpCounter++;

            if (col.gameObject.tag == "RBuilding")
            {

                RightWallHit();
                soundRef.PlayWallHit();

            

            }

            if (col.gameObject.tag == "LBuilding")
            {

            //transform.Rotate(new Vector3(0, 180, 0));
                velocity.x = 0;
                velocity.x += jumpVelocity.x;
                velocity.y += jumpVelocity.y / 10;
                transform.position += velocity * Time.deltaTime;

            

                if (jumpSwitchCount % 2 == 1)
                    {
                        transform.Rotate(new Vector3(0, 180, 0));
                        jumpSwitchCount++;
                    }

                if (!addLeftOnce)
                {
                    gameMangerRef.increaseScore();
                    addLeftOnce = true;
                    addRightOnce = false;
                }

                soundRef.PlayWallHit();
            //}
            }
        
            if(col.gameObject.tag == "hazardL" || col.gameObject.tag == "hazardR" || col.gameObject.tag == "hazardStay")
            {
            
                playerDeath();
            }


        if(col.gameObject.tag == "food")
        {
            if (col.gameObject.GetComponent<FoodScript>().isActive())
            {
                col.gameObject.GetComponent<FoodScript>().collected();
                gameMangerRef.breadScore();
                soundRef.PlayBreadEat();
            }

            

        }


            if(tapCount > 0)
            {
                if(col.gameObject.tag == "Floor")
                {
                    playerDeath();
                }
            }
    }

    


    public void RightWallHit()
    {
        velocity.x = 0;
        velocity.x += -jumpVelocity.x;
        velocity.y += jumpVelocity.y / 10f;
        transform.position += velocity * Time.deltaTime;

        if (jumpSwitchCount % 2 == 0)
        {
            transform.Rotate(new Vector3(0, 180, 0));
            jumpSwitchCount++;

        }

        if (!addRightOnce)
        {
            gameMangerRef.increaseScore();
            addRightOnce = true;
            addLeftOnce = false;
        }
    }

    public int getTapCount()
    {
        return tapCount;
    }

    public float getVelocity_Y()
    {
        return velocity.y;
    }

    public Vector3 getVelocity()
    {
        return velocity;
    }

    public float getPosition_Y()
    {
        return this.transform.position.y;
    }

    
    //public Vector3 getPlayerViewPos()
    //{
    //    return viewPos;
    //}

    public void playerDeath()
    {
        if (!dieOnce)
        {
            //transform.Rotate(new Vector3(0, 180, 0));
            deathTransform = transform.rotation.eulerAngles;
            this.gameObject.SetActive(false);
            GameObject deathChar = Instantiate(shards, this.transform.position, Quaternion.identity);
            deathChar.transform.Rotate(deathTransform);
            //deathChar.GetComponent<ShardScript>().addHorizontalForce();
            death = true;
            gameMangerRef.setDeath(death);
            
            soundRef.PlayGrunt();
            //soundRef.PlayDeathSound();
            myCamera.setDeath(death);
            //faceReplace.onDeath();
            dieOnce = true;
        }
    }





}
