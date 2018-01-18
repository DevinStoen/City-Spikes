using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundScript : MonoBehaviour
{

    public AudioSource[] gameSounds;
    int rand = 0;

    

    void Awake()
    {
        if (GameObject.FindGameObjectsWithTag(gameObject.tag).Length > 1)
        {
            Destroy(gameObject);
        }
    }
    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayJumpSound()
    {

        rand = Random.Range(5, 9);
        gameSounds[rand].Play();

        gameSounds[12].Play();

        //gameSounds[2].Play();
    }

    public void PlayDeathSound()
    {
        //meSounds[1].Play();
        gameSounds[11].Play();
    }

    public void PlayWallHit()
    {
        gameSounds[3].Play();
    }


    public void PlayHarp()
    {
        gameSounds[4].Play();
    }

    public void PlayGrunt()
    {
        //rand = Random.Range(5, 9);

        gameSounds[10].Play();

    }

    public void PlayCitySOunds()
    {
        gameSounds[9].Play();

    }

    public void PlayBreadEat()
    {
        gameSounds[0].Play();
    }

    public void PlayFirstCombo()
    {
        gameSounds[1].Play();
    }

    public void PlaySecondCombo()
    {
        gameSounds[2].Play();
    }


}

