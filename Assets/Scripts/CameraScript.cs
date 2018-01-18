using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public float cameraSpeed = 1f;

    Transform player;
    public GameObject player_go;
    Player playerScript;

    public float yPos;
    public float in_Y;

    public float maxSpeed = 5f;

    //Camera camera;
    public Vector3 camVelocity = Vector3.zero;
    //public GameObject myCamera;
    Vector3 playerviewPos;
    float timer = 0;

    public bool retry;

    float camVelocityNorm = 0;

    private Vector3 OriginalPos;
    Vector3 startPos;
    public bool Shaking;
    private float ShakeDecay;
    public float ShakeIntensity;

    public bool death;
    public float time;
    public bool doOnce;

    public int playCount;

    StayScript stayRef;

    Vector3 offset;

    //bool callOnce = false;

    public float smoothTime = 0.7F;
    private float yVelocity = 0.0F;

    public GameManager managerRef;

    // Use this for initialization
    void Start () {

        time = 0;


        player_go = GameObject.FindGameObjectWithTag("Player");
        //camera = this.GetComponent<Camera>();
        if (player_go == null)
        {
            Debug.Log("could not find player");
            return;
        }
        //player = player_go.transform;
        playerScript = player_go.GetComponent<Player>();

        retry = false;
        death = false;

        stayRef = GameObject.Find("StayObj").GetComponent<StayScript>();

        if(stayRef.getPlayCount()  < 1)
        {
            transform.position = new Vector3(this.transform.position.x, 13.7f, this.transform.position.z);
        }


        Shaking = false;
        doOnce = false;
        offset = transform.position - player_go.transform.position;

    }
	
	// Update is called once per frame
	void Update () {


 
        playerviewPos = Camera.main.WorldToViewportPoint(player_go.transform.position);
        yPos = this.transform.position.y;
        in_Y = player_go.transform.position.y;

        if (playerScript.getTapCount() > 0 && !death)
        {
            if(managerRef.getScore() < 20)
            {
                transform.Translate(10f * Vector3.up * Time.deltaTime, Space.World);
            }
            else if(managerRef.getScore() < 40)
            {
                transform.Translate(12f * Vector3.up * Time.deltaTime, Space.World);
            }
            else if (managerRef.getScore() < 60)
            {
                transform.Translate(16f * Vector3.up * Time.deltaTime, Space.World);
            }
            else if(managerRef.getScore() < 80)
            {
                transform.Translate(18f * Vector3.up * Time.deltaTime, Space.World);
            }
            else if (managerRef.getScore() >= 80)
            {
                transform.Translate(20f * Vector3.up * Time.deltaTime, Space.World);
            }



            //setYVelocity(6);
        }





        if (ShakeIntensity > 0 && time <= .2)
        {
            transform.position = new Vector3(OriginalPos.x + UnityEngine.Random.insideUnitSphere.x * ShakeIntensity, OriginalPos.y, OriginalPos.z);
            ShakeIntensity -= ShakeDecay;
        }

        if(time > .2)
        {
            transform.position = new Vector3(19, OriginalPos.y, OriginalPos.z);
        }
      
        //if (playerScript.getVelocity_Y() > 0)
        //{
        //    if (playerScript.getPosition_Y() < (Screen.height / 2))
        //    {
        //        transform.transform.position = new Vector3(19, playerScript.transform.position.y + 10, -90);
        //    }
        //    transform.transform.position = new Vector3(19, playerScript.transform.position.y + 10, -90);
        //}





        if (this.transform.position.y < 13.7f || retry == true)
        {
            transform.Translate(50 * Vector3.up * Time.deltaTime, Space.World);
           
        }
        
        //if the player leaves the screen he dies.
        if (playerviewPos.y < 0)
        {
            playerScript.playerDeath();
        }



        if (!death)
        {
            if (playerScript.getTapCount() > 0)
            {
                if (playerviewPos.y >= 0.45f)
                {

                    transform.position = new Vector3(19, Mathf.Lerp(transform.position.y, player_go.transform.position.y, Time.deltaTime), -90);

                    //transform.position = Vector3.Lerp(transform.position, player_go.transform.position, 2 * Time.deltaTime);

                    //setYVelocity(playerScript.getVelocity_Y());
                    //transform.position = new Vector3(19,player_go.transform.position.y + offset.y, -90);
                    //transform.position = new Vector3(this.transform.position.x, Mathf.Clamp(this.transform.position.y, player_go.transform.position.y + 5, -90));
                }
                else
                {
                    //transform.Translate(8f * Vector3.up * Time.deltaTime, Space.World);
                    //setYVelocity(8);
                }

                //else
                //{
                //    if (playerScript.getTapCount() > 0)
                //    {
                //        //camVelocity.y = playerScript.getVelocity_Y() - Time.deltaTime * 2;
                //        setYVelocity(6);
                //    }
                //}
            }
        }
        else
        {
            //if (!doOnce)
            //{
                setYVelocity(0);
                DoShake();
               // doOnce = true;
            //}
            time += Time.deltaTime;
        }
        

        //Debug.Log(playerviewPos.y);

        camVelocity = Vector3.ClampMagnitude(camVelocity, 100);
        transform.position += camVelocity * Time.deltaTime;


    }

    internal Vector3 WorldToViewportPoint(Vector3 position)
    {
        throw new NotImplementedException();
    }

    public void setYVelocity(float in_V)
    {
        //transform.position += camVelocity * Time.deltaTime;
        camVelocity.y = in_V;
        //camVelocity = Vector3.Lerp(this.transform.position, new Vector3(19,in_V.y,-90), timer);
        

       // camVelocity = Vector3.ClampMagnitude(camVelocity, 100);
        //transform.position += camVelocity * Time.deltaTime;

        //transform.position = Vector3.Lerp(this.transform.position, new Vector3(19, in_V.y, -90), timer);
        //timer -= Time.deltaTime;


        //if(in_Y > yPos)
        //{
        //    while (this.gameObject.transform.position.y < in_Y)
        //    {
        //        transform.transform.position = new Vector3(this.gameObject.transform.position.x, 1 * Time.deltaTime, -90);
        //    }

        //}
        //yPos = in_V;



    }

    public void setRetry(bool inBool)
    {
        retry = inBool;
    }

    public void DoShake()
    {

        //Debug.Log("shake()");
        OriginalPos = transform.position;
        ShakeIntensity = .3f;
        ShakeDecay = .01f;
        Shaking = true;
    }

    public void setDeath(bool inBool)
    {
        death = inBool;
    }

    public void setRetryCount(int inCount)
    {
        
        playCount = inCount;
    }





   


}
