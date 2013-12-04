#pragma strict

private var canEat : boolean;
public var eatCD : float;
private var eatCount : float;
private var canHurt : boolean; //can Angus take damage
private var health : int;
private var ZS : GameObject;

//0 1 2 for left middle right
private var position : int;

function Start () {
	eatCount = 0;
	canEat = true;
	position = 1;
	health = 5;
	ZS = GameObject.Find("ZomSaver");
}

/*
TODO: fix this so it works... lol
*/
function OnGUI () {
		if (health <= 0) {
			GUI.Label (Rect (200, 10, 100, 20), "I SO DEAD FRUM ZOMS. GAM OIVAY.");
		}
		// fuel box background
		var bgTexture : Texture2D = new Texture2D(1, 1);
		bgTexture.SetPixel(0, 0, Color.red);
		bgTexture.Apply();
		GUI.Box(new Rect(25, 20, 100, 10), bgTexture);

		// fuel left box
		var boxTexture : Texture2D = new Texture2D(1, 1);
		boxTexture.SetPixel(0, 0, Color.green);
		boxTexture.Apply();
		GUI.Box(new Rect(25, 20, (health > 0 ? health : 0), 10), boxTexture);
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
	
	var eatPercentage : float = (eatCount/eatCD);
	var eatBar : Color = new Color(0f, eatPercentage, 0f);
	GameObject.FindGameObjectWithTag("PlayerBite").renderer.material.color = eatBar;
	
	if (eatCount == eatCD) {
		canEat = true;
	}
	
	this.transform.position.x = -13 + (position * 13);
	
}

function OnTriggerStay(col : Collider) {
	if (Input.GetKeyDown("space")) {
		if (canEat && col.gameObject.tag == "Zombie") {
			col.gameObject.renderer.enabled = false;
		}
		canEat = false;
		eatCount = 0;
	} else {
		//This logic doesn't work
		/*
		if(col.gameObject.tag == "Zombie" && canHurt) {
			//Debug.Log("Zombie Hurt Us!");
			health--;
			canHurt = false;
		}
		*/
	}
}

function OnTriggerExit(col : Collider) {
	if(col.gameObject.tag == "Zombie") {
		canHurt = true;
	}
}