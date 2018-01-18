using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFactory : MonoBehaviour {

    List<GameObject> levels;
    int randPooledInt = 0;
    GameObject obj;
    public int pooledAmount = 6;

    public GameObject[] building;


    private void Awake()
    {
        levels = new List<GameObject>();

        for (int i = 0; i < pooledAmount; i++)
        {
            obj = (GameObject)Instantiate(building[i]);
            obj.SetActive(false);
            levels.Add(obj);
            //levels are ready  
        }
    }


    void Start()
    {
       
        
    }
   
    //Result of level factory.
    public GameObject GetPooledObject()
    {
        //Debug.Log(levels.Count);
        //based on the terrain state of the game,
        //pull a background image, choose a level skeleton
        //and choose correct level obsticals.

        //temarary level generation
        randPooledInt = Random.Range(0, pooledAmount);
        while (levels[randPooledInt].activeInHierarchy)
        {
            randPooledInt = Random.Range(0, pooledAmount);
        }
        return levels[randPooledInt];
    }


    public void changeLevelStates()
    {
        //changes the levels being spawned from "forest" to "snow" for example
    }

}
