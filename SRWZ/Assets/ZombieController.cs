using UnityEngine;
using System.Collections;

public class ZombieController : MonoBehaviour {

	public GameObject player;

	// Use this for initialization
	void Start () {
		// find the player game object
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		// move towards the player object
		transform.position = Vector3.Lerp(transform.position, player.transform.position, 0.0001f);
	}
}
