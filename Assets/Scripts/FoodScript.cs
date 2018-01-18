using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodScript : MonoBehaviour {


    public ParticleSystem breadCrumbs;
    Renderer foodRenderer;
    bool seen;
    bool collect;
    

    GameManager gameManagerRef;

    float timer = 0;

    void Awake()
    {
        foodRenderer = this.GetComponentInChildren<SpriteRenderer>();
    }

    // Use this for initialization
    

	// Use this for initialization
	void Start () {
        gameManagerRef = GameObject.Find("GameManager").GetComponent<GameManager>();

    }
	
	// Update is called once per frame
	void Update () {

        if (foodRenderer.isVisible && seen == false)
        {

            seen = true;

        }

        if(seen && !foodRenderer.isVisible && this.GetComponent<SpriteRenderer>().enabled == true)
        {
            gameManagerRef.resetBreadStreak();
            seen = false;
        }


       



	}

    public void setActive(bool inBool)
    {
        this.gameObject.SetActive(inBool);
    }

    public bool isActive()
    {
        if(this.gameObject.GetComponent<SpriteRenderer>().enabled == true && this.gameObject.activeInHierarchy)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void collected()
    {
        seen = false;
        breadCrumbs.transform.position = this.gameObject.transform.position;
        breadCrumbs.Play();
        
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        //do other stuff
    }

    public void disapear()
    {
       
        fadeOut(this.gameObject.GetComponent<SpriteRenderer>());
    }


    public void fadeOut(SpriteRenderer fadeoutRenderer)
    {
        StartCoroutine(fadeOutTxt(fadeoutRenderer));

    }

    private IEnumerator fadeOutTxt(SpriteRenderer fadeOut)
    {
        float duration = .6f;
        float currentTime = 0f;

        float oldAlpha = 1.0f;
        float finalAlpha = 0.0f;

        if (fadeOut.name == "blackbox")
        {
            duration = 2;
            Debug.Log("black");
        }


        while (currentTime < duration)
        {
            float alpha = Mathf.Lerp(oldAlpha, finalAlpha, currentTime / duration);
    
            fadeOut.color = new Color(fadeOut.color.r, fadeOut.color.g, fadeOut.color.b, alpha);


        
            currentTime += Time.deltaTime;
            yield return null;
        }

        //fadeOut.GetComponent<Renderer>().enabled = false;


        //blurRenderer.GetComponent<Renderer>().enabled = false;
        //tutLighttxt.GetComponent<Renderer>().enabled = false;
        //tutRighttxt.GetComponent<Renderer>().enabled = false;
        yield break;
    }

}
