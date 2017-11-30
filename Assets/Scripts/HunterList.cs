using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterList : MonoBehaviour {

	[SerializeField] private Hunter[] hunters;

	// Use this for initialization
	void Start () {	}

	// Method to send call-out to all the hunters in the list
	public void callHunters(Vector3 location) {
		for(int i=0; i<hunters.Length; i++) {
			if(hunters[i] != null)
				hunters[i].callIn(location);
		}
	}
}
