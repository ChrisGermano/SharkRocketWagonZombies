#pragma strict

private var canEat : boolean;
public var eatCD : float;
private var eatCount : float;

//0 1 2 for left middle right
private var position : int;

function Start () {
	eatCount = 0;
	canEat = true;
	position = 1;
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
	}
}