using UnityEngine;
using System.Collections;

public class ZomSaver : MonoBehaviour {

	public int remainingSpawns;
	public int water_index = 0; //basically, how many zombies before we collect water, so we know when to spawn 
	public ArrayList savedZombies = new ArrayList();
	public ArrayList missedZombies = new ArrayList();
	public ArrayList collectedWater = new ArrayList();

	// Use this for initialization
	void Start () {
		Object.DontDestroyOnLoad(this);
	}
	
	// Update is called once per frame
	void Update () {
	}
}
