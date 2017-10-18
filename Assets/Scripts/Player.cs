﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    private float speed = 3;
   
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        var move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        transform.position += move * speed * Time.deltaTime;
	}

    void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "hunter")
        {
            Application.LoadLevel("GameOver");            
        }
    }
}
