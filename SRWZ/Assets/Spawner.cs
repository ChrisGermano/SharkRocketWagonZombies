using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public GameObject zomSaverObj;
	public GameObject zombieObj;
	// number of zombies to spawn
	public int NUM_TO_SPAWN = 20;
	// number of spawned zombies
	public int spawned = 0;
	// spawn interval in seconds
	public float spawnInterval = 5;
	// time of last spawn
	public float lastSpawnTime = 0;
	// spawning bounds
	public float BOUND_RANGE = 6f;
	public float TOP_BOUND = 46f;
	public float LEFT_BOUND = -110f;
	public float RIGHT_BOUND = 110f;

	// Use this for initialization
	void Start () {
		if (!GameObject.FindGameObjectWithTag("ZomSaver")) {
			Instantiate(zomSaverObj);
		}
		ZomSaver ZS = GameObject.FindGameObjectWithTag("ZomSaver").GetComponent<ZomSaver>();
		NUM_TO_SPAWN = ZS.remainingSpawns;
		ArrayList MZ = ZS.missedZombies;
		foreach (GameObject zom in MZ) {
			GameObject newZ = (GameObject)Instantiate(zombieObj);
			if (zom == null) {
				float x = Random.Range(LEFT_BOUND, RIGHT_BOUND);
				float z;
				//trying to get zombies to spawn closer to the house from the sides
				//so that they dont all just group at the top
				if(x > 100f || x < -100f) {z = 0;}
				else{z = Random.Range(TOP_BOUND - BOUND_RANGE, TOP_BOUND);}
				newZ.transform.position = new Vector3(x,1f,z);
			} else {
				newZ.transform.position = zom.transform.position;
			}
		}
		ZS.missedZombies.Clear();
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - lastSpawnTime > spawnInterval && spawned < NUM_TO_SPAWN) {
			Spawn();
		}
	}

	void Spawn () {
		lastSpawnTime = Time.time;
		spawned++;
		GameObject zom = (GameObject)Instantiate(zombieObj);
		float x = Random.Range(LEFT_BOUND, RIGHT_BOUND);
		float z;
		//trying to get zombies to spawn closer to the house from the sides
		//so that they dont all just group at the top
		if(x > 100f || x < -100f) {z = 0;}
		else{z = Random.Range(TOP_BOUND - BOUND_RANGE, TOP_BOUND);}
		zom.transform.position = new Vector3(x,zom.transform.position.y,z);
	}
}

