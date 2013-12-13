#pragma strict

private var canEat : boolean;
public var eatCD : float;
private var eatCount : float;
private var canHurt : boolean; //can Angus take damage
private var health : int;
private var max_health : int;
private var ZS : GameObject;

public var biteMat : Material;
public var idleMat : Material;

//0 1 2 for left middle right
private var position : int;

function Start () {
	eatCount = 0;
	canEat = true;
	position = 1;
	max_health = 5;
	health = max_health;
	ZS = GameObject.Find("ZomSaver");
}

/*
TODO: fix this so it works... lol
*/
function OnGUI () {
	GUI.Label(Rect(100, 10, 100, 20), "Angus HP: " + health + " / " + max_health);
}

function Update() {
	if (Input.GetKeyDown("w")) {
		if (position > 0) {
			position--;
		}
	} else if (Input.GetKeyDown("s")) {
		if (position < 2) {
			position++;
		}
	}
	//check our health
	if(health <= 0) {
		//Application.LoadLevel("TopDownTest"); //game over screen here
		canEat = false;
	}
}

function LateUpdate () {
	
	if (eatCount < eatCD) {
		eatCount++;
	}
	
	if (eatCount == eatCD) {
		canEat = true;
		this.renderer.material = idleMat;
	}
	
	this.transform.position.x = -13 + (position * 13);
	
}

function OnTriggerEnter(col : Collider) {
	if(col.gameObject.tag == "WaterPickup") {
		health++;
		Debug.Log("HEALTH = " + health);
		col.gameObject.renderer.enabled = false;
	}
}

function OnTriggerStay(col : Collider) {
	if (Input.GetKeyDown("space")) {
		if (canEat && col.gameObject.tag == "Zombie") {
			this.renderer.material = biteMat;
			col.gameObject.renderer.enabled = false;
		}
		canEat = false;
		eatCount = 0;
	} 
}

function OnTriggerExit(col : Collider) {
	if(col.gameObject.tag == "Zombie") {
		canHurt = true;
	}
}