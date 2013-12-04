using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SaveLoadSide : MonoBehaviour {

	public GameObject zombie;

	public GameObject ZS;
	public ArrayList loadedZoms;
	private ArrayList generatedZoms;
	private int spawned;
	
	public int spawnRate;
	private int spawnTick;

	// Use this for initialization
	void Start () {
		ZS = GameObject.Find("ZomSaver");
		loadedZoms = ZS.GetComponent<ZomSaver>().savedZombies;
		generatedZoms = new ArrayList();
		spawned = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (spawned < loadedZoms.Count) {
			spawnTick++;
			if (spawnTick >= spawnRate) {
				spawnTick = 0;
				spawned++;
				float newX = -13+(Random.Range(0,2)*13);
				GameObject tempZom = (GameObject)GameObject.Instantiate(zombie, new Vector3(newX,-3,0), Quaternion.identity);
				generatedZoms.Add(tempZom);
			}
		}

		int ateCount = 0;
		int completed = 0;

		for (int i = 0; i < spawned; i++) {
			GameObject tempZ = (GameObject)generatedZoms[i];
			Move(tempZ);
			if (!tempZ.renderer.enabled && tempZ.transform.position.z > -70) {
				ateCount++;
			} else if (tempZ.transform.position.z < -70) {
				completed++;
			}
		}

		if((completed + ateCount) >= loadedZoms.Count) {
			
			ZS.GetComponent<ZomSaver>().savedZombies.Clear ();

			foreach (GameObject z in generatedZoms) {
				if (z.renderer.enabled) {
					ZS.GetComponent<ZomSaver>().missedZombies.Add(z);
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
