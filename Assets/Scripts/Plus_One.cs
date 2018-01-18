using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Plus_One : MonoBehaviour {


    public Transform scoreLoc;
    public Transform player;
    public bool start;
    public float time;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


        //moveTowardScore();
        //if (start)
        //{
        //    
        //    //start = false;
        //}


    }


    public void moveTowardScore()
    {
        while(time < 5)
        {

            transform.position = new Vector3(Mathf.Lerp(player.transform.position.x, 19, Time.deltaTime),
                                            Mathf.Lerp(player.transform.position.y, player.transform.position.y + 20, Time.deltaTime), -10f);
            //Debug.Log("hereeeee");


            time += Time.deltaTime;
        }
        
    }

    public void setStartMove(bool inStart)
    {
        start = inStart;
    }



}
