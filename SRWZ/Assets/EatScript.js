#pragma strict

private var canEat : boolean;
public var eatCD : float;
private var eatCount : float;



function Start () {
	eatCount = 0;
	canEat = true;
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
		Debug.Log("Can Eat");
	}
}

function OnTriggerStay(col : Collider) {
	if (Input.GetKeyDown("space")) {
		if (canEat && col.gameObject.tag == "Zombie") {
			col.gameObject.renderer.enabled = false;
		}
		Debug.Log("Can't Eat");
		canEat = false;
		eatCount = 0;
	}
}