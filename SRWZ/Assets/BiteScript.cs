using UnityEngine;
using System.Collections;

public class BiteScript : MonoBehaviour {
	
	public float fuelLeft;
	public float maxFuel;

	public GameObject ZS;

	// Use this for initialization
	void Start () {
		fuelLeft = maxFuel;
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (fuelLeft);
		if (fuelLeft > 0f) {
			if (Input.GetMouseButton(0)) {
				fuelLeft -= 0.5f;
			}
		} else {
			fuelLeft = 0f;
			Dragable dragScript = (Dragable)this.GetComponent (typeof(Dragable));
			dragScript.moveLimit = 0;
		}
	}

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag == "Zombie") {
			ZomSaver zoms = GameObject.FindGameObjectWithTag("ZomSaver").GetComponent<ZomSaver>();
			zoms.savedZombies.Add(col.gameObject);
			col.gameObject.renderer.enabled = false;
		}
		else if (col.gameObject.tag == "WaterPickup") {
			ZomSaver zoms = GameObject.FindGameObjectWithTag("ZomSaver").GetComponent<ZomSaver>();
			zoms.collectedWater.Add(col.gameObject);
			zoms.water_index = zoms.savedZombies.Count;
			col.gameObject.renderer.enabled = false;
			Debug.Log("Picked Up The Water At " + zoms.water_index);
		}
		else if (col.gameObject.tag == "FuelPickup") {
			fuelLeft += (maxFuel / 5);
			col.gameObject.renderer.enabled = false;
			Debug.Log("Picked Up The Fuel!");
		}
	}
}
