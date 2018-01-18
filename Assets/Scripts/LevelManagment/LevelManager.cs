using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//This is the top level script in charge of 
//all things having to do with the levels spawing
//in the game.
public class LevelManager : MonoBehaviour {

    public static LevelManager levelManager;

    //levels pooled in game
    GameObject obj;
    GameObject rem;
    bool loadOnce = false;

    float buildingHeight = 0;
    GameObject top;
    Vector3 temp;

    LevelBuffer levelBuffer;
    LevelFactory levelFactory;
    bool death;


    // Use this for initialization
    void Start () {

        levelManager = this;
        levelBuffer = this.GetComponent<LevelBuffer>();
        //levelFactory = this.GetComponent<LevelFactory>();

        GameObject Lvl = GameObject.Find("Buildings1");
        GameObject levelFactoryRef = GameObject.Find("LevelFactory");
        levelFactory = levelFactoryRef.GetComponent<LevelFactory>();

        if(Lvl != null)
        {
            buildingHeight = Lvl.transform.localScale.y + 2f;

            levelBuffer.Enqueue(Lvl);

            Debug.Log(buildingHeight);
            temp = new Vector3(0, 73.95f, 0);
            Debug.Log("change height: " + buildingHeight);
            if (!loadOnce)
            {
                //for (int i = 0; i < 2; i++)
                //{
                    LoadNewLevel();
                //}
                loadOnce = true;
            } 
        }
        else
        {
            Debug.Log("initial level null");
        }
    }

    //Get level from our level factory and add it 
    //to the top of the buffer.
    public void LoadNewLevel()
    {
        
        obj = levelFactory.GetPooledObject();

        if (obj != null)
        {
            top = levelBuffer.topOfBuffer();
            obj.SetActive(true);
            obj.transform.position = top.GetComponent<Level>().GetTransoform().position;
            obj.transform.position += temp;
            levelBuffer.Enqueue(obj);
            Debug.Log("active level count is " + levelBuffer.BufferCount());
        }
    }

    public void removeLevel()
    {
        if (!death)
        {
            //Dump the lower level
            rem = levelBuffer.Dequeue();
            rem.SetActive(false);
            LoadNewLevel();
            //Debug.Log("after removing, count is " + levelBuffer.BufferCount());
        }
    }


    public void setDeath(bool inDeath)
    {
        death = inDeath;
        levelBuffer.setLevelDeaths();
        //obj = levelBuffer.peek();
        //obj.GetComponent<Level>().setDeath(death);
    }


}
