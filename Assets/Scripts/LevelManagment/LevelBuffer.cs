using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This class keeps track of the Queue that 
//the levels are stored in the game for organization.
public class LevelBuffer : MonoBehaviour
{

    Queue<GameObject> activeLevels;

    GameObject topOfQueue;
    //Iterator<E> iter; 


    private void Awake()
    {
        activeLevels = new Queue<GameObject>();
    }


    void Start()
    {
        //activeLevels = new Queue<GameObject>();
       // iter = activeLevels.iterator();


    }


    public void Enqueue(GameObject inLvl)
    {
        topOfQueue = inLvl;
        activeLevels.Enqueue(inLvl);
    }

    public GameObject Dequeue()
    {
        return activeLevels.Dequeue();
    }

    public GameObject topOfBuffer()
    {

        if (topOfQueue != null)
        {
            return topOfQueue;
        }
        else
        {
            return null;
        }

    }

    public int BufferCount()
    {
        return activeLevels.Count;
    }

    public GameObject peek()
    {
        return activeLevels.Peek();
    }

    public void setLevelDeaths()
    {
        foreach (GameObject e in activeLevels)
        {
            if (e != null)
            {
                // do something with e
                e.GetComponent<Level>().setDeath(true);
            }
        }
    }


}
