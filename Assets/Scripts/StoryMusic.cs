using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryMusic : MonoBehaviour {

	
	// Update is called once per frame
	void Update () {
        if (Application.loadedLevelName == "Level1")
        {
            Destroy(this.gameObject);
            

        }
        



    }

    public AudioClip[] audioClip;
    AudioSource audio;
    //static bool AudioBegin = false;
    void Start()
    {
        //audio = GetComponent<AudioSource>();
        
            // audio.PlayOneShot(audioClip[0], 1f);
            GetComponent<AudioSource>().Play();
            DontDestroyOnLoad(gameObject);
            //AudioBegin = true;
        
        

    }
}
