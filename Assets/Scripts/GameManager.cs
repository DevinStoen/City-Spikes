using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public GUIStyle TextStyle;
    public GUIStyle TextStyle2;
    public int score;
    public GUIStyle btnStyle2;
    bool death;
    bool retry;
    bool transOnce = false;
    CameraScript myCam;
    public GameObject transition_curtain;
    public int highScore;
    public SpriteRenderer retryInstructions;
    float timer = 0;
    float highScoreTimer = 0;
    bool showHighScoreTitle = false;
    bool showParticleOnce = false;
    //public ParticleSystem feathers;

    public SoundScript soundRef;
    LevelManager lvlManagerRef;
    public StayScript stayRef;


    public GameObject plusOne;
    public GameObject plusTwo;
    public GameObject plusThree;

    public SpriteRenderer plusOneRender;
    public SpriteRenderer plusTwoRender;
    public SpriteRenderer plusThreeRender;

    public GameObject plusOneBread;
    public GameObject plusTwoBread;
    public GameObject plusThreeBread;

    public GameObject timesTwo;
    public GameObject timesThree;


    public SpriteRenderer plusOneBreadRender;
    public SpriteRenderer plusTwoBreadRender;
    public SpriteRenderer plusThreeBreadRender;
    

    bool plusTwoStreak;
    bool plusThreeStreak;

    float streakTimer = 0;


    public Transform player;
    public float newX;
    public float newY;
    public float finalX;
    public float finalY;


    public SpriteRenderer tapToJump;
    public SpriteRenderer finger_press;
    public SpriteRenderer blackBox;
    public SpriteRenderer highScoreTitle;
    public SpriteRenderer whiteFlash;

    int breadStreak = 0;

    void Awake()
    {
        myCam = GameObject.Find("Main Camera").GetComponent<CameraScript>();
        soundRef = GameObject.Find("Sound").GetComponent<SoundScript>();
        stayRef = GameObject.Find("StayObj").GetComponent<StayScript>();
        lvlManagerRef = GameObject.Find("LevelManager").GetComponent<LevelManager>();

    }


    // Use this for initialization
    void Start () {
       // PlayerPrefs.SetInt("HighScore", 0);
        death = false;
        retry = false;
        //plusOneRender = plusOne.GetComponent<SpriteRenderer>();
        // Shaking = false;
        fadeIn(tapToJump);
        fadeIn(finger_press);
        //stayRef.sendRetryCount();
        highScoreTitle.GetComponent<SpriteRenderer>().enabled = false;
        

        if (stayRef.getPlayCount() > 0)
        {
            blackBox.GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            fadeOut(blackBox);
        }

        highScore = PlayerPrefs.GetInt("HighScore");



        //score = 100;
    }
	
	// Update is called once per frame
	void Update () {

        TextStyle.fontSize = (int)(170.0f * (float)(Screen.width) / 1920.0f); //scale size font
        TextStyle2.fontSize = (int)(140.0f * (float)(Screen.width) / 1920.0f); //scale size font
        // retryInstructions.GetComponent<Renderer>().enabled = false;

        if (player.GetComponent<Player>().getTapCount() > 0 || death)
        {
            fadeOut(tapToJump);
            fadeOut(finger_press);
        }

        if(highScore > 4 && score > highScore)
        {
            if (!showHighScoreTitle)
            {
                highScoreTitle.transform.position = new Vector3(19, player.transform.position.y + 30, 70);
                showHighScoreTitle = true;
            }

            if (highScoreTimer < 6 && !death)
            {
                highScoreTimer += Time.deltaTime;

                
                highScoreTitle.GetComponent<SpriteRenderer>().enabled = true;
                
            }
            else
            {
                highScoreTitle.GetComponent<SpriteRenderer>().enabled = false;
            }
        }


        if (plusTwoStreak && !death && !plusThreeStreak)
        {
            streakTimer += 2 * Time.deltaTime;

            if(streakTimer < 6f)
            {
                timesTwo.transform.position = new Vector3(20, myCam.transform.position.y + 10, 60);

                

                if ((int) streakTimer % 2 == 0)
                {
                    timesTwo.GetComponent<SpriteRenderer>().enabled = true;
                }
                else
                {
                    timesTwo.GetComponent<SpriteRenderer>().enabled = false;
                }
            }
            else
            {

                timesTwo.GetComponent<SpriteRenderer>().enabled = false;
                plusTwoStreak = false;
                streakTimer = 0;


            }
        }
        else
        {
            timesTwo.GetComponent<SpriteRenderer>().enabled = false;
            
        }

        if (plusThreeStreak && !death)
        {
            streakTimer += 2 * Time.deltaTime;
            
            
            if (streakTimer < 6f)
            {
                timesThree.transform.position = new Vector3(20, myCam.transform.position.y + 10, 60);



                if ((int)streakTimer % 2 == 0)
                {
                    timesThree.GetComponent<SpriteRenderer>().enabled = true;
                }
                else
                {
                    timesThree.GetComponent<SpriteRenderer>().enabled = false;
                }
            }
            else
            {

                timesThree.GetComponent<SpriteRenderer>().enabled = false;
                plusThreeStreak = false;
                streakTimer = 0;

            }
        }
        else
        {
            timesThree.GetComponent<SpriteRenderer>().enabled = false;
            
        }




    }

    void OnGUI()
    {
        //GUI.Label(new Rect(10, 10, 100, 20), "Hello World!");
        if (!death && myCam.transform.position.y >= 13)
        {
            if (score > 0 && score < 10)
            {
                GUI.Label(new Rect(Screen.width / 2.1f, Screen.height / 20, Screen.width / 12, Screen.width / 12), score.ToString(), TextStyle);

               // GUI.Label(new Rect(Screen.width / 4f, Screen.height / 20, Screen.width / 12, Screen.width / 12), breadStreak.ToString(), TextStyle);
            }
            else if(score >= 10)
            {
                GUI.Label(new Rect(Screen.width / 2.25f, Screen.height / 20, Screen.width / 12, Screen.width / 12), score.ToString(), TextStyle);
            }
           
        }
        //GUI.Label(new Rect(0, Screen.height / 2, Screen.width / 12, Screen.width / 12), "-----------------------------------------------------", TextStyle);
        
        if (death)
        {
            
            if (score > PlayerPrefs.GetInt("HighScore"))
            {
                PlayerPrefs.SetInt("HighScore", score);
            }

            if (!showParticleOnce)
            {
                //feathers.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - 1, 70);
                //feathers.Play();

                showParticleOnce = true;
            }
            

            timer += Time.deltaTime;

            


            if (timer > 1.6f)
            {

                if (!retry)
                {
                    if ((int)timer % 2 == 0)
                    {
                        GUI.Label(new Rect(Screen.width / 3.5f, Screen.height / 1.9f, Screen.width / 13, Screen.width / 12), "Tap to Retry!", TextStyle2);
                    }

                    GUI.Label(new Rect(Screen.width / 4.7f, Screen.height / 3.6f, Screen.width / 13, Screen.width / 12), "      Score:  " + score.ToString(), TextStyle2);
                    GUI.Label(new Rect(Screen.width / 4.3f, Screen.height / 2.8f, Screen.width / 13, Screen.width / 12), "High Score:  " + PlayerPrefs.GetInt("HighScore").ToString(), TextStyle2);
                }
                

                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                {
                    if (!transOnce)
                    {
                        soundRef.PlayHarp();
                        retryInstructions.GetComponent<Renderer>().enabled = false;
                        transition_curtain.transform.position = new Vector3(this.transform.position.x, Camera.main.transform.position.y + 20, this.transform.position.z - 10);
                        retry = true;
                        myCam.setRetry(retry);
                        StartCoroutine(Example());
                        transOnce = true;
                        stayRef.incrementPlayCount();
                    }
                }
            }



        }

    }

    IEnumerator Example()
    {
        //print(Time.time);
        
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(0);
        //print(Time.time);
    }

    public void increaseScore()
    {

        if (breadStreak < 3)
        {
            score++;
            plusOne.transform.position = new Vector3(player.position.x, player.position.y, -1f);
            fadeOut(plusOneRender);
        }
        else if(breadStreak < 5)
        {
            score = score + 2;
            plusTwo.transform.position = new Vector3(player.position.x, player.position.y, -1f);
            fadeOut(plusTwoRender);
        }
        else
        {
            score = score + 3;
            plusThree.transform.position = new Vector3(player.position.x, player.position.y, -1f);
            fadeOut(plusThreeRender);
        }

    }

    public void breadScore()
    {
        
        breadStreak++;
        
        if (breadStreak < 4)
        {

            if(breadStreak == 3)
            {
                streakTimer = 0;
                plusTwoStreak = true;

                score = score + 2;
                soundRef.PlayFirstCombo();
                plusTwoBread.transform.position = new Vector3(player.position.x, player.position.y + 10, -1f);
                fadeOut(plusTwoBreadRender);
            }
            else
            {
                score++;
                plusOneBread.transform.position = new Vector3(player.position.x, player.position.y + 10, -1f);
                fadeOut(plusOneBreadRender);
            }

            
        }
        else if(breadStreak < 6)
        {

            if(breadStreak == 5)
            {
                streakTimer = 0;
                plusThreeStreak = true;
                score = score + 3;
                soundRef.PlaySecondCombo();
                plusThreeBread.transform.position = new Vector3(player.position.x, player.position.y + 10, -1f);
                fadeOut(plusThreeBreadRender);
            }
            else
            {
                score = score + 2;
                plusTwoBread.transform.position = new Vector3(player.position.x, player.position.y + 10, -1f);
                fadeOut(plusTwoBreadRender);
            }

           
        }
        else
        {
            score = score + 3;
            plusThreeBread.transform.position = new Vector3(player.position.x, player.position.y + 10, -1f);
            fadeOut(plusThreeBreadRender);
        }
        
        

        
    }

    public void resetBreadStreak()
    {
        
        breadStreak = 0;
        Debug.Log("streak " + breadStreak);
    }


    public void setDeath(bool inBool)
    {
        death = inBool;
        lvlManagerRef.setDeath(death);
        
    }

    public void fadeOut(SpriteRenderer fadeoutRenderer)
    {
        StartCoroutine(fadeOutTxt(fadeoutRenderer));

    }

    public void fadeIn(SpriteRenderer fadeinRenderer)
    {
        StartCoroutine(fadeInTxt(fadeinRenderer));

    }

    private IEnumerator fadeOutTxt(SpriteRenderer fadeOut)
    {
        float duration = .6f;
        float currentTime = 0f;

        float oldAlpha = 1.0f;
        float finalAlpha = 0.0f;

        if(fadeOut.name == "blackbox")
        {
            duration = 2;
            Debug.Log("black");
        }


        while (currentTime < duration)
        {
            float alpha = Mathf.Lerp(oldAlpha, finalAlpha, currentTime / duration);
            //blurRenderer.color = new Color(blurRenderer.color.r, blurRenderer.color.g, blurRenderer.color.b, alpha);
            //tapRenderer.color = new Color(blurRenderer.color.r, blurRenderer.color.g, blurRenderer.color.b, alpha);


            fadeOut.color = new Color(fadeOut.color.r, fadeOut.color.g, fadeOut.color.b, alpha);


            //tutLighttxt.color = new Color(blurRenderer.color.r, blurRenderer.color.g, blurRenderer.color.b, alpha);
            //tutRighttxt.color = new Color(tutRenderer.color.r, tutRenderer.color.g, tutRenderer.color.b, alpha);

            if(fadeOut.tag == "plus")
            {
                plusOne.transform.Translate(Vector3.up * Time.deltaTime * 5);
            }
            if(fadeOut.tag == "plustwo"){
                plusTwo.transform.Translate(Vector3.up * Time.deltaTime * 5);
            }
            if (fadeOut.tag == "plusthree")
            {
                plusThree.transform.Translate(Vector3.up * Time.deltaTime * 5);
            }

            if (fadeOut.tag == "plusonebread")
            {
                plusOneBread.transform.Translate(Vector3.up * Time.deltaTime * 5);
            }
            if (fadeOut.tag == "plustwobread")
            {
                plusTwoBread.transform.Translate(Vector3.up * Time.deltaTime * 5);
            }
            if (fadeOut.tag == "plusthreebread")
            {
                plusThreeBread.transform.Translate(Vector3.up * Time.deltaTime * 5);
            }



            currentTime += Time.deltaTime;
            yield return null;
        }

        //fadeOut.GetComponent<Renderer>().enabled = false;


        //blurRenderer.GetComponent<Renderer>().enabled = false;
        //tutLighttxt.GetComponent<Renderer>().enabled = false;
        //tutRighttxt.GetComponent<Renderer>().enabled = false;
        yield break;
    }


    private IEnumerator fadeInTxt(SpriteRenderer fadeOut)
    {
        float duration = .6f;
        float currentTime = 0f;

        float oldAlpha = 0.0f;
        float finalAlpha = 1.0f;

        while (currentTime < duration)
        {
            float alpha = Mathf.Lerp(oldAlpha, finalAlpha, currentTime / duration);
            //blurRenderer.color = new Color(blurRenderer.color.r, blurRenderer.color.g, blurRenderer.color.b, alpha);
            //tapRenderer.color = new Color(blurRenderer.color.r, blurRenderer.color.g, blurRenderer.color.b, alpha);


            fadeOut.color = new Color(fadeOut.color.r, fadeOut.color.g, fadeOut.color.b, alpha);


            //tutLighttxt.color = new Color(blurRenderer.color.r, blurRenderer.color.g, blurRenderer.color.b, alpha);
            //tutRighttxt.color = new Color(tutRenderer.color.r, tutRenderer.color.g, tutRenderer.color.b, alpha);

            if (fadeOut.tag == "plus")
            {
                plusOne.transform.Translate(Vector3.up * Time.deltaTime * 5);
            }
            if (fadeOut.tag == "plustwo")
            {
                plusTwo.transform.Translate(Vector3.up * Time.deltaTime * 5);
            }
            if (fadeOut.tag == "plusthree")
            {
                plusThree.transform.Translate(Vector3.up * Time.deltaTime * 5);
            }

            if (fadeOut.tag == "plusonebread")
            {
                plusOneBread.transform.Translate(Vector3.up * Time.deltaTime * 5);
            }
            if (fadeOut.tag == "plustwobread")
            {
                plusTwoBread.transform.Translate(Vector3.up * Time.deltaTime * 5);
            }
            if (fadeOut.tag == "plusthreebread")
            {
                plusThreeBread.transform.Translate(Vector3.up * Time.deltaTime * 5);
            }

            currentTime += Time.deltaTime;
            yield return null;
        }

        //fadeOut.GetComponent<Renderer>().enabled = false;


        //blurRenderer.GetComponent<Renderer>().enabled = false;
        //tutLighttxt.GetComponent<Renderer>().enabled = false;
        //tutRighttxt.GetComponent<Renderer>().enabled = false;
        yield break;
    }



    public int getScore()
    {

        return score;

    }





}
