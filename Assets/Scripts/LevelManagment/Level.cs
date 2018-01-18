using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This is the base Level class. soon to be abstract
public class Level : MonoBehaviour {


    
    //public bool off;
    public bool seen;
    Renderer levelRenderer;
    LevelManager levelManagerRef;
    GameManager gameManagerRef;


    public List<Transform> leftLevels;
    public List<Transform> rightLevels;

    public GameObject[] obsticals;
    public GameObject[] food;


    GameObject obj;
    ///List<Game>
 
    public int randRightDeacivateAmt = 0;
    public int randRightDeacivateAmt2 = 0;
    public int randRightDeacivateAmt3 = 0;


    public int randLeftDeacivateAmt = 0;
    public int randLeftDeacivateAmt2 = 0;
    public int randLeftDeacivateAmt3 = 0;

    public int randFoodDeactivate = 0;

    bool genOnce = false;

    public int genCount = 0;

    public bool death;


    void Awake()
    {
        levelRenderer = this.GetComponentInChildren<SpriteRenderer>();
        levelManagerRef = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        gameManagerRef = GameObject.Find("GameManager").GetComponent<GameManager>();
        leftLevels = new List<Transform>();
        rightLevels = new List<Transform>();
        

        foreach (Transform child in transform)
        {
            //child is your child transform
            if (child.tag == "hazardL")
            {
                leftLevels.Add(child);
            }
            else if (child.tag == "hazardR")
            {
                rightLevels.Add(child);
            }
          



        }

    }


    // Use this for initialization
    void Start () {
      
        //for (int i = 0; i < obsticalPoolAmt; i++)
        //{
        //    obj = (GameObject)Instantiate(obsticals[i]);
        //    obj.SetActive(false);
        //    levels.Add(obj);
        //    //levels are ready  
        //}


        //if (this.gameObject.name == "Buildings1")
        //{
        //    Debug.Log(leftLevels.Count);
        //    randRightDeacivateAmt = Random.Range(0, rightLevels.Count);
        //    randLeftDeacivateAmt = Random.Range(0, leftLevels.Count);
        //    generateLevelObsticals(randRightDeacivateAmt, randLeftDeacivateAmt);
        //}
        //if (!genOnce)
        //{
        //    randRightDeacivateAmt = Random.Range(0, 3);
        //    randLeftDeacivateAmt = Random.Range(0, 3);
        //    while (randRightDeacivateAmt == randLeftDeacivateAmt)
        //    {
        //        randRightDeacivateAmt = Random.Range(0, 3);
        //        randLeftDeacivateAmt = Random.Range(0, 3);
        //    }
        //    generateLevelObsticals(randRightDeacivateAmt, randLeftDeacivateAmt);
        //    genOnce = true;
        //}
    }

