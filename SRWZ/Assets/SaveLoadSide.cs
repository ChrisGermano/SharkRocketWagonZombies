using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SaveLoadSide : MonoBehaviour {

	public GameObject zombie;
	public GameObject water;
	
	public ZomSaver ZS;
	public ArrayList loadedZoms;
	private ArrayList generatedZoms;
	private ArrayList generatedPickups;
	private int spawned;
	private int pickups;
	
	public int spawnRate;
	private int spawnTick;

	// Use this for initialization
	void Start () {
		ZS = GameObject.FindGameObjectWithTag("ZomSaver").GetComponent<ZomSaver>();
		loadedZoms = ZS.savedZombies;
		generatedZoms = new ArrayList();
		generatedPickups = new ArrayList();
		spawned = 0;
		pickups = ZS.collectedWater.Count;
	}
	
	// Update is called once per frame
	void Update () {
		if (spawned < loadedZoms.Count) {
			spawnTick++;
			if (spawnTick >= spawnRate) {
				spawnTick = 0;
				spawned++;
				float newX = -13+(Random.Range(0,3)*13);
				GameObject tempZom = (GameObject)GameObject.Instantiate(zombie, new Vector3(newX,-3,0), Quaternion.identity);
				tempZom.transform.eulerAngles = new Vector3(0, 0, 180);
				generatedZoms.Add(tempZom);
			}
			//see if its about time we spawned the water
			if (ZS.water_index <= spawned && pickups > 0) {
				float newX = -13+(Random.Range(0,3)*13);
				GameObject tempWater = (GameObject)GameObject.Instantiate(water, new Vector3(newX,-3,0), Quaternion.identity);
				tempWater.transform.eulerAngles = new Vector3(0, 0, 180);
				generatedPickups.Add(tempWater);
				pickups--;
			}
		}

		int ateCount = 0;
		int completed = 0;

		//Move zombies
		for (int i = 0; i < spawned; i++) {
			GameObject tempZ = (GameObject)generatedZoms[i];
			Move(tempZ);
			if (!tempZ.renderer.enabled && tempZ.transform.position.z > -70) {
				ateCount++;
			} else if (tempZ.transform.position.z < -70) {
				completed++;
			}
		}

		//Move pickups
		for(int i = 0; i < generatedPickups.Count; i++) {
			GameObject tempW = (GameObject)generatedPickups[i];
			Move(tempW);
		}

		if((completed + ateCount) >= loadedZoms.Count) {
			
			ZS.savedZombies.Clear();

			foreach (GameObject z in generatedZoms) {
				if (z.renderer.enabled) {
					ZS.missedZombies.Add(z);
				}
			}

			Application.LoadLevel("TopDown");
		}
	}

	private void Move(GameObject zom) {
		Vector3 startPos = zom.transform.position;
		Vector3 newPos = startPos;
		newPos.z -= 0.2f;
		zom.transform.position = newPos;
	}

}
