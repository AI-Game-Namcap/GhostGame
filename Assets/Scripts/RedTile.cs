using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedTile : MonoBehaviour {

[SerializeField] RedTile[] links = null; // linked red tiles

	// pick a tile from the ones available to navigate to next
	public RedTile nextTile() {
		return links[(int)Random.Range(0,links.Length)];
	}
}
