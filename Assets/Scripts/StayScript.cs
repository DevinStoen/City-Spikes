using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class StayScript : MonoBehaviour {


    public int playCount = 0;
    CameraScript myCam;
    bool addOnce = false;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (GameObject.FindGameObjectsWithTag(gameObject.tag).Length > 1)
        {
            Destroy(gameObject);
        }

        myCam = GameObject.Find("Main Camera").GetComponent<CameraScript>();
        
    }


	// Use this for initialization
	void Start () {
        // Camera.main.GetComponent<CameraScript>().setRetryCount(playCount);
        Advertisement.Initialize("1646894", false);
    }
	
	// Update is called once per frame
	void Update () {
        //if (!addOnce)
        //{
            
            //addOnce = true;
        //}
    }

    public void incrementPlayCount()
    {
        playCount++;
        if (playCount % 2 == 1)
        {
            if (Advertisement.IsReady())
            {
                Advertisement.Show();
            }
        }
    }

    public void sendRetryCount()
    {
        myCam.setRetryCount(playCount);
        //Debug.Log("sending " + playCount);
    }

    public int getPlayCount()
    {
        return playCount;
    }


}
