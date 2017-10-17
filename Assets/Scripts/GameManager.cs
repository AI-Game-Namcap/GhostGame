using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    private float coinCount;
    private float total;
    private float currentCount;

    public Text score;
	// Use this for initialization
	void Start () {

        coinCount = GameObject.FindGameObjectsWithTag("coin").Length;
        total = coinCount;
	}
	
	// Update is called once per frame
	void Update () {
        coinCount = GameObject.FindGameObjectsWithTag("coin").Length;
        currentCount = total - coinCount;
        score.text = "Coins: " + currentCount + "/" + total;

        if(coinCount <= 0)
        {
            Application.LoadLevel("win");
        }
	}
    
}
