using UnityEngine;
using System.Collections;

public class ZomSaver : MonoBehaviour {

	public int remainingSpawns;
	public ArrayList savedZombies = new ArrayList();
	public ArrayList missedZombies = new ArrayList();

	// Use this for initialization
	void Start () {
		Object.DontDestroyOnLoad(this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
