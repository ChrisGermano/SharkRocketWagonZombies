using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

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
	public float LEFT_BOUND = -46f;
	public float RIGHT_BOUND = 46f;

	// Use this for initialization
	void Start () {
	
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
		GameObject z = (GameObject)Instantiate(zombieObj);
		z.transform.position = new Vector3(
			Random.Range(LEFT_BOUND, RIGHT_BOUND),
			z.transform.position.y,
			Random.Range(TOP_BOUND - BOUND_RANGE, TOP_BOUND));
	}
}

