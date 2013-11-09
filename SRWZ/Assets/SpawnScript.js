#pragma strict

public var zombie : GameObject;

public var xPlaces : int[];
public var spawnedZombies : GameObject[];
private var xCount : int;

public var spawnRate : int;
private var spawnTick : int;

public var moveTime : float;
public var moveDist : int;

function Start () {
	spawnTick = 0;
	xCount = 0;
	spawnedZombies = new GameObject[xPlaces.Length];
}

function Update () {
	spawnTick++;
	if (spawnTick >= spawnRate) {
		spawnTick = 0;
		xCount++;
		var doneCheck : boolean = true;
		var fullZoms : boolean = false;
		for (var i : int = 0; i < xPlaces.Length; i++) {
			if (xCount == xPlaces[i]) {
				
				if (xPlaces[i] != -1) {
					doneCheck = false;
				}
				var newZom : GameObject = GameObject.Instantiate(zombie, new Vector3(30,2,0), Quaternion.identity);
				spawnedZombies[i] = newZom;
				if (i == xPlaces.Length-1) {
					fullZoms = true;
				}
				move(newZom);
				xPlaces[i] = -1;
			}
		}
		if (doneCheck && fullZoms) {
			var ateCount : int = 0;
			for (var x : int = 0; x < xPlaces.Length; x++) {
				if (!spawnedZombies[x].renderer.enabled) {
					ateCount++;
				}
			}
		}
	}
}

function move(zombie : GameObject) {
	yield StartCoroutine("moveZom",zombie);
}

function moveZom(zom : GameObject) {
	var timeSpent : float = 0;
	var startPos : Vector3 = zom.transform.position;
	var newPos : Vector3 = startPos;
	newPos.x -= moveDist;
	
	
	while (timeSpent < moveTime) {
		zom.transform.position = Vector3.Lerp(startPos, newPos, (timeSpent/moveTime));
		timeSpent += Time.deltaTime;
		yield;
	}
}