    // Update is called once per frame
    void Update () {

        if (levelRenderer.isVisible)
        {
            if (!death)
            {
                seen = true;
                if (!genOnce)
                {
                    //randRightDeacivateAmt = Random.Range(1, 3);
                    //randLeftDeacivateAmt = Random.Range(1, 3);

                    //while (randRightDeacivateAmt == randLeftDeacivateAmt)
                    //{

                    if (this.gameObject.name != "Buildings1")
                    {

                        randLeftDeacivateAmt = Random.Range(0, 2);
                        randLeftDeacivateAmt2 = Random.Range(2, 4);
                        randLeftDeacivateAmt3 = Random.Range(4, 6);


                        randRightDeacivateAmt = Random.Range(0, 2);
                        randRightDeacivateAmt2 = Random.Range(2, 4);
                        randRightDeacivateAmt3 = Random.Range(4, 6);


                        randFoodDeactivate = Random.Range(0, 6);

                        //rightLevels[randRightDeacivateAmt].GetComponent<Obstical>().setActive(false);
                        //rightLevels[randRightDeacivateAmt2].GetComponent<Obstical>().setActive(false);
                        //rightLevels[randRightDeacivateAmt3].GetComponent<Obstical>().setActive(false);

                        //leftLevels[randLeftDeacivateAmt].GetComponent<Obstical>().setActive(false);
                        //leftLevels[randLeftDeacivateAmt2].GetComponent<Obstical>().setActive(false);
                        //leftLevels[randLeftDeacivateAmt3].GetComponent<Obstical>().setActive(false);



                        //}
                        generateLevelObsticals(randRightDeacivateAmt, randLeftDeacivateAmt);
                        generateLevelObsticals(randRightDeacivateAmt2, randLeftDeacivateAmt2);
                        generateLevelObsticals(randRightDeacivateAmt3, randLeftDeacivateAmt3);




                        


                        for (int i = 0; i < food.Length; i++)
                        {
                            if (i != randFoodDeactivate)
                            {
                                food[i].GetComponent<FoodScript>().setActive(false);
                            }
                        }



                    }
                    else
                    {
                        randLeftDeacivateAmt = Random.Range(1, 2);
                        randRightDeacivateAmt = Random.Range(0, 2);

                        randLeftDeacivateAmt2 = Random.Range(2, 4);
                        randRightDeacivateAmt2 = -1;

                        generateLevelObsticals(randRightDeacivateAmt, randLeftDeacivateAmt);

                        generateLevelObsticals(randRightDeacivateAmt2, randLeftDeacivateAmt2);


                        


                    }
                    //genCount++;
                    genOnce = true;
                }
            }
            else
            {
                for (int j = 0; j < food.Length; j++)
                {
                    if (food[j].GetComponent<FoodScript>().isActive())
                    {
                        food[j].GetComponent<FoodScript>().disapear();
                    }
                    
                }
            }
        
            //Debug.Log("SEEEEN");
        }

        if (seen && !levelRenderer.isVisible)
        {
            
            seen = false;
            //genCount = 0;
            genOnce = false;

            if (!death)
            {


                for (int i = 0; i < leftLevels.Count; i++)
                {
                    leftLevels[i].GetComponent<Obstical>().setActive(true);
                }
                for (int i = 0; i < rightLevels.Count; i++)
                {
                    rightLevels[i].GetComponent<Obstical>().setActive(true);
                }
                for(int j = 0; j < food.Length; j++)
                {

                    //if u missed a bread 
                    //if (food[j].GetComponent<FoodScript>().isActive())
                    //{
                        //gameManagerRef.resetBreadStreak();
                    //}
                    //food[j].GetComponent<FoodScript>().setCollectedBool(false);
                    food[j].GetComponent<FoodScript>().setActive(true);
                    food[j].GetComponent<SpriteRenderer>().enabled = true;
                }
                if (levelManagerRef != null)
                {

                    levelManagerRef.removeLevel();

                }
            }
            
        }

    }

    public Transform GetTransoform()
    {
        return this.transform;
    }

    public void generateLevelObsticals(int rightDeactivate, int leftDeactivate)
    {
        


        if (!death)
        {

            if (rightDeactivate != -1)
            {
                rightLevels[rightDeactivate].GetComponent<Obstical>().setActive(false);
            }

            if (leftDeactivate != -1)
            {
                leftLevels[leftDeactivate].GetComponent<Obstical>().setActive(false);
            }
            //for (int i = 0; i < leftLevels.Count; i++)
            //{
            //    if (i == leftDeactivate)
            //    {
            //        leftLevels[i].GetComponent<Obstical>().setActive(false);
            //    }

            //}
        }

        //for (int i = 0; i < rightLevels.Count; i++)
        //{
        //    Debug.Log(rightDeactivate);
        //    if (i == rightDeactivate)
        //    {
        //        rightLevels[i].GetComponent<Obstical>().setActive(false);
        //    }

        //}


    }
  
    public void setDeath(bool inDeath)
    {
        death = inDeath;
    }

    //public void setActive(bool inBool)
    //{
    //    this.setActive(inBool);
    //}

}
